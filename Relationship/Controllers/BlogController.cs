using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Relationship.Models;
using System.Reflection.Metadata.Ecma335;

namespace Relationship.Controllers
{
    public class BlogController : Controller
    {
        private readonly RelationshipContext context;

        public BlogController(RelationshipContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            List<Blog> blogs = context.Blogs.Include(x => x.Category).Include(x => x.User).ToList();
            return View(blogs);
        }

        public IActionResult Bookmark()
        {
            var bookmarks = context.BlogUsers.Where(x => x.UserId == getAuthUserId()).OrderByDescending(x => x.Created_At).Include(x => x.Blog).Include(x => x.User).ToList();
            return View(bookmarks);
        }

        public IActionResult BookmarkHandler(int blogId)
        {
            int authUserId = getAuthUserId();
            var isHeInclude = context.BlogUsers.Where(x => x.UserId == authUserId && x.BlogId == blogId).FirstOrDefault();

            if (isHeInclude != null)
            {
                context.BlogUsers.Remove(isHeInclude);
                context.SaveChanges();
            }
            else
            {
                var status = context.Database.ExecuteSql($"EXEC [dbo].[StoreBlogUsers] @BlogId={blogId}, @UserId={authUserId}");
                if (status == 1)
                {
                    return Redirect("/Blog");
                }
            }
           
            return Redirect("/Blog");
        }

        public IActionResult BookmarkRemove(int blogId)
        {
            var markedBlog = context.BlogUsers.Where(x=>x.BlogId == blogId && x.UserId == getAuthUserId()).FirstOrDefault();
            if (markedBlog != null)
            {
                context.BlogUsers.Remove(markedBlog);
                context.SaveChanges();
                return RedirectToAction("Bookmark");
            }

            return RedirectToAction("Bookmark");
        }

        [HttpPost]
        public IActionResult Delete(int blogId)
        {
            var blog = context.Blogs.Where(x => x.Id == blogId).Include(x => x.BlogUsers).FirstOrDefault();
            if (blog != null)
            {
                context.Blogs.Remove(blog);
                context.SaveChanges();

                return Redirect("/Blog");
            }

            return Redirect("/Blog");
        }

        private int getAuthUserId()
        {
            return HttpContext.Session.GetInt32("AuthUserId") ?? 0;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Relationship.Models;

namespace Relationship.Controllers
{
    public class CategoryController : Controller
    {
        private readonly RelationshipContext context;

        public CategoryController(RelationshipContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var category = context.Categories.Include(x => x.Blogs).ToList();
            return View(category);
        }
    }
}

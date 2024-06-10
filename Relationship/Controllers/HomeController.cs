using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Relationship.Models;
using System.Diagnostics;

namespace Relationship.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RelationshipContext context;

        public HomeController(ILogger<HomeController> logger, RelationshipContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

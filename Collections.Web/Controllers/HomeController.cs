using Collections.Infrastructure.Data;
using Collections.Web.Models;
using Collections.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Collections.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var latestItems = _dbContext.Items
                .OrderByDescending(item => item.CreatedAt)
                .Take(5)
                .Include(item => item.Collection)
                .ToList();

            var largestCollections = _dbContext.Coollections
                .OrderByDescending(collection => collection.Items.Count) 
                .Take(5)
                .Include(collection => collection.Category)
                .ToList();

            var response = new HomeViewModel 
            {
                LatestItems = latestItems,
                LargestCollections = largestCollections
            };

            return View(response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
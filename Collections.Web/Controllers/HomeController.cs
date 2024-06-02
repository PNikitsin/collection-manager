using AutoMapper;
using Collections.Infrastructure.Data;
using Collections.Web.ViewModels;
using Collections.Web.ViewModels.Collection;
using Collections.Web.ViewModels.Home;
using Collections.Web.ViewModels.Item;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Collections.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var items = _dbContext.Items
                .OrderByDescending(item => item.CreatedAt)
                .Take(5)
                .Include(item => item.Collection)
                .ToList();

            var collections = _dbContext.Coollections
                .OrderByDescending(collection => collection.Items.Count)
                .Take(5)
                .Include(collection => collection.Category)
                .ToList();

            var latestItemViewModels = _mapper.Map<IEnumerable<ItemViewModel>>(items);
            var LargestCollectionViewModels = _mapper.Map<IEnumerable<CollectionViewModel>>(collections);

            var response = new HomeViewModel
            {
                LatestItems = latestItemViewModels,
                LargestCollections = LargestCollectionViewModels
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
using Collections.Infrastructure.Data;
using Collections.Web.Application.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collections.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public UserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var response = _dbContext.Coollections.Where(collection
                => collection.ApplicationUserId == User.GetUserId());

            return View(response);
        }
    }
}
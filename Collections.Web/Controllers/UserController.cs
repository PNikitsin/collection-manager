using AutoMapper;
using Collections.Infrastructure.Data;
using Collections.Web.Application.Extension;
using Collections.Web.ViewModels.Collection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Collections.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var collections = _dbContext.Coollections
                .Include(collections => collections.Category)
                .Where(collection => collection.UserId == User.GetUserId());

            var response = _mapper.Map<IEnumerable<CollectionViewModel>>(collections);

            return View(response);
        }
    }
}
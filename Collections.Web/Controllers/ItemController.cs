using Collections.Infrastructure.Data;
using Collections.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Collections.Web.ViewModels.Item;
using AutoMapper;

namespace Collections.Web.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ItemController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id, string returnUrl)
        {
            var collection = await _dbContext.Coollections.FirstOrDefaultAsync(collection => collection.Id == id);
            var response = new CreateItemViewModel { CollectionId = collection.Id, ReturnUrl = returnUrl };

            return View(response); 
        }

        [AllowAnonymous]
        public async Task <IActionResult> Details(int id)
        {
            var item = await _dbContext.Items
                .Include(item => item.Likes)
                .FirstOrDefaultAsync(item => item.Id == id);

            var response = _mapper.Map<ItemViewModel>(item);

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateItemViewModel createItemViewModel) 
        {
            if (!ModelState.IsValid) 
            {
                return View(createItemViewModel);
            }

            var item = new Item
            {
                Name = createItemViewModel.Name,
                Description = createItemViewModel.Description,
                CollectionId = createItemViewModel.Id,
                CreatedAt = DateTime.UtcNow,
            };

            await _dbContext.Items.AddAsync(item);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Details", "Collection", new { Id = createItemViewModel.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _dbContext.Items.FirstOrDefaultAsync(item => item.Id == id);

            if (item == null) 
            {
                return NotFound();
            }

            _dbContext.Items.Remove(item);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Details", "Collection", new { Id = item.CollectionId });
        }

        [HttpPost]
        public async Task<IActionResult> AddLike(int id)
        {
            var like = new Like
            {
                ItemId = id,
                Username = HttpContext.User.Identity.Name
            };

            await _dbContext.Likes.AddAsync(like);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Details", "Item", new { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveLike(int id)
        {
            var like = await _dbContext.Likes.
                FirstOrDefaultAsync(item => item.ItemId == id && item.Username == HttpContext.User.Identity.Name);

            _dbContext.Likes.Remove(like);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Details", "Item", new { Id = id });
        }
    }
}
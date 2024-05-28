using Collections.Infrastructure.Data;
using Collections.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Collections.Web.ViewModels.Item;

namespace Collections.Web.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ItemController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id, string returnUrl)
        {
            var collection = await _dbContext.Coollections.FirstOrDefaultAsync(c => c.Id == id);
            var model = new CreateItemViewModel { CollectionId = collection.Id, ReturnUrl = returnUrl };

            return View(model); 
        }

        public async Task <IActionResult> Details(int id)
        {
            var item = await _dbContext.Items.FirstOrDefaultAsync(c => c.Id == id);

            var itemViewModel = new ItemViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
            };

            return View(itemViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateItemViewModel model) 
        {
            var item = new Item
            {
                Name = model.Name,
                Description = model.Description,
                CollectionId = model.Id,
                CreatedAt = DateTime.UtcNow,
            };

            await _dbContext.Items.AddAsync(item);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Edit", "Collection", new { Id = model.Id });
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

            return RedirectToAction("Edit", "Collection", new { Id = item.CollectionId });
        }
    }
}
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
        public async Task<IActionResult> Create(int id)
        {
            var collection = await _dbContext.Coollections.FirstOrDefaultAsync(c => c.Id == id);
            var model = new CreateItemViewModel { CollectionId = collection.Id };

            return View(model); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateItemViewModel model) 
        {
            var item = new Item
            {
                Name = model.Name,
                Description = model.Description,
                CollectionId = model.Id
            };

            await _dbContext.Items.AddAsync(item);
            await _dbContext.SaveChangesAsync();

            return View(model);
        }
    }
}
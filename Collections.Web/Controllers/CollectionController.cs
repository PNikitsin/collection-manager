using Collections.Infrastructure.Data;
using Collections.Domain.Entities;
using Collections.Web.Extension;
using Collections.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Collections.Web.ViewModels.Collection;

namespace Collections.Web.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IImageService _imageService;

        public CollectionController(ApplicationDbContext dbContext, IImageService imageService)
        {
            _dbContext = dbContext;
            _imageService = imageService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index() 
        { 
            var response = _dbContext.Coollections.ToList();
            return View(response);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categories = _dbContext.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCollectionViewModel model)
        {
            if (ModelState.IsValid) 
            {
                string uniqueFileName = _imageService.UploadImage(model);

                var collection = new Collection()
                {
                    Name = model.Name,
                    Description = model.Description,
                    CollectionPicture = uniqueFileName,
                    ApplicationUserId = User.GetUserId(),
                    Author = User.Identity.Name,
                    CategoryId = model.CategoryId,
                };

                await _dbContext.Coollections.AddAsync(collection);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _dbContext.Coollections
                .Include(Category => Category.Category)
                .Include(Items => Items.Items)
                .FirstOrDefaultAsync(collection => collection.Id == id);

            var items = _dbContext.Items.Where(item => item.CollectionId == collection.Id).ToList();

            var detailsCollectionViewModel = new DetailsCollectionViewModel
            {
                Id = collection.Id,
                Name = collection.Name,
                Category = collection.Category.Name,
                Description = collection.Description,
                Author = collection.Author,
                CollectionPicture = collection.CollectionPicture,
                Items = items,
            };

            return View(detailsCollectionViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _dbContext.Coollections
                .Include(Category => Category.Category)
                .FirstOrDefaultAsync(collection => collection.Id == id);

            var items = _dbContext.Items.Where(item => item.CollectionId == collection.Id).ToList();

            var detailsCollectionViewModel = new DetailsCollectionViewModel
            {
                Id = collection.Id,
                Name = collection.Name,
                Category = collection.Category.Name,
                Description = collection.Description,
                Author = collection.Author,
                CollectionPicture = collection.CollectionPicture,
                Items = items,
            };

            return View(detailsCollectionViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var collection = await _dbContext.Coollections.FirstOrDefaultAsync(collection => collection.Id == id);

            if (collection == null)
            {
                return NotFound();
            }

            _dbContext.Coollections.Remove(collection);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    } 
}
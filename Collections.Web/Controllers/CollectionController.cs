using Collections.Infrastructure.Data;
using Collections.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Collections.Web.ViewModels.Collection;
using Collections.Web.Application.Services.Interfaces;
using AutoMapper;
using Collections.Web.Application.Extension;

namespace Collections.Web.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public CollectionController(ApplicationDbContext dbContext, IImageService imageService, IMapper mapper)
        {
            _dbContext = dbContext;
            _imageService = imageService;
            _mapper = mapper;
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
        public async Task<IActionResult> Create(CreateCollectionViewModel createCollectionViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createCollectionViewModel);
            }

            string uniqueFileName = _imageService.UploadImage(createCollectionViewModel);

            var collection = _mapper.Map<Collection>(createCollectionViewModel);

            collection.ApplicationUserId = User.GetUserId();
            collection.Author = User.Identity.Name;
            collection.CollectionPicture = uniqueFileName;

            await _dbContext.Coollections.AddAsync(collection);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var responce = new EditCollectionViewModel { Id = id };

            return View(responce);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCollectionViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            string uniqueFileName = _imageService.UploadImage(model);

            var collection = await _dbContext.Coollections.FirstOrDefaultAsync(collection => collection.Id == model.Id);
            collection.Name = model.Name;
            collection.Description = model.Description;
            collection.CollectionPicture = uniqueFileName;

            _dbContext.Coollections.Update(collection);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Details", "Collection", new { model.Id });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            var collection = await _dbContext.Coollections
                .Include(Category => Category.Category)
                .Include(Items => Items.Items)
                .FirstOrDefaultAsync(collection => collection.Id == id);

            if (collection == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<CollectionViewModel>(collection);

            return View(response);
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

            return RedirectToAction("Index", "User");
        }
    } 
}
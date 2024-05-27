﻿using Collections.Web.Data;
using Collections.Web.Entities;
using Collections.Web.Extension;
using Collections.Web.Services.Interfaces;
using Collections.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Create(CollectionViewModel model)
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
                    CategoryId = model.CategoryId,
                };

                await _dbContext.Coollections.AddAsync(collection);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
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
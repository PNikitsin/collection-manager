﻿using Collections.Web.Application.Services.Interfaces;
using Collections.Web.ViewModels.Collection;

namespace Collections.Web.Application.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;

        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public string UploadImage(CreateCollectionViewModel model)
        {
            string uniqueImageName = null;
            string path = Path.Combine(_environment.WebRootPath, "Uploads");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (model.CollectionPicture != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "Uploads");
                uniqueImageName = Guid.NewGuid().ToString() + "_" + model.CollectionPicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueImageName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CollectionPicture.CopyTo(fileStream);
                }
            }

            return uniqueImageName;
        }

        public string UploadImage(EditCollectionViewModel model)
        {
            string uniqueImageName = null;
            string path = Path.Combine(_environment.WebRootPath, "Uploads");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (model.CollectionPicture != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "Uploads");
                uniqueImageName = Guid.NewGuid().ToString() + "_" + model.CollectionPicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueImageName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CollectionPicture.CopyTo(fileStream);
                }
            }

            return uniqueImageName;
        }
    }
}
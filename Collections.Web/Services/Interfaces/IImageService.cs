using Collections.Web.ViewModels;

namespace Collections.Web.Services.Interfaces
{
    public interface IImageService
    {
        public string UploadImage(CreateCollectionViewModel model);
    }
}
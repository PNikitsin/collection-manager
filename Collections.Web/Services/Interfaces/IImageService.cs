using Collections.Web.ViewModels.Collection;

namespace Collections.Web.Services.Interfaces
{
    public interface IImageService
    {
        public string UploadImage(CreateCollectionViewModel model);
        public string UploadImage(EditCollectionViewModel model);
    }
}
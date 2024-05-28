using System.ComponentModel.DataAnnotations;

namespace Collections.Web.ViewModels.Collection
{
    public class CreateCollectionViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Picture")]
        public IFormFile CollectionPicture { get; set; }
    }
}
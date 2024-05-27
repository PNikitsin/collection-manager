using System.ComponentModel.DataAnnotations;

namespace Collections.Web.ViewModels
{
    public class CollectionViewModel
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
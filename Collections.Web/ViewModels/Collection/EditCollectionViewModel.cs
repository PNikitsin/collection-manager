using Collections.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Collections.Web.ViewModels.Collection
{
    public class EditCollectionViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public List<Category> Categories { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Picture")]
        public IFormFile CollectionPicture { get; set; }
    }
}
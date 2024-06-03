using System.ComponentModel.DataAnnotations;

namespace Collections.Web.ViewModels.Item
{
    public class CreateItemViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        [Required]
        [StringLength(512)]
        public string Description { get; set; }

        public int CollectionId { get; set; }

        public string ReturnUrl { get; set; }
    }
}
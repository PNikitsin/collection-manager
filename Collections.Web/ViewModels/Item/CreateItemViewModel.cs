using System.ComponentModel.DataAnnotations;

namespace Collections.Web.ViewModels.Item
{
    public class CreateItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public int CollectionId { get; set; }

        public string ReturnUrl { get; set; }
    }
}
using Collections.Web.ViewModels.Item;
using System.ComponentModel.DataAnnotations;

namespace Collections.Web.ViewModels.Collection
{
    public class CollectionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public List<ItemViewModel> Items { get; set; }

        [Display(Name = "Image")]
        public string CollectionPicture { get; set; }

    }
}
using Collections.Domain.Entities;

namespace Collections.Web.ViewModels.Item
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Like> Likes { get; set; }
        public List<Collections.Domain.Entities.Comment> Comments { get; set; }
    }
}
using Collections.Domain.Entities;
using Collections.Web.ViewModels.Comment;

namespace Collections.Web.ViewModels.Item
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Collection { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public List<Like> Likes { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}
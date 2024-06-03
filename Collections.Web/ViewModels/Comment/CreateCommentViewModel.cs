using System.ComponentModel.DataAnnotations;

namespace Collections.Web.ViewModels.Comment
{
    public class CreateCommentViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(512)]
        public string Text { get; set; }
        public int ItemId { get; set; }
    }
}
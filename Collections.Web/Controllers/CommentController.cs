using Collections.Domain.Entities;
using Collections.Infrastructure.Data;
using Collections.Web.ViewModels.Comment;
using Microsoft.AspNetCore.Mvc;

namespace Collections.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CommentController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            var response = new CreateCommentViewModel()
            {
                ItemId = id
            };

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentViewModel createCommentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createCommentViewModel);
            }

            var comment = new Comment()
            {
                Text = createCommentViewModel.Text,
                ItemId = createCommentViewModel.Id,
                Username = HttpContext.User.Identity.Name
            };

            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Details", "Item", new { Id = comment.ItemId }); 
        }
    }
}
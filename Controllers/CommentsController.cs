using Microsoft.AspNetCore.Mvc;
using UniProject1.Data;
using Microsoft.EntityFrameworkCore;

namespace UniProject1.Controllers
{
    public class CommentsController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly AppDbContext _db;

        public CommentsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _db.Comments
                .Include(c => c.User)
                .Include(c => c.TodoItem)
                .ToListAsync();
            return Ok(comments);
        }
    }
}

using ContestGenerator.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContestGenerator.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Question(int id)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == id);
            if (question is null)
                return NotFound();
            return View(question);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == id);
            if (question is null)
                return NotFound();
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return Redirect(HttpContext.Request.Headers.Referer.FirstOrDefault());
        }
    }
}

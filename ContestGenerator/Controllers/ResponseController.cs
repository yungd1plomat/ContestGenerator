using ContestGenerator.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContestGenerator.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class ResponseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResponseController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Response(int id)
        {
            var response = await _context.Responses.Include(x => x.Responses).FirstOrDefaultAsync(x => x.Id == id);
            if (response is null)
                return NotFound();
            return View(response);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _context.Responses.Include(x => x.Responses).FirstOrDefaultAsync(x => x.Id == id);
            if (response is null)
                return NotFound();
            _context.Responses.Remove(response);
            await _context.SaveChangesAsync();
            return Redirect(HttpContext.Request.Headers.Referer.FirstOrDefault());
        }
    }
}

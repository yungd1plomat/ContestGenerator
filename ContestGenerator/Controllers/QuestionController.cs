using ContestGenerator.Abstractions;
using ContestGenerator.Data;
using ContestGenerator.Models.Contest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContestGenerator.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class QuestionController : Controller
    {
        const int localTimeOffset = 7;

        private readonly ApplicationDbContext _context;

        private readonly IExcelRepo _excelRepo;

        public QuestionController(ApplicationDbContext dbContext, IExcelRepo excelRepo)
        {
            _excelRepo = excelRepo;
            _context = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Question(int id)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == id);
            if (question is null)
                return NotFound(id);
            return View(question);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == id);
            if (question is null)
                return NotFound(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return Redirect(HttpContext.Request.Headers.Referer.FirstOrDefault());
        }

        [HttpGet("download/{contestName}")]
        public async Task<IActionResult> Download(string contestName)
        {
            var date = DateTimeOffset.UtcNow.AddHours(localTimeOffset);
            var filename = $"[{date.ToString("dd.MM.yy_HH-mm")}]{contestName}_questions.xlsx";
            var questions = _context.Questions.Include(x => x.Contest)
                .Where(x => x.Contest.Name == contestName).ToList();
            if (!questions.Any())
                return RedirectToAction("List", "Contest");
            var sheet = await _excelRepo.Generate(questions);
            return File(sheet, "application/vnd.ms-excel", filename);
        }
    }
}

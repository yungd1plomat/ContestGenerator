using ContestGenerator.Abstractions;
using ContestGenerator.Data;
using ContestGenerator.Models.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContestGenerator.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class ResponseController : Controller
    {
        const int localTimeOffset = 7;

        private readonly ApplicationDbContext _context;

        private readonly IExcelRepo _excelRepo;

        public ResponseController(ApplicationDbContext dbContext, IExcelRepo excelRepo)
        {
            _context = dbContext;
            _excelRepo = excelRepo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Response(int id)
        {
            var response = await _context.Responses.Include(x => x.Responses)
                .Include(x => x.Contest.Criterias)
                .Include(x => x.Contest.ResponseEvaluation)
                .ThenInclude(x => x.Results)
                .ThenInclude(x => x.Criteria)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
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

        [HttpGet("download/{contestName}")]
        public async Task<IActionResult> Download(string contestName)
        {
            var date = DateTimeOffset.UtcNow.AddHours(localTimeOffset);
            var filename = $"[{date.ToString("dd.MM.yy_HH-mm")}]{contestName}_responses.xlsx";
            var responses = _context.Responses.Include(x => x.Responses)
                .Include(x => x.Contest)
                .Where(x => x.Contest.Name == contestName).ToList();
            if (!responses.Any())
                return RedirectToAction("List", "Contest");
            var sheet = await _excelRepo.Generate(responses);
            return File(sheet, "application/vnd.ms-excel", filename);
        }
    }
}

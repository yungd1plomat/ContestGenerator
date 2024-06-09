using ContestGenerator.Abstractions;
using ContestGenerator.Data;
using ContestGenerator.Models.Contest;
using ContestGenerator.Models.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContestGenerator.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "admin, jury")]
    public class ResponseController : Controller
    {
        const int localTimeOffset = 7;

        private readonly ApplicationDbContext _context;

        private readonly IExcelRepo _excelRepo;

        private readonly UserManager<AppUser> _userManager;

        public ResponseController(ApplicationDbContext dbContext, 
            IExcelRepo excelRepo,
            UserManager<AppUser> userManager)
        {
            _context = dbContext;
            _excelRepo = excelRepo;
            _userManager = userManager;
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

        [HttpPost("{id}/evaluate")]
        public async Task<IActionResult> Evaluate(int id, ResponseEvaluation evaluation)
        {
            evaluation.Id = null;
            var response = await _context.Responses.FirstOrDefaultAsync(x => x.Id == id);
            if (response is null)
                return NotFound();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var evaluations = _context.ResponseEvaluations.Include(x => x.Results)
                .Where(x => x.User == user)
                .ToList();
            _context.ResponseEvaluations.RemoveRange(evaluations);
            evaluation.User = user;
            await _context.ResponseEvaluations.AddAsync(evaluation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Response", new { id });
        }

        [HttpGet("{id}/evaluations")]
        public async Task<IActionResult> Evaluatitions(int id)
        {
            var results = _context.ResponseEvaluations.Include(x => x.Results)
                                                      .Include(x => x.User)
                                                      .Where(x => x.ResponseId == id)
                                                      .ToList()
                                                      .GroupBy(x => x.User);
            var averageEvaluations = new List<AverageUserEvaluation>();
            foreach (var userEvaluations in results)
            {
                foreach (var evaluation in userEvaluations)
                {
                    var averageUserEvaluation = new AverageUserEvaluation
                    {
                        Username = evaluation.User.UserName,
                    };
                    var avgEvaluation = evaluation.Results.Select(x => x.Evaluation)
                                                          .Average();
                    averageUserEvaluation.Evaluation = Math.Round(avgEvaluation, 1);
                    averageEvaluations.Add(averageUserEvaluation);
                }
            }
            return Ok(averageEvaluations);
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

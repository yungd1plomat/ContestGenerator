using ContestGenerator.Data;
using ContestGenerator.Models;
using ContestGenerator.Models.Contest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContestGenerator.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class ContestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContestController(ApplicationDbContext dbContext) 
        {
            _context = dbContext;
        }


        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] Contest contest) 
        {
            if (!ModelState.IsValid)
                return View();
            if (_context.Contests.Any(x => x.Name == contest.Name))
            {
                ModelState.AddModelError(string.Empty, $"Конкурс с названием {contest.Name} уже существует");
                return View();
            }
            await _context.AddAsync(contest);
            await _context.SaveChangesAsync();
            return View();
        }

        [HttpGet("list")]
        public async Task<IActionResult> List(int page = 0)
        {
            var chunked = _context.Contests.ToList().Chunk(14).ToList();
            if (page > chunked.Count - 1 && chunked.Any())
                return RedirectToAction("List", "Contest");

            return View(new ContestsViewmodel()
            {
                Page = page,
                Contests = chunked.Any() ? chunked[page] : Array.Empty<Contest>(),
            });
        }

        [HttpGet("delete/{contestName}")]
        public async Task<IActionResult> Delete(string contestName)
        {
            var contest = _context.Contests.Include(x => x.Partners)
                                                 .Include(x => x.Steps)
                                                 .Include(x => x.FormFields)
                                                 .ThenInclude(x => x.Predefined)
                                                 .Include(x => x.Helps)
                                                 .Include(x => x.Nominations)
                                                 .Include(x => x.PhotoUrls)
                                                 .Include(x => x.Reviews).FirstOrDefault(x => x.Name == contestName);
            if (contest is null)
                return BadRequest($"Конкурс {contest} не найден");
            _context.Contests.Remove(contest);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }


        [HttpGet("{contestName}")]
        [AllowAnonymous]
        public async Task<IActionResult> Contest(string contestName)
        {
            var contest = await _context.Contests.Include(x => x.Partners)
                                                 .Include(x => x.Steps)
                                                 .Include(x => x.FormFields)
                                                 .ThenInclude(x => x.Predefined)
                                                 .Include(x => x.Helps)
                                                 .Include(x => x.Nominations)
                                                 .Include(x => x.PhotoUrls)
                                                 .Include(x => x.Reviews).FirstOrDefaultAsync(x => x.Name == contestName);

            if (contest is null)
                return NotFound(contestName);
            return View(contest);
        }
    }
}

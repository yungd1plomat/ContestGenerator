using ContestGenerator.Data;
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

        [HttpGet("{contestName}")]
        public async Task<IActionResult> Contest(string contestName)
        {
            var contest = await _context.Contests.Include(x => x.Partners)
                                                 .Include(x => x.Steps)
                                                 .Include(x => x.FormFields)
                                                 .Include(x => x.Helps)
                                                 .Include(x => x.Nominations)
                                                 .Include(x => x.PhotoUrls)
                                                 .Include(x => x.Reviews).FirstOrDefaultAsync(x => x.Name == contestName);
        
            if (contest is null)
                return NotFound(contestName);
            return View(contest);
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
    }
}

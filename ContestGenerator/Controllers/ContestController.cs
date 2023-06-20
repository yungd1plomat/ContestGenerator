using ContestGenerator.Data;
using ContestGenerator.Models.Contest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            if (_context.Contests.Any(x => x.ShortName == contest.ShortName))
            {
                ModelState.AddModelError(string.Empty, $"Конкурс с названием {contest.ShortName} уже существует");
                return View();
            }
            await _context.AddAsync(contest);
            await _context.SaveChangesAsync();
            return View();
        }
    }
}

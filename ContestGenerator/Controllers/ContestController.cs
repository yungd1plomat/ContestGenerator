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
            var contestInfos = new List<ContestInfoViewmodel>();
            if (chunked.Any())
            {
                foreach (var contest in chunked[page])
                {
                    var info = new ContestInfoViewmodel()
                    {
                        Contest = contest,
                    };
                    info.ResponseCount = _context.Responses.Count(x => x.Contest == contest);
                    info.AnswersCount = 0;
                    contestInfos.Add(info);
                }
            }
            return View(new ContestsViewmodel()
            {
                Page = page,
                Contests = contestInfos,
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
            var responses = _context.Responses.Where(x => x.Contest == contest);
            if (responses.Any())
                _context.Responses.RemoveRange(responses);
            await _context.SaveChangesAsync();
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

        [HttpGet("{contestName}/send")]
        [AllowAnonymous]
        public async Task<IActionResult> GetForm(string contestName)
        {
            return RedirectToAction("Contest", new { contestName = contestName });
        }

        [HttpPost("{contestName}/send")]
        [AllowAnonymous]
        public async Task<IActionResult> GetForm(string contestName, IFormCollection form)
        {
            var contest = await _context.Contests.FirstOrDefaultAsync(x => x.Name == contestName);
            if (contest is null)
                return NotFound(contestName);
            var response = new Response()
            {
                Contest = contest,
                Responses = new List<FormResponse>(),
            };
            foreach (var item in form)
            {
                var formResponse = new FormResponse()
                {
                    Name = item.Key,
                    Value = item.Value.FirstOrDefault(),
                };
                response.Responses.Add(formResponse);
            }
            await _context.Responses.AddAsync(response);
            await _context.SaveChangesAsync();
            return RedirectToAction("Contest", new { contestName = contestName });
        }

        [HttpGet("{contestName}/responses")]
        public async Task<IActionResult> Responses(string contestName, int page = 0)
        {
            var chunked= _context.Responses.Include(x => x.Contest).Where(x => x.Contest.Name == contestName).ToList().Chunk(14).ToList();
            if (page > chunked.Count - 1 && chunked.Any())
                return RedirectToAction("Responses", "Contest");
            return View("Responses", new ResponsesViewmodel()
            {
                Page = page,
                Responses = chunked.Any() ? chunked[page] : Array.Empty<Response>(),
            });
        }
    }
}

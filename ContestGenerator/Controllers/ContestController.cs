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
        const int chunkSize = 14;

        private readonly ApplicationDbContext _context;

        public ContestController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] Contest modifiedContest)
        {
            var contest = await _context.Contests.Include(x => x.Partners)
                                                 .Include(x => x.Steps)
                                                 .Include(x => x.FormFields)
                                                 .ThenInclude(x => x.Predefined)
                                                 .Include(x => x.Helps)
                                                 .Include(x => x.Nominations)
                                                 .Include(x => x.PhotoUrls)
                                                 .Include(x => x.News)
                                                 .Include(x => x.Files)
                                                 .Include(x => x.Reviews).FirstOrDefaultAsync(x => x.Id == id);
            var files = await _context.Files.ToListAsync();
            if (contest is null)
                return NotFound(id);
            contest.Name = modifiedContest.Name;
            contest.LogoUrl = modifiedContest.LogoUrl;
            contest.MainPhotoUrl = modifiedContest.MainPhotoUrl;
            contest.ShortName = modifiedContest.ShortName;
            contest.FullName = modifiedContest.FullName;
            contest.Description = modifiedContest.Description;
            contest.Rules = modifiedContest.Rules;
            contest.History = modifiedContest.History;
            contest.Reviews = modifiedContest.Reviews;
            contest.PhotoUrls = modifiedContest.PhotoUrls;
            contest.Nominations = modifiedContest.Nominations;
            contest.Steps = modifiedContest.Steps;
            contest.FormFields = modifiedContest.FormFields;
            contest.Helps = modifiedContest.Helps;
            contest.News = modifiedContest.News;
            contest.Partners = modifiedContest.Partners;
            contest.Vk = modifiedContest.Vk;
            contest.Tg = modifiedContest.Tg;
            contest.Rutube = modifiedContest.Rutube;
            contest.Youtube = modifiedContest.Youtube;
            contest.Email = modifiedContest.Email;
            contest.Address = modifiedContest.Address;
            contest.Phone = modifiedContest.Phone;

            var contestFiles = modifiedContest.Files is null ? null : files.Where(x => modifiedContest.Files.Any(f => f.Name == x.Name)).ToList();
            contest.Files = contestFiles;

            _context.Contests.Update(contest);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var contest = await _context.Contests.Include(x => x.Partners)
                                                 .Include(x => x.Steps)
                                                 .Include(x => x.FormFields)
                                                 .ThenInclude(x => x.Predefined)
                                                 .Include(x => x.Helps)
                                                 .Include(x => x.Nominations)
                                                 .Include(x => x.PhotoUrls)
                                                 .Include(x => x.News)
                                                 .Include(x => x.Files)
                                                 .Include(x => x.Reviews).FirstOrDefaultAsync(x => x.Id == id);
            if (contest is null)
                return RedirectToAction("List");
            var files = await _context.Files.ToListAsync();
            var editVm = new EditViewmodel()
            {
                Contest = contest,
                Files = files,
            };
            return View(editVm);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var files = await _context.Files.ToListAsync();
            return View(files);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] Contest contest)
        {
            if (!ModelState.IsValid)
                return View(await _context.Files.ToListAsync());
            if (_context.Contests.Any(x => x.Name == contest.Name))
            {
                ModelState.AddModelError(string.Empty, $"Конкурс с названием {contest.Name} уже существует");
                return View(await _context.Files.ToListAsync());
            }
            if (contest.Files != null)
            {
                var fileIds = contest.Files.Select(x => x.Id).ToList();
                contest.Files = _context.Files.Where(x => fileIds.Contains(x.Id)).ToList();
            }
            await _context.AddAsync(contest);
            await _context.SaveChangesAsync();
            return View(await _context.Files.ToListAsync());
        }

        [HttpGet("list")]
        public async Task<IActionResult> List(int page = 0)
        {
            var chunked = _context.Contests.ToList().Chunk(chunkSize).ToList();
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
                    info.AnswersCount = _context.Questions.Count(x => x.Contest == contest);
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
                                           .Include(x => x.News)
                                           .Include(x => x.Reviews).FirstOrDefault(x => x.Name == contestName);
            if (contest is null)
                return BadRequest($"Конкурс {contest} не найден");
            var responses = _context.Responses.Include(x => x.Responses).Where(x => x.Contest == contest);
            if (responses.Any())
                _context.Responses.RemoveRange(responses);
            var questions = _context.Questions.Where(x => x.Contest == contest);
            if (questions.Any())
                _context.Questions.RemoveRange(questions);
            _context.Contests.Remove(contest);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpGet("{contestName}/responses")]
        public async Task<IActionResult> Responses(string contestName, int page = 0)
        {
            var chunked = _context.Responses.Include(x => x.Contest).Where(x => x.Contest.Name == contestName).ToList().Chunk(14).ToList();
            if (page > chunked.Count - 1 && chunked.Any())
                return RedirectToAction("Responses", "Contest");
            return View("Responses", new ResponsesViewmodel()
            {
                Page = page,
                Responses = chunked.Any() ? chunked[page] : Array.Empty<Response>(),
            });
        }

        [HttpGet("{contestName}/questions")]
        public async Task<IActionResult> Questions(string contestName, int page = 0)
        {
            var chunked = _context.Questions.Include(x => x.Contest).Where(x => x.Contest.Name == contestName).ToList().Chunk(14).ToList();
            if (page > chunked.Count - 1 && chunked.Any())
                return RedirectToAction("Questions", "Contest");
            return View("Questions", new QuestionsViewmodel()
            {
                Page = page,
                Questions = chunked.Any() ? chunked[page] : Array.Empty<Question>(),
            });
        }
    }
}

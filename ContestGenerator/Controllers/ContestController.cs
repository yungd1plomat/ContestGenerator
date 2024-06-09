using ContestGenerator.Data;
using ContestGenerator.Models;
using ContestGenerator.Models.Contest;
using ContestGenerator.Models.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContestGenerator.Controllers
{
    [Route("[controller]")]
    public class ContestController : Controller
    {
        const int chunkSize = 14;

        private readonly ApplicationDbContext _context;

        public ContestController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        [Authorize(Roles = "admin")]
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
                                                 .Include(x => x.Criterias)
                                                 .Include(x => x.Reviews).FirstOrDefaultAsync(x => x.Id == id);
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

            var files = await _context.Files.ToListAsync();
            var contestFiles = modifiedContest.Files is null ? null : files.Where(x => modifiedContest.Files.Any(f => f.Name == x.Name)).ToList();
            contest.Files = contestFiles;

            var criterias = await _context.Criterias.ToListAsync();
            var contestCriterias = modifiedContest.Criterias is null ? null : criterias.Where(x => modifiedContest.Criterias.Any(c => c.Name == x.Name)).ToList();
            contest.Criterias = contestCriterias;
            _context.Contests.Update(contest);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit");
        }

        [Authorize(Roles = "admin")]
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
                                                 .Include(x => x.Criterias)
                                                 .Include(x => x.Reviews).FirstOrDefaultAsync(x => x.Id == id);
            if (contest is null)
                return RedirectToAction("List");
            var files = await _context.Files.ToListAsync();
            var criterias = await _context.Criterias.ToListAsync();
            var editVm = new EditViewmodel()
            {
                Contest = contest,
                Files = files,
                Criterias = criterias
            };
            return View(editVm);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var errors = TempData["errors"] as IEnumerable<string>;
            if (errors != null && errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            var files = await _context.Files.ToListAsync();
            var criterias = await _context.Criterias.ToListAsync();
            var createVm = new CreateContestViewmodel()
            {
                Files = files,
                Criterias = criterias,
            };
            return View(createVm);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] Contest contest)
        {
            if (!ModelState.IsValid)
            {
                TempData["errors"] = ModelState.Values.SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();
                return RedirectToAction("Create");
            }
            if (_context.Contests.Any(x => x.Name == contest.Name))
            {
                ModelState.AddModelError(string.Empty, $"Конкурс с названием {contest.Name} уже существует");
                TempData["errors"] = ModelState.Values.SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();
                return RedirectToAction("Create");
            }
            if (contest.Files != null)
            {
                var fileIds = contest.Files.Select(x => x.Id).ToList();
                contest.Files = _context.Files.Where(x => fileIds.Contains(x.Id)).ToList();
            }
            if (contest.Criterias != null)
            {
                var criteriaIds = contest.Criterias.Select(x => x.Id).ToList();
                contest.Criterias = _context.Criterias.Where(x => criteriaIds.Contains(x.Id)).ToList();
            }
            await _context.AddAsync(contest);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create");
        }

        [Authorize(Roles = "admin, jury")]
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

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin, jury")]
        [HttpGet("{contestName}/responses")]
        public async Task<IActionResult> Responses(string contestName)
        {
            var responses = _context.Responses.Include(x => x.Contest)
                .Where(x => x.Contest.Name == contestName)
                .ToList();
            var responsesVm = new List<ResponseViewmodel>();
            foreach (var response in responses)
            {
                var responseViewmodel = new ResponseViewmodel()
                {
                    Response = response,
                };
                var evaluations = await _context.ResponseEvaluations.Where(x => x.ResponseId == response.Id)
                                                                    .SelectMany(x => x.Results)
                                                                    .Select(x => x.Evaluation)
                                                                    .ToListAsync();
                if (evaluations != null && evaluations.Any())
                {
                    var averageEvaluation = Math.Round(evaluations.Average(), 1);
                    responseViewmodel.AverageEvaluation = averageEvaluation;
                }
                responsesVm.Add(responseViewmodel);
            }
            return View("Responses", responsesVm);
        }

        [Authorize(Roles = "admin, jury")]
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

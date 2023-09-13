using ContestGenerator.Data;
using ContestGenerator.Helpers;
using ContestGenerator.Models.Contest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContestGenerator.Controllers
{
    [Route("[controller]")]
    [ForwardedHost]
    public class ContestViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger _logger;

        public ContestViewController(ApplicationDbContext context, ILogger<ContestViewController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Contest([FromHeader(Name = "X-Forwarded-Host")] string domainName)
        {
            var domain = await _context.Domains.Include(x => x.Contest.Partners)
                                               .Include(x => x.Contest.Steps)
                                               .Include(x => x.Contest.FormFields)
                                               .ThenInclude(x => x.Predefined)
                                               .Include(x => x.Contest.Helps)
                                               .Include(x => x.Contest.Nominations)
                                               .Include(x => x.Contest.PhotoUrls)
                                               .Include(x => x.Contest.Reviews).FirstOrDefaultAsync(/*x => x.DomainName == domainName*/);
            if (domain is null)
                return NotFound(domainName);
            return View(domain.Contest);
        }

        [HttpPost("send")]
        public async Task<IActionResult> GetForm([FromHeader(Name = "X-Forwarded-Host")] string domainName, IFormCollection form)
        {
            _logger.LogInformation($"Received form from {domainName} with {form}");
            var domain = await _context.Domains.Include(x => x.Contest)
                                               .FirstOrDefaultAsync(/*x => x.DomainName == domainName*/);
            if (domain is null)
                return NotFound(domainName);
            var response = new Response()
            {
                Contest = domain.Contest,
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
            return RedirectToAction("Contest");
        }

        [HttpPost("question")]
        public async Task<IActionResult> GetQuestion([FromHeader(Name = "X-Forwarded-Host")] string domainName, Question question)
        {
            _logger.LogInformation($"Received question from {domainName} with {question}");
            var domain = await _context.Domains.Include(x => x.Contest)
                                               .FirstOrDefaultAsync(x => x.DomainName == domainName);
            if (domain is null)
                return NotFound(domainName);
            question.Contest = domain.Contest;
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
            return RedirectToAction("Contest");
        }
    }
}

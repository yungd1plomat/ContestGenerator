using ContestGenerator.Abstractions;
using ContestGenerator.Data;
using ContestGenerator.Models;
using ContestGenerator.Models.Contest;
using ContestGenerator.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Nodes;

namespace ContestGenerator.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class DomainController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ICaddyApi _caddyApi;

        public DomainController(ApplicationDbContext dbContext, ICaddyApi caddyApi)
        {
            _context = dbContext;
            _caddyApi = caddyApi;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List(int page = 0)
        {
            var errData = TempData["Errors"];
            if (errData is not null)
            {
                var errors = (string[])errData;
                foreach (string err in errors)
                {
                    ModelState.AddModelError(string.Empty, err);
                }
            }
            var contests = await _context.Contests.ToListAsync();
            var domains = _context.Domains.ToList().Chunk(14).ToList();
            if (page > domains.Count - 1 && domains.Any())
                return RedirectToAction("List", "Domain");
            return View(new DomainsViewmodel()
            {
                Contests = contests,
                Domains = domains.Any() ? domains[page] : Array.Empty<Domain>(),
            });
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] AddDomainViewmodel vm)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                TempData["Errors"] = errors;
                return RedirectToAction("List");
            }

            if (_context.Domains.Any(x => x.DomainName == vm.Domain))
            {
                ModelState.AddModelError(string.Empty, $"Домен {vm.Domain} уже привязан");
            }
            
            if (_context.Domains.Any(x => x.Contest.Name == vm.Contest))
            {
                ModelState.AddModelError(string.Empty, $"Конкурс {vm.Contest} уже привязан");
            }

            var contest = _context.Contests.FirstOrDefault(x => x.Name == vm.Contest);
            if (contest is null) 
            {
                ModelState.AddModelError(string.Empty, $"Конкурс {vm.Contest} не найден");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                TempData["Errors"] = errors;
                return RedirectToAction("List");
            }

            var domain = new Domain()
            {
                Contest = contest,
                DomainName = vm.Domain,
            };
            await _context.Domains.AddAsync(domain);
            await _context.SaveChangesAsync();
            await _caddyApi.AddNewRoute(vm.Domain);
            return RedirectToAction("List");
        }

        [HttpGet("delete/{domainName}")]
        public async Task<IActionResult> Delete(string domainName)
        {
            var domain = _context.Domains.FirstOrDefault(x => x.DomainName == domainName);
            if (domain is null)
                return BadRequest($"Домен {domain} не найден");

            var config = await _caddyApi.GetConfig();
            var routes = config["apps"]["http"]["servers"]["srv0"]["routes"] as JsonArray;
            var index = routes.ToList().FindIndex(x => x["match"][0]["host"][0].ToString() == domainName);
            await _caddyApi.DeleteRoute(index);

            _context.Domains.Remove(domain);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }
    }
}

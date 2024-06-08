using ContestGenerator.Abstractions;
using ContestGenerator.Data;
using ContestGenerator.Models.Contest;
using ContestGenerator.Models.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContestGenerator.Controllers
{
    [Authorize(Roles = "admin, jury")]
    [Route("[controller]")]
    public class CriteriaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CriteriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var errors = TempData["errors"] as IEnumerable<string>;
            if (errors != null && errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            var criterias = await _context.Criterias.ToListAsync();
            return View(criterias);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AddCriteriaViewmodel criteriaVm)
        {
            if (!ModelState.IsValid)
            {
                TempData["errors"] = ModelState.Values.SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();
                return RedirectToAction("List");
            }
            var criteria = new Criteria()
            {
                Name = criteriaVm.Name,
            };
            await _context.Criterias.AddAsync(criteria);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var criteria = await _context.Criterias.FirstOrDefaultAsync(x => x.Id == id);
            if (criteria != null)
            {
                _context.Criterias.Remove(criteria);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }
    }
}

using ContestGenerator.Data;
using ContestGenerator.Models.Domain;
using ContestGenerator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContestGenerator.Models.File;
using System.Text.Json.Nodes;

namespace ContestGenerator.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "admin")]
    public class FileController : Controller
    {
        const int chunkSize = 14;

        private readonly ApplicationDbContext _context;

        public FileController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(UploadFileViewmodel fileViewmodel)
        {
            if (_context.Files.Any(x => x.Name == fileViewmodel.Name))
                ModelState.AddModelError(string.Empty, $"Файл с таким названием уже существует");
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                TempData["Errors"] = errors;
                return RedirectToAction("List");
            }
            var path = Path.Combine("files", fileViewmodel.File.FileName);
            var systemPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path);
            using (var stream = new FileStream(systemPath, FileMode.Create))
            {
                await fileViewmodel.File.CopyToAsync(stream);
            }
            await _context.Files.AddAsync(new FileModel()
            {
                Name = fileViewmodel.Name,
                Path = path,
            });
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
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
            var files = await _context.Files.ToListAsync();
            var chunkFiles = files.Chunk(chunkSize).ToList();
            if (page > chunkFiles.Count - 1 && chunkFiles.Any())
                return RedirectToAction("List", "Domain");
            var chunk = chunkFiles.Any() ? chunkFiles[page] : Array.Empty<FileModel>();
            return View(new FileListViewmodel()
            {
                Page = page,
                Files = chunk,
            });
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var file = await _context.Files.FirstOrDefaultAsync(x => x.Id == id);
            if (file is null)
                return NotFound(id);
            var systemPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.Path);
            var fileExist = System.IO.File.Exists(systemPath);
            if (fileExist)
                System.IO.File.Delete(systemPath);
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }
    }
}

using ContestGenerator.Data;
using ContestGenerator.Models;
using ContestGenerator.Models.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContestGenerator.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        private readonly ApplicationDbContext _context;

        private readonly UserManager<AppUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(SignInManager<AppUser> signInManager,
            ApplicationDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager) 
        {
            _signInManager = signInManager;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
                return RedirectToAction("Index", "Home");
            return View(new LoginViewModel());
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            ModelState.AddModelError(string.Empty, "Неверный логин или пароль.");
            return View(model);
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var errors = TempData["Errors"] as IEnumerable<string>;
            if (errors != null && errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            var userVms = new List<UserViewmodel>();
            var users = await _context.Users.ToListAsync();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userVm = new UserViewmodel() 
                {
                    User = user,
                    Roles = string.Join(",", roles)
                };
                userVms.Add(userVm);
            }
            return View(userVms);
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateUser(AddUserViewmodel viewmodel)
        {
            var isRoleExist = await _roleManager.RoleExistsAsync(viewmodel.Role);
            if (!isRoleExist)
            {
                ModelState.AddModelError(string.Empty, "Роль не существует");
                TempData["Errors"] = ModelState.Values.SelectMany(x => x.Errors)
                    .Select(y => y.ErrorMessage)
                    .ToList();
                return RedirectToAction("List");
            }
            var user = new AppUser()
            {
                UserName = viewmodel.Username,
            };
            var createResult = await _userManager.CreateAsync(user, viewmodel.Password);
            if (!createResult.Succeeded)
            {
                foreach (var error in createResult.Errors)
                {

                    ModelState.AddModelError(string.Empty, error.Description);
                }
                TempData["Errors"] = ModelState.Values.SelectMany(x => x.Errors)
                    .Select(y => y.ErrorMessage)
                    .ToList();
                return RedirectToAction("List");
            }
            await _userManager.AddToRoleAsync(user, viewmodel.Role);
            return RedirectToAction("List");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
                await _userManager.DeleteAsync(user);
            return RedirectToAction("List");
        }
    }
}
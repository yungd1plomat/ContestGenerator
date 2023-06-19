using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContestGenerator.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

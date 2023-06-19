using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContestGenerator.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class ContestController : Controller
    {
        public ContestController() 
        {

        }

        [HttpGet("test")]
        public IActionResult Index()
        {
            return Ok("hello world!!!");
        }
    }
}

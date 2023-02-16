using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("Access")]
    public class AcessController : Controller
    {
        [HttpGet(Name = "GetAccessTypes")]
        public IActionResult GetAccessTypes()
        {
            return View();
        }

        [HttpGet("{access_id}", Name = "GetAccessType")]
        public IActionResult GetAccessType(int access_id)
        {
            return View();
        }
    }
}

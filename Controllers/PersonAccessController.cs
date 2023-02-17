using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("PersonAccess")]
    public class PersonAccessController : Controller
    {
        [HttpGet("GettAllPersonAccess", Name = "GettAllPersonAccess")]
        public IActionResult GetAllPersonAccess()
        {
            return View();
        }

        [HttpGet("GetPersonAccessByID", Name = "GetPersonAccessByID")]
        public IActionResult GetPersonAccessByID()
        {
            return View();
        }

        [HttpPut("AddPersonAccess", Name = "AddPersonAccess")]
        public IActionResult AddPersonAccess()
        {
            return View();
        }

        [HttpDelete("DeletePersonAccess", Name = "DeletePersonAccess")]
        public IActionResult DeletePersonAccess()
        {
            return View();
        }
    }
}

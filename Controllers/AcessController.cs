using Microsoft.AspNetCore.Mvc;
using ProjectManagement.CRUD;
using ProjectManagement.Models;

namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("Access")]
    public class AcessController : Controller
    {
        [HttpGet(Name = "GetAccessTypes")]
        public IActionResult GetAccessTypes()
        {
            return new ObjectResult(QueryAccess.GetAll());
        }

        [HttpGet("GetAccessType/{access_id}", Name = "GetAccessType")]
        public IActionResult GetAccessType(int access_id)
        {
            return new ObjectResult(QueryAccess.GetByID(access_id));
        }

        [HttpPut("AddAccessType", Name = "AddAccessType")]
        public IActionResult AddAccessType(string accessName)
        {
            Access access = new Access()
            {
                AccessName = accessName
            };
            return new ObjectResult($"INSERT RESULT: {QueryAccess.InsertEntry(access)}");
        }
    }
}

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

        [HttpPost("AddAccessType", Name = "AddAccessType")]
        public IActionResult AddAccessType(string accessName)
        {
            Access access = new Access()
            {
                AccessName = accessName
            };
            return new ObjectResult($"INSERT RESULT: {QueryAccess.InsertEntry(access)}");
        }

        [HttpPut("SoftDeleteAccess/{access_id}", Name = "SoftDeleteAccess")]
        public IActionResult SoftDeleteAccess(int access_id)
        {
            return new ObjectResult($"DELETE RESULT: {QueryAccess.SoftDeleteEntryByID(access_id)}");
        }

        [HttpDelete("DeleteAccess/{access_id}", Name = "DeleteAccess")]
        public IActionResult DeleteAccess(int access_id)
        {
            return new ObjectResult($"DELETE RESULT: {QueryAccess.DeleteEntryByID(access_id)}");
        }

        [HttpPut("UpdateAccess/{access_id}", Name = "UpdateAccess")]
        public IActionResult UpdateAccess(int access_id, string accessName="")
        {
            Access access = new Access()
            {
                AccessName = accessName
            };
            return new ObjectResult($"UPDATE RESULT: {QueryAccess.UpdateEntryByID(access_id, access)}");
        }
    }
}

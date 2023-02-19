using Microsoft.AspNetCore.Mvc;
using ProjectManagement.CRUD;
using ProjectManagement.Model;
using ProjectManagement.Models;

namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("PersonAccess")]
    public class PersonAccessController : Controller
    {
        [HttpGet(Name = "GettAllPersonAccess")]
        public IActionResult GetAllPersonAccess()
        {
            return new ObjectResult(QueryPersonAccess.GetAll());
        }

        [HttpGet("GetPersonAccessByID/{person_access_id}", Name = "GetPersonAccessByID")]
        public IActionResult GetPersonAccessByID(int person_access_id)
        {
            return new ObjectResult(QueryPersonAccess.GetByID(person_access_id));
        }

        [HttpPost("AddPersonAccess", Name = "AddPersonAccess")]
        public IActionResult AddPersonAccess(int personID, int accessTypeID)
        {
            PersonAccess personAccess = new PersonAccess()
            {
                personID = personID,
                AccessTypeID= accessTypeID
            };
            return new ObjectResult($"ENTRY RESULT: {QueryPersonAccess.InsertEntry(personAccess)}");
        }

        [HttpPut("UpdatePersonAccess/{person_access_id}", Name = "UpdatePersonAccess")]
        public IActionResult UpdatePersonAccess(int person_access_id, int personID = 0, int accessTypeID = 0)
        {
            PersonAccess personAccess = new PersonAccess()
            {
                personID = personID,
                AccessTypeID = accessTypeID
            };
            return new ObjectResult($"UPDATE RESULT: {QueryPersonAccess.UpdateEntryByID(person_access_id, personAccess)}");
        }

        [HttpDelete("DeletePersonAccess/{person_access_id}", Name = "DeletePersonAccess")]
        public IActionResult DeletePersonAccess(int person_access_id)
        {
            return new ObjectResult($"DELETE RESULT: {QueryPersonAccess.DeleteEntryByID(person_access_id)}");
        }

        [HttpPut("SoftDeletePersonAccess/{person_access_id}", Name = "SoftDeletePersonAccess")]
        public IActionResult SoftDeletePersonAccess(int person_access_id)
        {
            return new ObjectResult($"DELETE RESULT: {QueryPersonAccess.SoftDeleteEntryByID(person_access_id)}");
        }
    }
}

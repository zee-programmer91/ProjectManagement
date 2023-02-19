using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Model;
using ProjectManagement.CRUD;

namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("Person")]
    public class PersonController : Controller
    {
        [HttpGet(Name = "GetAllPerson")]
        public IActionResult GetAllPeople()
        {
            return new ObjectResult(QueryPerson.GetAll());
        }

        [HttpGet("{person_id}", Name = "GetPerson")]
        public IActionResult GetPerson(int person_id)
        {
            return new ObjectResult(QueryPerson.GetByID(person_id));
        }

        [HttpPost("AddPerson", Name = "AddPerson")]
        public IActionResult AddPerson(string name = "", string surname = "", string identityCode = "")
        {
            Person person = new Person()
            {
                personName = name,
                personSurname = surname,
                identityCode = identityCode
            };
            return new ObjectResult($"INSERT RESULT: {QueryPerson.InsertEntry(person)}");
        }

        [HttpPut("UpdatePerson/{person_id}", Name = "UpdatePerson")]
        public IActionResult UpdatePerson(int person_id, string name = "", string surname = "", string identityCode = "")
        {
            Person person = new Person()
            {
                personName = name,
                personSurname = surname,
                identityCode = identityCode
            };
            return new ObjectResult($"INSERT RESULT: {QueryPerson.UpdateEntryByID(person_id, person)}");
        }

        [HttpPut("SoftDeletePerson/{person_id}", Name = "SoftDeletePerson")]
        public IActionResult SoftDeletePerson(int person_id)
        {
            return new ObjectResult($"DELETE RESULT: {QueryPerson.SoftDeleteEntryByID(person_id)}");
        }

        [HttpDelete("HardDeletePerson/{person_id}", Name = "HardDeletePerson")]
        public IActionResult HardDeletePerson(int person_id)
        {
            return new ObjectResult($"DELETE RESULT: {QueryPerson.DeleteEntryByID(person_id)}");
        }
    }
}

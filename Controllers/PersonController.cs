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
            return new ObjectResult(QueryPerson.GetAllPersons());
        }

        [HttpGet("{person_id}", Name = "GetPerson")]
        public IActionResult GetPerson(int id)
        {
            return new ObjectResult($"Number of rows affected: {QueryPerson.GetPersonByID(id)}");
        }

        [HttpGet("GetPersonID", Name = "GetPersonID")]
        public IActionResult GetPersonID(string name = "", string surname = "")
        {
            return new ObjectResult($"Number of rows affected: {QueryPerson.GetPersonID(name, surname)}");
        }

        [HttpPost("AddPerson", Name = "AddPerson")]
        public IActionResult AddPerson(string name = "", string surname = "", string identityCode = "")
        {
            return new ObjectResult($"Number of rows affected: {QueryPerson.AddPerson(name, surname, identityCode)}");
        }

        [HttpPut("UpdatePerson/{person_id}", Name = "UpdatePerson")]
        public IActionResult UpdatePerson(int person_id, string name = "", string surname = "")
        {
            return new ObjectResult($"Number of rows affected: {QueryPerson.UpdatePerson(person_id, name, surname)}");
        }

        [HttpPut("SoftDeletePerson/{person_id}", Name = "SoftDeletePerson")]
        public IActionResult SoftDeletePerson(int person_id)
        {
            return new ObjectResult($"Number of rows affected: {QueryPerson.SoftDeletingPerson(person_id)}");
        }

        [HttpDelete("HardDeletePerson/{person_id}", Name = "HardDeletePerson")]
        public IActionResult HardDeletePerson(int person_id)
        {
            return new ObjectResult($"Number of rows affected: {QueryPerson.HardDeletingPerson(person_id)}");
        }
    }
}

using LiveNiceApp;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Model;

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
            return new ObjectResult(QueryPerson.GetPersonByID(id));
        }

        [HttpGet("GetPersonID", Name = "GetPersonID")]
        public IActionResult GetPersonID(string name, string surname)
        {
            return new ObjectResult(QueryPerson.GetPersonID(name, surname));
        }

        [HttpPost("AddPerson", Name = "AddPerson")]
        public IActionResult AddPerson(string name, string surname, string identityCode)
        {
            return new ObjectResult(QueryPerson.AddPerson(name, surname, identityCode));
        }

        [HttpPut("UpdatePerson/{person_id}", Name = "UpdatePerson")]
        public IActionResult UpdatePerson(int person_id, string name = "", string surname = "")
        {
            return new ObjectResult(QueryPerson.UpdatePerson(person_id, name, surname));
        }

        [HttpPost("UpdatePersonSurname/{person_id}", Name = "UpdatePersonSurname")]
        public IActionResult UpdatePersonSurname(int id, string surnname)
        {
            return new ObjectResult(QueryPerson.UpdatePersonSurname(id, surnname));
        }

        [HttpPost("SoftDeletePerson/{person_id}", Name = "SoftDeletePerson")]
        public IActionResult SoftDeletePerson(int person_id)
        {
            return new ObjectResult(QueryPerson.SoftDelete(person_id));
        }

        [HttpPost("HardDeletePerson/{person_id}", Name = "HardDeletePerson")]
        public IActionResult HardDeletePerson(int person_id)
        {
            return new ObjectResult(QueryPerson.HardDelete(person_id));
        }
    }
}

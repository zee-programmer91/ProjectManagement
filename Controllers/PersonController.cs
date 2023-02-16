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

        [HttpPost("AddPerson", Name = "AddPerson")]
        public IActionResult AddPerson(string name, string surname, string identityCode)
        {
            return new ObjectResult(QueryPerson.AddPerson(name, surname, identityCode));
        }

        [HttpPost("UpdatePersonName/{person_id}", Name = "UpdatePersonName")]
        public IActionResult UpdatePersonName(int id, string name)
        {
            return new ObjectResult(QueryPerson.UpdatePersonName(id, name));
        }

        [HttpPost("UpdatePersonSurname/{person_id}", Name = "UpdatePersonSurname")]
        public IActionResult UpdatePersonSurname(int id, string surnname)
        {
            return new ObjectResult(QueryPerson.UpdatePersonSurname(id, surnname));
        }
    }
}

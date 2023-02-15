using LiveNiceApp;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Model;

namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : Controller
    {
        [HttpGet(Name = "GetAllPerson")]
        public IActionResult GetAllPerson()
        {
            return new ObjectResult(QueryPerson.GetAllPersons());
        }

        [HttpGet("{id}", Name = "GetPersonByID")]
        public IActionResult GetPerson(int id)
        {
            return new ObjectResult(QueryPerson.GetPersonByID(id));
        }

        [HttpPost("UpdatePersonNameByID", Name = "UpdatePersonName")]
        public IActionResult UpdatePersonName(int id, string name)
        {
            return new ObjectResult(QueryPerson.UpdatePersonName(id, name));
        }

        [HttpPost("UpdatePersonSurnameByID", Name = "UpdatePersonSurname")]
        public IActionResult UpdatePersonSurname(int id, string surnname)
        {
            return new ObjectResult(QueryPerson.UpdatePersonSurname(id, surnname));
        }
    }
}

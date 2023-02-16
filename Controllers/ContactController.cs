using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Queries;

namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("Contact")]
    public class ContactController : Controller
    {
        [HttpGet(Name = "GetAllContacts")]
        public IActionResult GetAllContacts()
        {
            return new ObjectResult(QueryContact.GetAllContacts());
        }

        [HttpGet("{contact_id}", Name = "GetContact")]
        public IActionResult GetContact(int id)
        {
            return new ObjectResult(QueryContact.GetContactByID(id));
        }

        [HttpPost("AddContact/{person_id}", Name = "AddContact")]
        public IActionResult AddContact(int person_id, string cellphone, string email)
        {
            return new ObjectResult(QueryContact.AddContact(person_id, cellphone, email));
        }

        [HttpPut("UpdateContact/{contact_id}", Name = "UpdateContact")]
        public IActionResult UpdateContact(int contact_id, string cellphoneNumber, string email)
        {
            return new ObjectResult(QueryContact.UpdateContact(contact_id, cellphoneNumber, email));
        }

        [HttpDelete("SoftDeleteContact/{contact_id}", Name = "SoftDeleteContact")]
        public IActionResult UpdateContact(int contact_id)
        {
            return new ObjectResult($"Number of rows affected: {QueryContact.SoftDeleteContact(contact_id)}");
        }
    }
}

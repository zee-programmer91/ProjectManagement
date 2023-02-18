using Microsoft.AspNetCore.Mvc;
using Model;
using ProjectManagement.CRUD;
using ProjectManagement.Model;

namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("Contact")]
    public class ContactController : Controller
    {
        [HttpGet(Name = "GetAllContacts")]
        public IActionResult GetAllContacts()
        {
            return new ObjectResult((List<Contact>)QueryContact.GetAll());
        }

        [HttpGet("{contact_id}", Name = "GetContact")]
        public IActionResult GetContact(int contact_id)
        {
            return new ObjectResult((Contact)QueryContact.GetByID(contact_id));
        }

        [HttpPost("AddContact/{person_id}", Name = "AddContact")]
        public IActionResult AddContact(int person_id, string cellphone, string email)
        {
            return new ObjectResult(QueryContact.InsertEntry(person_id, new Contact()));
        }

        [HttpPut("UpdateContact/{contact_id}", Name = "UpdateContact")]
        public IActionResult UpdateContact(int contact_id, string cellphoneNumber, string email)
        {
            return new ObjectResult(QueryContact.UpdateEntryByID(contact_id, new Contact()));
        }

        [HttpPut("SoftDeleteContact/{contact_id}", Name = "SoftDeleteContact")]
        public IActionResult UpdateContact(int contact_id)
        {
            return new ObjectResult(QueryContact.SoftDeleteEntryByID(contact_id));
        }

        [HttpDelete("HardDeleteContact/{contact_id}", Name = "HardDeleteContact")]
        public IActionResult HardDeleteContact(int contact_id)
        {
            return new ObjectResult(QueryContact.DeleteEntryByID(contact_id));
        }
    }
}

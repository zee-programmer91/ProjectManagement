using Microsoft.AspNetCore.Mvc;
using Model;
using ProjectManagement.CRUD;
using ProjectManagement.utlis;
using System.Diagnostics.CodeAnalysis;

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
        [SuppressMessage("Style", "IDE0090:Use 'new(...)'", Justification = "<Pending>")]
        public IActionResult AddContact(int person_id, string cellphone, string email)
        {
            ValidationMessage message = Validator.ContactInputValidated(person_id, cellphone, email);
            Console.WriteLine($"response: {message}");
            switch (message)
            {
                case ValidationMessage.Validated:
                    Contact newContact = new Contact()
                    {
                        personID = person_id,
                        cellphoneNumber = cellphone,
                        email = email
                    };
                    return new ObjectResult($"ENTRY RESULT: {QueryContact.InsertEntry(person_id, newContact)}");
                case ValidationMessage.InvalidEmail:
                    return new ObjectResult($"ENTRY RESULT: {ValidationMessage.InvalidEmail}");
                case ValidationMessage.InvalidPersonID:
                    return new ObjectResult($"ENTRY RESULT: {ValidationMessage.InvalidPersonID}");
                case ValidationMessage.InvalidCellphoneNumber:
                    return new ObjectResult($"ENTRY RESULT: {ValidationMessage.InvalidCellphoneNumber}");
            }
            return new ObjectResult($"ENTRY RESULT: {"Invalid Input"}");
        }

        [HttpPut("UpdateContact/{contact_id}", Name = "UpdateContact")]
        public IActionResult UpdateContact(int contact_id, string cellphoneNumber, string email)
        {
            return new ObjectResult(QueryContact.UpdateEntryByID(contact_id, new Contact()));
        }

        [HttpPut("SoftDeleteContact/{contact_id}", Name = "SoftDeleteContact")]
        public IActionResult SoftDeleteEntryByID(int contact_id)
        {
            return new ObjectResult($"DELETE RESULT: {QueryContact.SoftDeleteEntryByID(contact_id)}");
        }

        [HttpDelete("HardDeleteContact/{contact_id}", Name = "HardDeleteContact")]
        public IActionResult HardDeleteContact(int contact_id)
        {
            return new ObjectResult($"DELETE RESULT: {QueryContact.DeleteEntryByID(contact_id)}");
        }
    }
}

﻿using LiveNiceApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Models;

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

        [HttpGet("{id}", Name = "GetContact")]
        public IActionResult GetContact(int id)
        {
            return new ObjectResult(QueryContact.GetContactByID(id));
        }

        [HttpPost("AddContact/{person_id}", Name = "AddContact")]
        public IActionResult AddContact(int person_id, string cellphone, string email)
        {
            return new ObjectResult(QueryContact.AddContact(person_id, cellphone, email));
        }

        [HttpPost("UpdateEmail/{contact_id}", Name = "UpdateEmail")]
        public IActionResult UpdateEmail(int id, string email)
        {
            return new ObjectResult(QueryContact.UpdateContactEmail(id, email));
        }

        [HttpPost("UpdateCellphone/{contact_id}", Name = "UpdateCellphone")]
        public IActionResult UpdateCellphone(int id, string cellphone)
        {
            return new ObjectResult(QueryContact.UpdateContactCellphone(id, cellphone));
        }
    }
}

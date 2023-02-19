using LiveNiceApp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;
using ProjectManagement.Model;
using ProjectManagement.Models;
using System.Collections;
using System.Xml.Linq;

namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("Visit")]
    public class VisitController : Controller
    {
        
        [HttpGet(Name = "GetAllVisitors")]
        public IActionResult GetAllVisits()
        {
            return new ObjectResult(QueryVisit.GetAll());
        }

        [HttpGet("{visit_id}", Name = "GetVisit")]
        public IActionResult GetVisitOfTenant(int visit_id)
        {
            return new ObjectResult(QueryVisit.GetByID(visit_id));
        }

        [HttpPost("AddVisit/{tenant_id}", Name = "AddVisit")]
        public IActionResult AddVisit(string name, string surname, string identityCode, string email, string cellphone, int tenant_id, DateTime dateOfVisit)
        {
            Person person= new Person()
            {
                personName= name,
                personSurname= surname,
                identityCode=identityCode,
            };
            Contact contact = new Contact()
            {
                email=email,
                cellphoneNumber=cellphone,
            };
            Visit visit = new Visit()
            {
                dateOfVisit=dateOfVisit,
                tenantID=tenant_id, 
            };
            ArrayList vistList = new ArrayList
            {
                person,
                contact,
                visit,
            };
            return new ObjectResult($"INSERT RESULT: {QueryVisit.InsertEntry(vistList)}");
        }

        [HttpPut("UpdateVisit/{visit_id}", Name = "UpdateVisit")]
        public IActionResult UpdateVisit(int visit_id, string name, string surname, string identityCode, string email, string cellphone, int tenant_id, DateTime dateOfVisit)
        {
            Person person = new Person()
            {
                personName = name,
                personSurname = surname,
                identityCode = identityCode,
            };
            Contact contact = new Contact()
            {
                email = email,
                cellphoneNumber = cellphone,
            };
            Visit visit = new Visit()
            {
                dateOfVisit = dateOfVisit,
            };
            ArrayList vistList = new ArrayList
            {
                person,
                contact,
                visit
            };

            return new ObjectResult($"UPDATE RESULT: {QueryVisit.UpdateEntryByID(visit_id, vistList)}");
        }

        [HttpPut("SoftDeleteVisit/{visit_id}", Name = "SoftDeleteVisit")]
        public IActionResult SoftDeleteVisit(int visit_id)
        {
            return new ObjectResult($"DELETE RESULT: {QueryVisit.SoftDeleteEntryByID(visit_id)}");
        }

        [HttpDelete("DeleteVisit/{visit_id}", Name = "DeleteVisit")]
        public IActionResult DeleteVisit(int visit_id)
        {
            return new ObjectResult($"DELETE RESULT: {QueryVisit.DeleteEntryByID(visit_id)}");
        }
    }
}

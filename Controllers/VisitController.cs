using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Models;

namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("Visit")]
    public class VisitController : Controller
    {
        
        [HttpGet(Name = "GetAllVisitors")]
        public IActionResult GetAllVisits()
        {
            return new ObjectResult(QueryVisit.GetVisits());
        }

        [HttpGet("{id}", Name = "GetVisit")]
        public IActionResult GetVisitsOfTenant(int id)
        {
            return new ObjectResult(QueryVisit.GetVisitByID(id));
        }

        [HttpPost("AddVisit/{tenant_id}", Name = "AddVisit")]
        public IActionResult AddVisit(string name, string surname, string identityCode, string email, string cellphone, int tenant_id)
        {
            return new ObjectResult(QueryVisit.AddVisit(name, surname, identityCode, email, cellphone, tenant_id));
        }

        [HttpPost("UpdateDateOfVisit/{id}", Name = "UpdateDateOfVisit")]
        public IActionResult UpdateDateOfVisit(int id, DateOnly date)
        {
            return View();
        }

        //    [HttpPost("UpdateCellphone", Name = "UpdateCellphone")]
        //    public IActionResult UpdateCellphone(int contact_id, string cellphone)
        //    {
        //        return new ObjectResult(QueryContact.UpdateContactCellphone(contact_id, cellphone));
        //    }
    }
}

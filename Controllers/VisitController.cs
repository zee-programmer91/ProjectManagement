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

        [HttpGet("{visit_id}", Name = "GetVisit")]
        public IActionResult GetVisitsOfTenant(int visit_id)
        {
            return new ObjectResult(QueryVisit.GetVisitByID(visit_id));
        }

        [HttpPost("AddVisit/{tenant_id}", Name = "AddVisit")]
        public IActionResult AddVisit(string name, string surname, string identityCode, string email, string cellphone, int tenant_id, DateTime dateOfVisit)
        {
            
            int numberOfRowsAffected = QueryVisit.AddVisit(name, surname, identityCode, email, cellphone, tenant_id, dateOfVisit);
            string resultString = $"Saved visit of person into VISIT table\n";
            resultString += $"Number of rows affected: {numberOfRowsAffected}";

            if (numberOfRowsAffected > 0)
            {
                return new ObjectResult(resultString);
            }
            resultString = $"Could not add visit to the tenant with the following ID: {tenant_id}\n";
            resultString += $"Number of rows affected: {numberOfRowsAffected}";

            return new ObjectResult(resultString);
        }

        [HttpPut("UpdateDateOfVisit/{visit_id}", Name = "UpdateDateOfVisit")]
        public IActionResult UpdateDateOfVisit(int visit_id, DateTime date)
        {
            int numberOfRowsAffected = QueryVisit.UpdateDateOfVisit(visit_id, date);
            string resultString = $"Updated row with ID {visit_id} IN VISIT TABLE\n";
            resultString += $"Number of rows affected: {numberOfRowsAffected}";

            if (numberOfRowsAffected > 0)
            {
                return new ObjectResult(resultString);
            }
            resultString = $"Could not update VISIT table on the row with ID {visit_id}\n";
            resultString += $"Number of rows affected: {numberOfRowsAffected}";

            return new ObjectResult(resultString);
        }
    }
}

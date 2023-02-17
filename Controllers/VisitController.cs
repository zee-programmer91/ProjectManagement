using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Models;
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
            int numberOfRowsAffected = QueryVisit.GetVisits();
            string resultString = $"Retrieved {numberOfRowsAffected} visit entries from the VISIT table\n";
            resultString += $"Number of rows affected: {numberOfRowsAffected}";

            if (numberOfRowsAffected > 0)
            {
                return new ObjectResult(resultString);
            }
            resultString = $"Could not get any visits\n";
            resultString += $"Number of rows affected: {numberOfRowsAffected}";

            return new ObjectResult(resultString);
        }

        [HttpGet("{visit_id}", Name = "GetVisit")]
        public IActionResult GetVisitOfTenant(int visit_id)
        {
            int numberOfRowsAffected = QueryVisit.GetVisitByID(visit_id);
            string resultString = $"Retrieved {numberOfRowsAffected} visit entry from the VISIT table\n";
            resultString += $"Number of rows affected: {numberOfRowsAffected}";

            if (numberOfRowsAffected > 0)
            {
                return new ObjectResult(resultString);
            }
            resultString = $"Could not get the visit of the ID {visit_id}\n";
            resultString += $"Number of rows affected: {numberOfRowsAffected}";

            return new ObjectResult(resultString);
        }

        [HttpPost("AddVisit/{tenant_id}", Name = "AddVisit")]
        public IActionResult AddVisit(string name, string surname, string identityCode, string email, string cellphone, int tenant_id, DateTime dateOfVisit)
        {
            int numberOfRowsAffected = QueryVisit.AddVisit(name, surname, identityCode, email, cellphone, tenant_id, dateOfVisit);
            string resultString = $"Saved visit of person to tenant with ID {tenant_id} into VISIT table\n";
            resultString += $"Number of rows affected: {numberOfRowsAffected}";

            if (numberOfRowsAffected > 0)
            {
                return new ObjectResult(resultString);
            }
            resultString = $"Could not add visit to the tenant with the following ID: {tenant_id}\n";
            resultString += $"Number of rows affected: {numberOfRowsAffected}";

            return new ObjectResult(resultString);
        }

        [HttpPut("UpdateVisit/{visit_id}", Name = "UpdateVisit")]
        public IActionResult UpdateVisit(int visit_id, DateTime dateOfVisit, DateTime dateLeftVisit)
        {
            int numberOfRowsAffected = QueryVisit.UpdateVisit(visit_id, dateOfVisit, dateLeftVisit);
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

        [HttpPut("SoftDeleteVisit/{visit_id}", Name = "SoftDeleteVisit")]
        public IActionResult SoftDeleteVisit(int visit_id, DateTime date)
        {
            int numberOfRowsAffected = QueryVisit.SoftDeleteVisit(visit_id);
            string resultString = $"Updated row with ID {visit_id} IN VISIT TABLE\n";
            resultString += $"Number of rows affected: {numberOfRowsAffected}";

            if (numberOfRowsAffected > 0)
            {
                return new ObjectResult(resultString);
            }
            resultString = $"Could not delete visit with ID {visit_id} from the VISIT table\n";
            resultString += $"Number of rows affected: {numberOfRowsAffected}";

            return new ObjectResult(resultString);
        }
    }
}

using Microsoft.AspNetCore.Http;
using ProjectManagement.Model;
using System.Text;

namespace ProjectManagement.utlis
{
    public static class UpdateCreator
    {
        public static string CreatePersonUpdateQuery(Person person)
        {
            string updateQuery = "UPDATE PERSON ";

            if (person.PersonName == "" && person.PersonSurname == "")
            {
                return "";
            }

            if (person.PersonName != "")
            {
                updateQuery += $"SET person_name = {person.PersonName}";
            }
            if (person.PersonSurname != "" && person.PersonName != "")
            {
                updateQuery += $", person_surname = {person.personSurname}";
            } else
            {
                updateQuery += $"SET person_surname = {person.personSurname}";
            }

            updateQuery += $" WHERE person_id = {person.PersonId}";
            return updateQuery;
        }

        public static string CreateVisitUpdateQuery(DateTime dateOfVisit, DateTime dateLeftVisit, int visit_id)
        {
            const string dateTimeDefaultDate = "0001/01/01 00:00:00";
            string defaultReturnValue = "";

            //  All possible update queries to the VISIT table
            string updateBoth = $"UPDATE VISIT SET DATE_OF_VISIT = {dateOfVisit.Date}, " +
                $"SET DATE_LEFT_VISIT = {dateLeftVisit.Date} WHERE VISIT_ID = {visit_id};";

            Console.WriteLine($"dateOfVisit.Date : {dateOfVisit.Date}");
            string updateDateOfVisit = $"UPDATE VISIT SET DATE_OF_VISIT = {dateOfVisit.Date} WHERE VISIT_ID = {visit_id};";
            string updateDateLeftVisit = $"UPDATE VISIT SET DATE_LEFT_VISIT = {dateLeftVisit.Date} WHERE VISIT_ID = {visit_id};";

            // First check if both the years match the default year
            bool dateOfVisitMatches = dateOfVisit.ToString() == dateTimeDefaultDate;
            Console.WriteLine($"dateOfVisitMatches: {dateOfVisitMatches}");
            bool dateLeftVisitMatches = dateLeftVisit.ToString() == dateTimeDefaultDate;
            Console.WriteLine($"dateLeftVisitMatches: {dateLeftVisit.Year} - {dateLeftVisitMatches}");

            //  Now match the results to see what string to return
            if (!dateOfVisitMatches && !dateLeftVisitMatches) // Both A and B don't match the default year
            {
                return updateBoth;
            }
            
            else if (!dateOfVisitMatches && dateLeftVisitMatches) // A doesn't match but B matches
            {
                return updateDateOfVisit;
            }
            
            else if (dateOfVisitMatches && !dateLeftVisitMatches) // A matches but B doesn't match
            {
                return updateDateLeftVisit;
            }

            return defaultReturnValue; // both of them match
        }
    }
}

using LiveNiceApp;
using Npgsql;
using ProjectManagement.Database;
using ProjectManagement.CRUD;
using ProjectManagement.utlis;
using Model;
using ProjectManagement.Model;
using static ProjectManagement.Database.DatabaseActions;
using System.Collections;

namespace ProjectManagement.Models
{
    public class QueryVisit : DatabaseActionsBridge
    {
        private static int GetVisitID(int personID, int tenantID)
        {
            int visitID = 0;

            try
            {
                string commandText = $"SELECT VISIT.visit_id FROM VIST WHERE person_id = @personID and tenat_id = @tenantID;";
                using NpgsqlCommand cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("personID", personID);
                cmd.Parameters.AddWithValue("tenantID", tenantID);

                visitID = (int)cmd.ExecuteScalar();
                Console.WriteLine($"Found the ID of tenant {tenantID} and visitor {personID}");
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not get Visit with PersonID '{personID}' and tenant '{tenantID}'");
            }

            return visitID;
        }

        public static new Visit GetByID(int ID)
        {
            try
            {
                string commandText = $"SELECT * FROM VISIT WHERE VISIT_ID = @visitID;";
                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());

                cmd.Parameters.AddWithValue("visitID", ID);

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Visit visit = DatabaseReaders.ReadVisit(reader);
                    Console.WriteLine($"Selected visit with visitID {ID} from the VISIT Table");

                    return visit;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR - Could not get visit: {e.Message}");
            }

            return new Visit();
        }

        public static new List<Visit> GetAll()
        {
            List<Visit> visits = new();

            try
            {
                string commandText = $"SELECT * FROM VISIT;";
                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Visit visit = DatabaseReaders.ReadVisit(reader);
                    visits.Add(visit);
                }
                Console.WriteLine($"Selected all visits from the VIST Table");

                return visits;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not get all visits");
            }

            return visits;
        }

        public static new DatabaseActionsResponses InsertEntry(object newEntry)
        {
            int result = 0;
            ArrayList visitList= (ArrayList)newEntry;

            Person person = (Person)visitList[0];
            Contact contact = (Contact)visitList[1];
            Visit visit = (Visit)visitList[2];

            try
            {
                //  Save Visitor's personal details
                QueryPerson.InsertEntry(person);
                int personID = QueryPerson.GetPersonID(person.PersonName, person.PersonSurname);
                //  Save Visitor's contact details
                contact.personID = personID;
                QueryContact.InsertEntry(contact);
                visit.PersonID = personID;
                //  Save Visit details

                string commandText = $"INSERT INTO VISIT (person_id, date_of_visit, tenant_id) VALUES(@personID, @dateOfVisit, @tenantID);";
                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());

                cmd.Parameters.AddWithValue("personID", visit.personID);
                cmd.Parameters.AddWithValue("dateOfVisit", visit.dateOfVisit);
                cmd.Parameters.AddWithValue("tenantID", visit.tenantID);

                result = cmd.ExecuteNonQuery();
                Console.WriteLine($"Saved visit to tenant with ID {visit.tenantID} INTO the VISIT table");
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not add visit to the room table for the tenant with the ID {visit.tenantID}");
            }
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }

        public static new DatabaseActionsResponses DeleteAll()
        {
            return DatabaseActionsResponses.Success;
        }

        public static new DatabaseActionsResponses SoftDeleteEntryByID(int ID)
        {
            int result = 0;

            try
            {
                string commandText = $"UPDATE VISIT SET deleted = CAST(1 AS bit) WHERE visit_id = @visitID;";

                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("visitID", ID);

                result = (int)cmd.ExecuteNonQuery();
                Console.WriteLine($"Soft Deleted VISIT WITH ID {ID} IN VISIT TABLE");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine($"ERROR - Visit with VisitID {ID} could not be deleted");
            }
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }

        public static new DatabaseActionsResponses DeleteEntryByID(int ID)
        {
            int result = 0;

            try
            {
                string commandText = $"DELETE FROM VISIT WHERE visit_id = @visitID;";

                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("visitID", ID);

                result = (int)cmd.ExecuteNonQuery();
                Console.WriteLine($"Hard Deleted Visit WITH ID {ID} IN VISIT TABLE");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine($"ERROR - Visit with visitID {ID} could not be deleted");
            }
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }

        public static new DatabaseActionsResponses UpdateEntryByID(int ID, object updateEntry)
        {
            int result = 0;

            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }
    }
}

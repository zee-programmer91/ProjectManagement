using LiveNiceApp;
using Npgsql;
using ProjectManagement.Database;
using ProjectManagement.Readers;

namespace ProjectManagement.Models
{
    internal class QueryVisit
    {
        private static readonly DatabaseConnection databaseConnection = new DatabaseConnection();

        public static Visit[] GetVisitByID(int visit_id)
        {
            databaseConnection.OpenConnection();
            List<Visit> visits = new();

            try
            {
                string commandText = $"SELECT * FROM VISIT WHERE visit_id = @visit_id;";
                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                cmd.Parameters.AddWithValue("visit_id", visit_id);

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Visit visit = DatabaseReaders.ReadVisit(reader);
                    databaseConnection.DisposeConnection();
                    visits.Add(visit);
                    return visits.ToArray();
                }
                Console.WriteLine($"Selected a visit with the ID {visit_id} from the Visit table");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not get visit");
            }

            databaseConnection.DisposeConnection();
            return visits.ToArray();
        }

        public static Visit[] GetVisits()
        {
            databaseConnection.OpenConnection();
            List<Visit> visits = new();

            try
            {
                string commandText = $"SELECT * FROM VISIT;";
                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Visit visit = DatabaseReaders.ReadVisit(reader);
                    visits.Add(visit);
                }
                Console.WriteLine($"Selected a visit with the ID from the Visit table");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not get all visits");
            }

            databaseConnection.DisposeConnection();
            return visits.ToArray();
        }

        public static Visit[] AddVisit(string name, string surname, string identityCode, string email, string cellphone, int tenant_id, DateOnly dateOfVisit)
        {
            try
            {
                QueryPerson.AddPerson(name, surname, identityCode);
                int person_id = QueryPerson.GetPersonID(name, surname);
                QueryContact.AddContact(person_id, cellphone, email);

                databaseConnection.OpenConnection();
                string commandText = $"INSERT INTO VISIT (person_id, tenant_id, date_of_visit) VALUES(@person_id,@tenant_id, @dateOfVisit);";

                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("person_id", person_id);
                cmd.Parameters.AddWithValue("tenant_id", tenant_id);
                cmd.Parameters.AddWithValue("dateOfVisit", dateOfVisit);

                cmd.ExecuteNonQuery();
                Console.WriteLine($"Saved contact of person with ID {person_id} INTO CONTACT table");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not add visit to the tenant with the following ID: {tenant_id}");
            }

            databaseConnection.DisposeConnection();
            return GetVisits();
        }

        //        public static void DeleteContact(Contact contact) { }

        //        public static Contact[] UpdateContactEmail(int id, string email)
        //        {
        //            databaseConnection.OpenConnection();
        //            List<Contact> contacts = new();

        //            try
        //            {
        //                string commandText = $@"UPDATE CONTACT SET email = @email WHERE contact_id = @id;";

        //                using (var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection()))
        //                {
        //                    cmd.Parameters.AddWithValue("email", email);
        //                    cmd.Parameters.AddWithValue("id", id);

        //                    cmd.ExecuteNonQuery();
        //                    Console.WriteLine($"UPDATED CONTACT EMAIL WITH ID {id} IN CONTACT TABLE");
        //                    databaseConnection.DisposeConnection();
        //                    contacts.Add(GetContactByID(id)[0]);
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                Console.WriteLine(e.Message);
        //                Console.WriteLine($"ERROR - Could not update CONTACT table with id {id}");
        //            }

        //            databaseConnection.DisposeConnection();
        //            return contacts.ToArray();
        //        }

        //        public static Contact[] UpdateContactCellphone(int id, string cellphone)
        //        {
        //            databaseConnection.OpenConnection();
        //            List<Contact> contacts = new();

        //            try
        //            {
        //                string commandText = $@"UPDATE CONTACT SET cellphone_number = @cellphone WHERE contact_id = @id;";

        //                using (var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection()))
        //                {
        //                    cmd.Parameters.AddWithValue("cellphone", cellphone);
        //                    cmd.Parameters.AddWithValue("id", id);

        //                    cmd.ExecuteNonQuery();
        //                    Console.WriteLine($"UPDATED CONTACT Cellphone WITH ID {id} IN CONTACT TABLE");
        //                    databaseConnection.DisposeConnection();
        //                    contacts.Add(GetContactByID(id)[0]);
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                Console.WriteLine(e.Message);
        //                Console.WriteLine($"ERROR - Could not update CONTACT table with id {id}");
        //            }

        //            databaseConnection.DisposeConnection();
        //            return contacts.ToArray();
        //        }

        //        private static Contact ReadContact(NpgsqlDataReader reader)
        //        {

        //            var tempId = reader["person_id"];
        //            int id = 0;

        //            switch (tempId != null)
        //            {
        //                case true:
        //                    id = (int)tempId;
        //                    break;
        //                case false:
        //                    return new Contact();
        //            }

        //            var tempPersonID = reader["person_id"] as int?;
        //            int personID;

        //            switch (tempPersonID != null)
        //            {
        //                case true:
        //                    personID = (int)tempPersonID;
        //                    break;
        //                case false:
        //                    personID = 0;
        //                    break;
        //            }

        //            var tempEmail = reader["email"] as string;
        //            string email;

        //            switch (tempEmail != null)
        //            {
        //                case true:
        //                    email = tempEmail;
        //                    break;
        //                case false:
        //                    email = "";
        //                    break;
        //            }

        //            var tempCellphoneNumber = reader["cellphone_number"] as string;
        //            string cellphoneNumber;

        //            switch (tempCellphoneNumber != null)
        //            {
        //                case true:
        //                    cellphoneNumber = tempCellphoneNumber;
        //                    break;
        //                case false:
        //                    cellphoneNumber = "";
        //                    break;
        //            }

        //            Contact contact = new()
        //            {
        //                contactID = id,
        //                personID = personID,
        //                email = email,
        //                cellphoneNumber = cellphoneNumber,
        //            };

        //            return contact;
        //        }
        //    }
    }
}

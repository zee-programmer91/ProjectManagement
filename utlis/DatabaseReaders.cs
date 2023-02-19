using LiveNiceApp;
using Model;
using Npgsql;
using ProjectManagement.Model;

namespace ProjectManagement.utlis
{
    public class DatabaseReaders
    {
        public static Contact ReadContact(NpgsqlDataReader reader)
        {
            int id = (int)reader["contact_id"];

            int personID = (int)reader["person_id"];

            string email = reader["email"] as string;

            string cellphoneNumber = reader["cellphone_number"] as string;

            bool deleted = (bool)reader["deleted"];

            Contact contact = new()
            {
                contactID = id,
                personID = personID,
                email = email,
                cellphoneNumber = cellphoneNumber,
                deleted = deleted
            };

            return contact;
        }

        public static Person ReadPerson(NpgsqlDataReader reader)
        {

            int id = (int)reader["person_id"];

            string? name = reader["person_name"] as string;

            string surname = reader["person_surname"] as string;

            string identity = reader["identity_code"] as string;

            bool deleted = (bool)reader["deleted"];

            Person person = new()
            {
                personId = id,
                personName = name,
                personSurname = surname,
                identityCode = identity,
                deleted = deleted
            };

            return person;
        }

        public static Visit ReadVisit(NpgsqlDataReader reader)
        {

            int id = (int)reader["visit_id"];

            int personID = (int)reader["person_id"];

            int tenantID = (int)reader["tenant_id"];

            DateTime dateOfVisit = (DateTime)reader["date_of_visit"];

            Visit visit = new()
            {
                visitorID = id,
                personID = personID,
                tenantID = tenantID,
                dateOfVisit = dateOfVisit,
            };

            return visit;
        }
    }
}

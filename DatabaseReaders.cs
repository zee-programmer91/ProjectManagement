using LiveNiceApp;
using Npgsql;
using ProjectManagement.Model;

namespace ProjectManagement.Readers
{
    public class DatabaseReaders
    {
        public static Contact ReadContact(NpgsqlDataReader reader)
        {

            var tempId = reader["person_id"];
            int id = 0;

            switch (tempId != null)
            {
                case true:
                    id = (int)tempId;
                    break;
                case false:
                    return new Contact();
            }

            var tempPersonID = reader["person_id"] as int?;
            int personID;

            switch (tempPersonID != null)
            {
                case true:
                    personID = (int)tempPersonID;
                    break;
                case false:
                    personID = 0;
                    break;
            }

            var tempEmail = reader["email"] as string;
            string email;

            switch (tempEmail != null)
            {
                case true:
                    email = tempEmail;
                    break;
                case false:
                    email = "";
                    break;
            }

            var tempCellphoneNumber = reader["cellphone_number"] as string;
            string cellphoneNumber;

            switch (tempCellphoneNumber != null)
            {
                case true:
                    cellphoneNumber = tempCellphoneNumber;
                    break;
                case false:
                    cellphoneNumber = "";
                    break;
            }

            Contact contact = new()
            {
                contactID = id,
                personID = personID,
                email = email,
                cellphoneNumber = cellphoneNumber,
            };

            return contact;
        }

        public static Person ReadPerson(NpgsqlDataReader reader)
        {

            var tempId = reader["person_id"];
            int id = 0;

            switch (tempId != null)
            {
                case true:
                    id = (int)tempId;
                    break;
                case false:
                    return new Person();
            }

            var tempName = reader["person_name"] as string;
            string name;

            switch (tempName != null)
            {
                case true:
                    name = tempName;
                    break;
                case false:
                    name = "";
                    break;
            }

            var tempSurname = reader["person_surname"] as string;
            string surname;

            switch (tempSurname != null)
            {
                case true:
                    surname = tempSurname;
                    break;
                case false:
                    surname = "";
                    break;
            }

            var tempIdentity = reader["identity_code"] as string;
            string identity;

            switch (tempIdentity != null)
            {
                case true:
                    identity = tempIdentity;
                    break;
                case false:
                    identity = "";
                    break;
            }

            Person person = new()
            {
                personId = id,
                personName = name,
                personSurname = surname,
                identityCode = identity,
            };

            return person;
        }

        public static Visit ReadVisit(NpgsqlDataReader reader)
        {

            var tempId = reader["visit_id"];
            int id = 0;

            switch (tempId != null)
            {
                case true:
                    id = (int)tempId;
                    break;
                case false:
                    return new Visit();
            }

            var tempPersonID = reader["person_id"] as int?;
            int personID;

            switch (tempPersonID != null)
            {
                case true:
                    personID = (int)tempPersonID;
                    break;
                case false:
                    personID = 0;
                    break;
            }

            var temptenantID = reader["tenant_id"] as int?;
            int tenantID;

            switch (temptenantID != null)
            {
                case true:
                    tenantID = (int)temptenantID;
                    break;
                case false:
                    tenantID = 0;
                    break;
            }

            var tempDateOfVisit = reader["date_of_visit"] as DateOnly?;
            DateOnly dateOfVisit;

            switch (tempDateOfVisit != null)
            {
                case true:
                    dateOfVisit = (DateOnly)tempDateOfVisit;
                    break;
                case false:
                    dateOfVisit = new();
                    break;
            }

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

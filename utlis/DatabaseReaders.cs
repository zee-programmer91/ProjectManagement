using LiveNiceApp;
using Model;
using Npgsql;
using ProjectManagement.Model;
using ProjectManagement.Models;

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

        public static Access ReadAccess(NpgsqlDataReader reader)
        {
            int id = (int)reader["access_type_id"];

            string? access_type_name = reader["access_type_name"] as string;

            bool deleted = (bool)reader["deleted"];

            Access access = new()
            {
                accessID = id,
                accessName = access_type_name,
                deleted = deleted
            };

            return access;
        }

        public static Visit ReadVisit(NpgsqlDataReader reader)
        {

            int id = (int)reader["visit_id"];

            int personID = (int)reader["person_id"];

            int tenantID = (int)reader["tenant_id"];

            DateTime dateOfVisit = (DateTime)reader["date_of_visit"];

            DateTime dateLeftVisit = (DateTime)reader["date_left_visit"];

            bool deleted = (bool)reader["deleted"];

            Visit visit = new()
            {
                visitID = id,
                personID = personID,
                tenantID = tenantID,
                dateOfVisit = dateOfVisit,
                dateLeftVisit= dateLeftVisit,
                deleted= deleted
            };

            return visit;
        }

        public static PersonAccess ReadPersonAccess(NpgsqlDataReader reader)
        {

            int id = (int)reader["person_access_id"];

            int personID = (int)reader["person_id"];

            int accessID = (int)reader["access_id"];

            bool deleted = (bool)reader["deleted"];

            PersonAccess personAccess = new()
            {
                personAccessTypeID= id,
                personID= personID,
                accessTypeID= accessID,
                deleted = deleted
            };

            return personAccess;
        }

        public static Room ReadRoom(NpgsqlDataReader reader)
        {

            int id = (int)reader["room_id"];

            int roomNumber = (int)reader["room_number"];

            int tenantID = (int)reader["tenant_id"];

            bool hasRoomAccess = (bool)reader["has_room_access"];

            DateTime fromDate = (DateTime)reader["from_date"];
            DateTime toDate = (DateTime)reader["to_date"];

            bool deleted = (bool)reader["deleted"];

            Room room = new()
            {
                roomID= id,
                roomNumber= roomNumber,
                tenantID= tenantID,
                hasRoomAccess= hasRoomAccess,
                fromDate= fromDate,
                toDate= toDate,
                deleted= deleted
            };

            return room;
        }
    }
}

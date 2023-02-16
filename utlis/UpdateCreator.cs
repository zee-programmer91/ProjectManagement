using ProjectManagement.Model;

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
    }
}

using ProjectManagement.CRUD;
using ProjectManagement.Model;

namespace ProjectManagement.utlis
{
    public enum ValidationMessage
    {
        InvalidEmail,
        InvalidPersonID,
        InvalidCellphoneNumber,
        Validated
    }

    public class Validator
    {
        public static ValidationMessage ContactInputValidated(int person_id, string cellphone, string email)
        {
            // Check if person with the id given exists
            Console.WriteLine($"email.Contains(\".com\"): {email.Contains(".com")}");
            Person person = QueryPerson.GetPersonByID(person_id);
            if (person.personName == "")
            {
                return ValidationMessage.InvalidPersonID;
            }
            //  Check if cellphone number valid
            if ((cellphone.Length != 10 && !cellphone.Contains('+')) || (cellphone.Contains('+') && cellphone.Length != 12))
            {
                return ValidationMessage.InvalidCellphoneNumber;
            }
            // Check if email number includes @, co.za, org, com
            if (email.Contains('@') && email.Contains(".co.za"))
            {
                return ValidationMessage.Validated;
            } else if (email.Contains('@') && email.Contains(".org"))
            {
                return ValidationMessage.Validated;
            }
            else if (email.Contains('@') && email.Contains(".com"))
            {
                return ValidationMessage.Validated;
            }

            return ValidationMessage.InvalidEmail;
        }
    }
}

using Npgsql;
using System;

namespace LiveNiceApp
{
    public class Contact
    {
        public int contactID = 0;
        public int personID = 0;
        public string email = "";
        public string cellphoneNumber = "";

        public int ContactID
        {
            get { return contactID; }
            set { contactID = value; }
        }
        public int PersonID
        { 
            get { return personID; }
            set { personID = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string CellphoneNumber
        {
            get { return cellphoneNumber; }
            set { cellphoneNumber = value; }
        }

        public Contact() { }
    }
}

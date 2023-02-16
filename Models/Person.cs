
namespace ProjectManagement.Model
{
    public class Person
    {
        public int personId = 0;
        public string personName = "";
        public string personSurname = "";
        public string identityCode = "";
        public bool deleted = false;

        public int PersonId {
            get { return personId; } set { personId = value; }
        }

        public string PersonName
        {
            get { return personName; }
            set { personName = value; }
        }
        public string PersonSurname
        {
            get { return personSurname; }
            set { personSurname = value; }
        }
        public string IdentityCode
        {
            get { return identityCode; }
            set { identityCode = value; }
        }

        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }

        public Person() {}
    }
}

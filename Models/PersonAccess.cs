namespace ProjectManagement.Models
{
    public class PersonAccess
    {
        public int personAccessType = 0;
        public int personID = 0;
        public int accessTypeID = 0;
        public int PersonAccessType
        {
            get { return personAccessType; }
            set { personAccessType = value; }
        }
        public int PersonID
        {
            get { return personID; }
            set { personID = value; }
        }
        public int AccessTypeID
        {
            get { return accessTypeID; }
            set { accessTypeID = value; }
        }
    }
}

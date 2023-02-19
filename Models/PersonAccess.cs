namespace ProjectManagement.Models
{
    public class PersonAccess
    {
        public int personAccessTypeID = 0;
        public int personID = 0;
        public int accessTypeID = 0;
        public bool deleted = false;
        public int PersonAccessTypeID
        {
            get { return personAccessTypeID; }
            set { personAccessTypeID = value; }
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

        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }
    }
}

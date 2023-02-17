namespace ProjectManagement.Models
{
    public class Access
    {
        public int accessID = 0;
        public string accessName = "";
        public bool deleted = false;

        public int AccessID
        {
            get { return accessID; }
            set { accessID = value; }
        }
        public string AccessName
        {
            get { return accessName; }
            set { accessName = value; }
        }
        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }

        public Access() { }
    }
}

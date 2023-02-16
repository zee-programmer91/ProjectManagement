namespace ProjectManagement.Models
{
    public class Access
    {
        public int accessID = 0;
        public string accessName = "";

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

        public Access() { }
    }
}

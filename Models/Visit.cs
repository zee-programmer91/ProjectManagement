using System;

namespace LiveNiceApp
{
    public class Visit
    {
        public int visitID = 0;
        public int personID = 0;
        public int tenantID = 0;
        public DateTime dateOfVisit = new();
        public bool deleted = false;
        public DateTime dateLeftVisit = new();

        public int VisitID
        {
            get { return visitID; }
            set { visitID = value; }
        }
        public int PersonID
        {
            get { return personID; }
            set { personID = value; }
        }
        public int TenantID
        {
            get { return tenantID; }
            set { tenantID = value; }
        }
        public DateTime DateOfVisit
        {
            get { return dateOfVisit; }
            set { dateOfVisit = value; }
        }
        public DateTime DateLeftVisit
        {
            get { return dateLeftVisit; }
            set { dateLeftVisit = value; }
        }

        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }

        public Visit() { }
    }
}

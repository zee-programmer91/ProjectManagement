using System;

namespace LiveNiceApp
{
    public class Visit
    {
        public int visitorID = 0;
        public int personID = 0;
        public int tenantID = 0;
        public DateOnly dateOfVisit = new();

        public int VisitorID
        {
            get { return visitorID; }
            set { visitorID = value; }
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
        public DateOnly DateOfVisit
        {
            get { return dateOfVisit; }
            set { dateOfVisit = value; }
        }

        public Visit() { }
    }
}

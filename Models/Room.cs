namespace ProjectManagement.Models
{
    public class Room
    {
        public int roomID = 0;
        public int roomNumber = 0;
        public int tenantID = 0;
        public bool hasRoomAccess = false;
        public DateTime fromDate = new();
        public DateTime toDate = new();
        public bool deleted = false;

        public int RoomID
        {
            get { return roomID; }
            set { roomID = value; }
        }
        public int RoomNumber
        {
            get { return roomNumber; }
            set { roomNumber = value; }
        }
        public int TenantID
        {
            get { return tenantID; }
            set { tenantID = value; }
        }
        public bool HasRoomAccess
        {
            get { return hasRoomAccess; }
            set { hasRoomAccess = value; }
        }
        public DateTime FromDate
        {
            get { return fromDate; }
            set { fromDate = value; }
        }
        public DateTime ToDate
        {
            get { return toDate; }
            set { toDate = value; }
        }

        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }

        public Room() { }
    }
}

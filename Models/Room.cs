namespace ProjectManagement.Models
{
    public class Room
    {
        public int roomID = 0;
        public int roomNumber = 0;
        public int tenantID = 0;
        public bool hasRoomAccess = false;
        public DateOnly fromDate = new();
        public DateOnly toDate = new();

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
        public DateOnly FromDate
        {
            get { return fromDate; }
            set { fromDate = value; }
        }
        public DateOnly ToDate
        {
            get { return toDate; }
            set { toDate = value; }
        }

        public Room() { }
    }
}

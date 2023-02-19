using Npgsql;
using ProjectManagement.Database;
using ProjectManagement.Models;
using ProjectManagement.utlis;
using static ProjectManagement.Database.DatabaseActions;

namespace ProjectManagement.CRUD
{
    public class QueryRoom : DatabaseActionsBridge
    {
        public static new Room GetByID(int ID)
        {
            try
            {
                string commandText = $"SELECT * FROM ROOM WHERE TENANT_ID = @tenantID;";
                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());

                cmd.Parameters.AddWithValue("tenantID", ID);

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Room room = DatabaseReaders.ReadRoom(reader);
                    Console.WriteLine($"Selected room with personID {ID} from the ROOM Table");

                    return room;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR - Could not get room: {e.Message}");
            }

            return new Room();
        }

        public static new List<Room> GetAll()
        {
            List<Room> rooms = new();

            try
            {
                string commandText = $"SELECT * FROM ROOM;";
                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Room room = DatabaseReaders.ReadRoom(reader);
                    rooms.Add(room);
                }
                Console.WriteLine($"Selected all rooms from the ROOM Table");

                return rooms;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not get all rooms");
            }

            return rooms;
        }

        public static new DatabaseActionsResponses InsertEntry(object newEntry)
        {
            int result = 0;
            Room newRoom = (Room)newEntry;

            try
            {
                string commandText = $"INSERT INTO ROOM (room_number, tenant_id, from_date) VALUES(@roomNumber, @tenantID, @fromDate);";
                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());

                cmd.Parameters.AddWithValue("roomNumber", newRoom.roomNumber);
                cmd.Parameters.AddWithValue("tenantID", newRoom.tenantID);
                cmd.Parameters.AddWithValue("fromDate", newRoom.fromDate);

                result = cmd.ExecuteNonQuery();
                Console.WriteLine($"Saved room access of person with ID {newRoom.tenantID} INTO the ROOM table");
            }
            catch (Exception)
            {
                Console.WriteLine($"ERROR - Could not add room access to the room table for the person with the ID {newRoom.tenantID}");
            }
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }

        public static new DatabaseActionsResponses DeleteAll()
        {
            return DatabaseActionsResponses.Success;
        }

        public static new DatabaseActionsResponses SoftDeleteEntryByID(int ID)
        {
            int result = 0;

            try
            {
                string commandText = $"UPDATE ROOM SET deleted = CAST(1 AS bit) WHERE room_id = @room_id;";

                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("room_id", ID);

                result = (int)cmd.ExecuteNonQuery();
                Console.WriteLine($"Soft Deleted Room WITH ID {ID} IN ROOM TABLE");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine($"ERROR - Room with RoomID {ID} could not be deleted");
            }
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }

        public static new DatabaseActionsResponses DeleteEntryByID(int ID)
        {
            int result = 0;

            try
            {
                string commandText = $"DELETE FROM ROOM WHERE room_id = @room_id;";

                using var cmd = new NpgsqlCommand(commandText, DatabaseConnection.GetConnection());
                cmd.Parameters.AddWithValue("room_id", ID);

                result = (int)cmd.ExecuteNonQuery();
                Console.WriteLine($"Hard Deleted Room Access WITH ID {ID} IN ROOM TABLE");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine($"ERROR - Room with PersonAccessID {ID} could not be deleted");
            }
            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }

        public static new DatabaseActionsResponses UpdateEntryByID(int ID, object updateEntry)
        {
            int result = 0;

            return result > 0 ? DatabaseActionsResponses.Success : DatabaseActionsResponses.Failed;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ProjectManagement.CRUD;
using ProjectManagement.Models;

namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("Room")]
    public class RoomController : Controller
    {
        [HttpGet(Name = "GetOccupiedRooms")]
        public IActionResult GetOccupiedRooms()
        {
            return new ObjectResult(QueryRoom.GetAll());
        }

        [HttpGet("GetOccupiedRoom/{room_id}", Name = "GetOccupiedRoom")]
        public IActionResult GetOccupiedRoom(int room_id)
        {
            return new ObjectResult(QueryRoom.GetByID(room_id));
        }

        [HttpPost("AddRoom", Name = "AddRoom")]
        public IActionResult AddRoom(int roomNumber, int tenantID, DateTime fromDate)
        {
            Room room = new Room()
            {
                roomNumber= roomNumber,
                tenantID=tenantID,
                fromDate=fromDate,
            };
            return new ObjectResult($"ENTRY RESULT: {QueryRoom.InsertEntry(room)}");
        }

        [HttpPut("UpdateRoom/{room_id}", Name = "UpdateRoom")]
        public IActionResult UpdateRoom(int room_id, DateTime fromDate, DateTime toDate, int roomNumber = 0, int tenantID = 0, bool hasRoomAccess = false)
        {
            Room room = new Room()
            {
                roomNumber = roomNumber,
                tenantID = tenantID,
                fromDate = fromDate,
                toDate= toDate,
                hasRoomAccess= hasRoomAccess
            };
            return new ObjectResult($"UPDATE RESULT: {QueryRoom.UpdateEntryByID(room_id, room)}");
        }

        [HttpPut("SoftDeleteRoom/{room_id}", Name = "SoftDeleteRoom")]
        public IActionResult SoftDeleteRoom(int room_id)
        {
            return new ObjectResult($"DELETE RESULT: {QueryRoom.SoftDeleteEntryByID(room_id)}");
        }

        [HttpDelete("DeleteRoom/{room_id}", Name = "DeleteRoom")]
        public IActionResult DeleteRoom(int room_id)
        {
            return new ObjectResult($"DELETE RESULT: {QueryRoom.DeleteEntryByID(room_id)}");
        }
    }
}

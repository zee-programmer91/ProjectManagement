using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("Room")]
    public class RoomController : Controller
    {
        [HttpGet(Name = "GetOccupiedRooms")]
        public IActionResult GetOccupiedRooms()
        {
            return View();
        }

        [HttpGet("GetOccupiedRoom/{room_id}", Name = "GetOccupiedRoom")]
        public IActionResult GetOccupiedRoom()
        {
            return View();
        }

        [HttpPut("AddRoom/{room_id}", Name = "AddRoom")]
        public IActionResult AddRoom()
        {
            return View();
        }

        [HttpDelete("DeleteRoom/{room_id}", Name = "DeleteRoom")]
        public IActionResult DeleteRoom()
        {
            return View();
        }
    }
}

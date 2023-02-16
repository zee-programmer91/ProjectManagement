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

        [HttpGet("{room_id}", Name = "GetOccupiedRoom")]
        public IActionResult GetOccupiedRoom()
        {
            return View();
        }
    }
}

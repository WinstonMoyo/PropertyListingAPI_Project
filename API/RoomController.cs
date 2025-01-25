using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Orchestrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly RoomService roomService;

        private static List<Room> bookedRooms { get; set; } = new List<Room>();
        private static List<Room> canceledRooms { get; set; } = new List<Room>();

        public RoomController()
        {
            roomService = new RoomService("path");
        }

        [HttpGet]
        public ActionResult<List<Room>> GetRooms()
        {
            return Ok(RoomService.Rooms);
        }

        [HttpPost("book/{roomID}")]
        public ActionResult BookRoom(int roomID)
        {
            if(roomID < 0 || roomID >= RoomService.Rooms.Count)
            {
                return NotFound("Room Not Found");
            }

            var room = RoomService.Rooms[roomID];

            if(bookedRooms.Contains(room))
            {
                return BadRequest("Room is already booked");
            }

            bookedRooms.Add(room);

            return Ok("Room: " + roomID.ToString() + " booked successfully");
        }

        [HttpPost("cancel/{roomID}")]
        public ActionResult CancelRoom(int roomID)
        {
            if (roomID < 0 || roomID >= RoomService.Rooms.Count)
            {
                return NotFound("Room Not Found");
            }

            var room = RoomService.Rooms[roomID];

            if (!bookedRooms.Contains(room))
            {
                return BadRequest("Room is not booked");
            }

            canceledRooms.Add(room);

            return Ok("Room: " + roomID.ToString() + " canceled successfully");
        }
    }
}

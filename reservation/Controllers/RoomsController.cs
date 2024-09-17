using reservation.Models;
using reservation.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace reservation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly RoomService _roomService;

        // Ін'єкція залежності RoomService
        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }

        // Отримати всі зали
        [HttpGet]
        public ActionResult<List<Rooms>> GetRooms()
        {
            var rooms = _roomService.GetRooms();
            return Ok(rooms);
        }

        // Отримати конкретний зал за ID
        [HttpGet("{id}")]
        public ActionResult<Rooms> GetRoom(Guid id)
        {
            var room = _roomService.GetRooms().Find(r => r.Id == id);
            if (room == null)
                return NotFound();
            return Ok(room);
        }

        // Додати новий зал
        [HttpPost]
        public ActionResult<Rooms> AddRoom([FromBody] Rooms room)
        {
            var newRoom = _roomService.AddRoom(room);
            return CreatedAtAction(nameof(GetRoom), new { id = newRoom.Id }, newRoom); // Повертаємо посилання на новостворений ресурс
        }

        // Оновити інформацію про зал
        [HttpPut("{id}")]
        public ActionResult<Rooms> UpdateRoom(Guid id, [FromBody] Rooms updatedRoom)
        {
            var room = _roomService.UpdateRoom(id, updatedRoom);
            if (room == null)
                return NotFound();
            return Ok(room);
        }

        // Видалити зал
        [HttpDelete("{id}")]
        public ActionResult DeleteRoom(Guid id)
        {
            var isDeleted = _roomService.DeleteRoom(id);
            if (!isDeleted)
                return NotFound();
            return NoContent(); // 204 статус — успішно, але немає контенту для повернення
        }

        // Розрахувати вартість бронювання
        [HttpPost("calculate-cost")]
        public ActionResult<decimal> CalculateBookingCost([FromBody] BookingRequest bookingRequest)
        {
            var totalCost = _roomService.CalculateBookingCost(
                bookingRequest.RoomId,
                bookingRequest.StartTime,
                bookingRequest.EndTime,
                bookingRequest.Services
            );

            if (totalCost == 0)
                return NotFound("Room not found");

            return Ok(totalCost);
        }
    }

    // DTO для запиту розрахунку вартості бронювання
    public class BookingRequest
    {
        public Guid RoomId { get; set; }  // ID залу
        public DateTime StartTime { get; set; }  // Час початку бронювання
        public DateTime EndTime { get; set; }    // Час закінчення бронювання
        public List<AddService> Services { get; set; } = new List<AddService>(); // Обрані послуги
    }
}

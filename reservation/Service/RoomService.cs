using reservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace reservation.Services
{
    // Сервіс для управління залами
    public class RoomService
    {
        private readonly List<Rooms> _rooms = new(); // Список залів

        // Отримати всі зали
        public List<Rooms> GetRooms() => _rooms;

        // Додати новий зал
        public Rooms AddRoom(Rooms room)
        {
            Guid g = Guid.NewGuid();
            room.Id = g; // Генерація унікального ID
            _rooms.Add(room); // Додаємо зал у список
            return room;
        }

        // Оновити інформацію про зал
        public Rooms UpdateRoom(Guid id, Rooms updatedRoom)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == id);
            if (room != null)
            {
                room.Name = updatedRoom.Name;
                room.Capacity = updatedRoom.Capacity;
                room.BasePrice = updatedRoom.BasePrice;
                room.Services = updatedRoom.Services;
            }
            return room;
        }

        // Видалити зал
        public bool DeleteRoom(Guid id)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == id);
            if (room != null)
            {
                _rooms.Remove(room);
                return true;
            }
            return false;
        }

        // Пошук залів
        public List<Rooms> SearchRooms(string name)
        {
            return _rooms.Where(r => r.Name.Contains(name)).ToList();
        }
        // Метод для визначення знижок або націнок в залежності від часу бронювання
        private float GetDiscount(DateTime startTime)
        {
            if (startTime.Hour >= 18 && startTime.Hour < 23)
                return 0.20f; // Вечірня знижка 20%
            if (startTime.Hour >= 6 && startTime.Hour < 9)
                return 0.10f; // Ранкова знижка 10%
            if (startTime.Hour >= 12 && startTime.Hour < 14)
                return -0.15f; // Пікова націнка 15%
            return 0;
        }
        // Розрахунок вартості оренди залу разом із послугами
        public float CalculateBookingCost(Guid roomId, DateTime startTime, DateTime endTime, List<AddService> services)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == roomId);
            if (room == null)
                return 0;

            // Вартість оренди залу
            float baseCost = room.BasePrice * (float)(endTime - startTime).TotalHours;

            // Вартість додаткових послуг
            float servicesCost = services.Sum(service => (int)service);

            // Підсумкова вартість із урахуванням послуг та знижок/націнок
            float discount = GetDiscount(startTime);
            return (baseCost + servicesCost) * (1 - discount);
        }



    }
}

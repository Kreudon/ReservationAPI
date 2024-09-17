namespace reservation.Models
{
    /// <summary>
    /// Передача даних DTO
    /// </summary>
    public enum AddServiceDTO           //Перелік послуг
    {
        Projector = 500,
        WiFi = 300,
        Sound = 700
    }
    public class RoomsDTO                //Свторення кімнат
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public float BasePrice { get; set; }
        public List<AddService> Services { get; set; } = new List<AddService>();
    }
}

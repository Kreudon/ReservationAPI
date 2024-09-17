namespace reservation.Models
{
    public enum  AddService             //Перелік послуг
    {
        Projector = 500,
        WiFi = 300,
        Sound = 700
    }
    public class Rooms                 //Свторення кімнат
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public float BasePrice { get; set; }
        public List<AddService> Services { get; set; } = new List<AddService>(); 
    }
}

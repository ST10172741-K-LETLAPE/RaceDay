namespace RaceDay.API.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public int EventId { get; set; }
        public string CategoryName { get; set; } = "";
        public double DistanceKm { get; set; }
        public decimal EntryFee { get; set; }
        public Event? Event { get; set; }
    }
}

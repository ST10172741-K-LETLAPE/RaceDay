using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.Models
{
    public class EventRoute
    {
        [Key]
        public int EventRouteId { get; set; }

        [Required]
        public int EventId { get; set; }
        public Event? Event { get; set; }

        [Required]
        public string GpxData { get; set; } = string.Empty;

        public double? DistanceKm { get; set; }
        public int? ElevationGainM { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
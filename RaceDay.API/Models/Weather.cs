using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.Models
{
    public class Weather
    {
        [Key]
        public int WeatherId { get; set; }

        [Required]
        public int EventId { get; set; }
        public Event? Event { get; set; }

        public decimal? Temperature { get; set; }

        [MaxLength(50)]
        public string? WeatherCondition { get; set; }

        public decimal? WindSpeed { get; set; }

        public DateTime RecordedAt { get; set; } = DateTime.Now;
    }
}
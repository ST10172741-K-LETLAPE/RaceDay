using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        
        [Required]
        public string Title { get; set; } = "";
        
        public string Location { get; set; } = "";
        
        public DateTime EventDate { get; set; }
        
        public string Description { get; set; } = "";
        
        public string BannerUrl { get; set; } = ""; // For Number 5 Azure Blob
        
        public string Status { get; set; } = "Upcoming"; // Upcoming, Published, Draft, Cancelled
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
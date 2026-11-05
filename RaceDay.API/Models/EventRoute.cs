namespace RaceDay.API.Models
{
    public class EventRoute
    {
        public int EventRouteId { get; set; }
        public int EventId { get; set; }
        public string GpxData { get; set; } = "";
        public double DistanceKm { get; set; }
        public double ElevationGainM { get; set; }
        public string RouteName { get; set; } = "";
        public string RouteDescription { get; set; } = "";
        public Event? Event { get; set; }
    }
}

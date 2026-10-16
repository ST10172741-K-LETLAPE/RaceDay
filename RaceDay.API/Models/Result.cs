namespace RaceDay.API.Models
{
    public class Result
    {
        public int ResultId { get; set; }
        public int EnrolmentId { get; set; }
        public System.TimeSpan FinishTime { get; set; }
        public int Position { get; set; }
        public string Status { get; set; } = "Finished";
        public Enrolment? Enrolment { get; set; }
    }
}

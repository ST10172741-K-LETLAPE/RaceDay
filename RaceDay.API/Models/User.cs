namespace RaceDay.API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = "";
        public string Surname { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public string Role { get; set; } = "Participant";
        public string Phone { get; set; } = "";
        public ICollection<Enrolment> Enrolments { get; set; } = new List<Enrolment>();
    }
}

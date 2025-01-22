namespace ClubRainbowSG.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public string? contactFK { get; set; }
        public string? programmePCS_FK { get; set; }
        public string? programmeSession_nameFK { get; set; }
        public int ticket_count { get; set; }
        public string?  Attendence { get; set; } 
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubRainbowSG.Models
{
    public class RegistrationDto
    {
        [Key]
        public int Id { get; set; }
        public string? ContactFK { get; set; }


        public string? programmePCS_FK { get; set; }
   
        public string? programmeSession_name_FK { get; set; }
        public int TicketCount { get; set; }
    }
}

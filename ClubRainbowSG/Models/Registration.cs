using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubRainbowSG.Models
{
    [Table("Registration")]
    public class Registration
    {

        [Key]  
        public int Id { get; set; }
        public string? contactFK { get; set; }
      
        public string? programmePCS_FK { get; set; }
        
        public string? programmeSession_name_FK { get; set; }
        public int ticket_count { get; set; }
    }
}

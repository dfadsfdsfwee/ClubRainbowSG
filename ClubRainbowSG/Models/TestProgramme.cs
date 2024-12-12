using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubRainbowSG.Models
{
    [Table("TestProgram")]
    public class TestProgramme
    {

        [Key]
        public string pcscode { get; set; }
        public string? pcsname { get; set; }
        public string type { get; set; } = string.Empty;
        public string? other_type { get; set; }
        public string? age_group { get; set; }
        public string? other_age_group { get; set; }
        public DateTime start_date_time { get; set; }
        public DateTime end_date_time { get; set; }
        public string session_name { get; set; } = string.Empty;
        public int frequency { get; set; }
        public int capacity { get; set; }
        public int programme_status { get; set; }
        public string location { get; set; } = string.Empty;
        public string attire { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public int ticket_issused { get; set; }
    }
}


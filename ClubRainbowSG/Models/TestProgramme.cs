using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubRainbowSG.Models
{
    [Table("TestProgram")]
    public class TestProgramme
    {

        [Key]
        public int PSCCode { get; set; }
        public string? PSCName { get; set; } 

        public string? Type { get; set; } 

        public string? OtherType { get; set; }

        public string? ProgramGroupAge { get; set; }

        public string? OtherAgeGroup { get; set; }

        public DateTime StartDate { get; set; } 

        public DateTime EndDate { get; set; } 

        public TimeSpan StartTime { get; set; } 

        public TimeSpan EndTime { get; set; } 

        public string? SessionDesc { get; set; }

        public int Frequency { get; set; } 

        public int? CapacityPerSession { get; set; } 

        public int? ProgramStatus { get; set; } 

        public string? Location { get; set; } 

        
        public string? Description { get; set; } 

        public string? Attire { get; set; } // Matches [Attire]

        public int? TicketCount { get; set; } // Matches [TicketCount]
    }
}


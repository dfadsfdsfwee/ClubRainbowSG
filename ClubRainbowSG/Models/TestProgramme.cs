using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubRainbowSG.Models
{
    [Table("testprogrammes")]
    public class TestProgramme
    {
        [Key]
        public string? PCSname { get; set; }
        public string? Type { get; set; }
        public string? OtherType { get; set; }
        public string? AgeGrp { get; set; }
        public string? OtherAgeGrp { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int NoOfSession { get; set; }
        public int Frequency { get; set; }
        public int? Capacity { get; set; }
        public string? ProgrammeStatus { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string? Attire { get; set; }
        public string? PscCode { get; set; }
        public int? TicketsPerUser { get; set; }
    }
}


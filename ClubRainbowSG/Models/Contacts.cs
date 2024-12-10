using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubRainbowSG.Models
{
    [Table("Contacts")]
    public class Contacts
    {
        

        public int? salutation { get; set; }
        public string?salutation_only { get; set; }
        public string? full_name { get; set; }
        public string? title { get; set; }
        public string? mailing_street { get; set; }
        public string? mailing_City { get; set; }
       
        public string? mailing_zip_postal { get; set; }
        public string? mailing_country { get; set; }
        public string? phone { get; set; }
        public int? mobile { get; set; }
        public int? fax { get; set; }
        public string? email { get; set; }
        public string? account_owner { get; set; }
        [Key]
        public string? account_name { get; set; }
        public string? hashed_password { get; set; }
        public string? guardian_1 { get; set; }
        public string? guardian_2 { get; set; }
        public string? guardian_3 { get; set; }
        public string? guardian_4 { get; set; }

        /*ForeignKey*/
   
    }
}

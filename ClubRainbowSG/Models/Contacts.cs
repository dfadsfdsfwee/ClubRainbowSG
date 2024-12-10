using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubRainbowSG.Models
{
    [Table("Contacts")]
    public class Contacts
    {
        
        //public string AccountID {  get; set; }
        public int? salutation { get; set; }
        public string? salutation_Only { get; set; }
        public string? full_name { get; set; }
        public string? title { get; set; }
        public string? mailing_street { get; set; }
        public string? mailing_city { get; set; }
        public string? mailing_state_provence { get; set; }
        public string? mailing_zip_postal{ get; set; }
        public string? mailing_country { get; set; }
        public string? phone { get; set; }
        public int? mobile { get; set; }
        public int? fax { get; set; }
        public string? email { get; set; }
        public string? account_owner { get; set; }
        [Key]
        public string account_name { get; set; }
        public string? hashed_password { get; set; }
        public string? Guardian_1 { get; set; }
        public string? Guardian_2 { get; set; }
        public string? Guardian_3 { get; set; }
        public string? Guardian_4 { get; set; }

        /*ForeignKey*/
   
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubRainbowSG.Models
{
    [Table("Contact")]
    public class Contacts
    {
        [Key]
        public string AccountID {  get; set; }
        public string? Salutation { get; set; }
        public string?SalutationOnly { get; set; }
        public string? FullName { get; set; }
        public string? Title { get; set; }
        public string? MailingStreet { get; set; }
        public string? MailingCity { get; set; }
        public string? MailingState_Province { get; set; }
        public string? MailingZip_PostalCode { get; set; }
        public string? MailingCountry { get; set; }
        public string? Phone { get; set; }
        public int? Mobile { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? AccountOwner { get; set; }
        public string? Password { get; set; }
        public string? Guardian_1 { get; set; }
        public string? Guardian_2 { get; set; }
        public string? Guardian_3 { get; set; }
        public string? Guardian_4 { get; set; }

        /*ForeignKey*/
   
    }
}

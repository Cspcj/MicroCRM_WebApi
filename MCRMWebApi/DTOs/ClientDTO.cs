using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MCRMWebApi.DTOs
{
    [Table("Clients")]
    public class ClientDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ClientId")]
        public Guid ClientID { get; set; }

        [Column("ClientName")]
        [Required]
        public string ClientName { get; set; }

        [Column("ClientCompany")]
        [AllowNull]
        public string? ClientCompany { get; set; }

        [Column("ClientEmail")]
        [Required]
        public string ClientEmail { get; set; }

        [Column("ClientPhoneNumber")]
        [Required]
        //[MinLength(10, "Phone number minimum length is 10 chars")]
        public string ClientPhoneNumber { get; set; }

        [Column("ClientAdress")]
        public string? ClientAdress { get; set; }

        [Column("ClientOtherInformation")]
        public string? ClientOtherInformation { get; set; }

        [ForeignKey("Users")]
        [Column("UserID")]
        public Guid UserId { get; set; }
    }

}


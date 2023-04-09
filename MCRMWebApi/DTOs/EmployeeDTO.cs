using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MCRMWebApi.DTOs
{
    [Table("Employees")]
    public class EmployeeDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("EmployeeId")]
        public Guid EmployeeID { get; set; }

        [Column("EmployeeName")]
        [Required]
        public string EmployeeName { get; set; }

        [Column("EmployeeEmail")]
        [Required]
        public string EmployeeEmail { get; set; }

        [Column("EmployeePhoneNumber")]
        [Required]
        //[MinLength(10, "Phone number minimum length is 10 chars")]
        public string EmployeePhoneNumber { get; set; }

        [Column("EmployeeAdress")]
        public string? EmployeeAdress { get; set; }

        [Column("EmployeeOtherInformation")]
        public string? EmployeeOtherInformation { get; set; }

        [ForeignKey("Users")]
        [Column("UserID")]
        public Guid UserId { get; set; }
    }

}


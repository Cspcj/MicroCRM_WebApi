using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCRMWebApi.DTOs.PartialDTOCreate
{
    public class EmployeePartialCreateDTO
    {
        [Required]
        public string EmployeeName { get; set; }

        [Required]
        public string EmployeeEmail { get; set; }

        [Required]
        public string EmployeePhoneNumber { get; set; }
        
        public string? EmployeeAdress { get; set; }
        
        public string? EmployeeOtherInformation { get; set; }
    }
}

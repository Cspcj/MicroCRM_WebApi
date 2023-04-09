using System.ComponentModel.DataAnnotations;

namespace MCRMWebApi.DTOs.PartialDTOUpdate
{
    public class EmployeePartialUpdateDTO
    {
        public string? EmployeeName { get; set; }

        public string? EmployeeEmail { get; set; }

        public string? EmployeePhoneNumber { get; set; }

        public string? EmployeeAdress { get; set; }

        public string? EmployeeOtherInformation { get; set; }

    }
}

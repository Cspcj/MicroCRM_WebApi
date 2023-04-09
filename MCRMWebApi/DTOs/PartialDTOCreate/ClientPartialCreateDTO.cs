using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCRMWebApi.DTOs.PartialDTOCreate
{
    public class ClientPartialCreateDTO
    {
        [Required]
        public string ClientName { get; set; }

        public string? ClientCompany { get; set; }

        [Required]
        public string ClientEmail { get; set; }

        [Required]
        public string ClientPhoneNumber { get; set; }
        
        public string? ClientAdress { get; set; }
        
        public string? ClientOtherInformation { get; set; }
    }
}

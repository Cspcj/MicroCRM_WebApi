using System.ComponentModel.DataAnnotations;

namespace MCRMWebApi.DTOs.PartialDTOUpdate
{
    public class ClientPartialUpdateDTO
    {
        public string? ClientName { get; set; }

        public string? ClientCompany { get; set; }

        public string? ClientEmail { get; set; }

        public string? ClientPhoneNumber { get; set; }

        public string? ClientAdress { get; set; }

        public string? ClientOtherInformation { get; set; }

    }
}

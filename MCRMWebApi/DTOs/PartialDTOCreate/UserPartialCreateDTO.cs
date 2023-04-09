using System.ComponentModel.DataAnnotations;


namespace MCRMWebApi.DTOs.PartialDTOCreate
{
    public class UserPartialCreateDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

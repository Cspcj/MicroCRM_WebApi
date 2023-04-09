using System.ComponentModel.DataAnnotations;

namespace MCRMWebApi.DTOs.PartialDTOUpdate
{
    public class UserDTOUpdate
    {
            public string? UserName { get; set; }
            public string? Password { get; set; }
    }
}

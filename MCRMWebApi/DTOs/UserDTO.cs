using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCRMWebApi.DTOs
{
    [Table ("Users")]
    public class UserDTO
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

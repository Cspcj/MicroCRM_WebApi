using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCRMWebApi.DTOs
{
    public class NotePartialUpdateDTO
    {
        public string? Title { get; set; }
        [Required]
        public string? Note { get; set; }
        public int? ProjectId { get; set; }
        public Guid? OwnerId { get; set; }
    }
}

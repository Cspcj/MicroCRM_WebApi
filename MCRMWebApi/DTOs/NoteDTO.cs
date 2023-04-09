using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCRMWebApi.DTOs
{
    [Table("Notes")]
    public class NoteDTO
    {
        [Key]
        public Guid NoteId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Note { get; set; }
        [Required]
        [ForeignKey("Projects")]
        public int ProjectId { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

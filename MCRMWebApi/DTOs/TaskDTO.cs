using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCRMWebApi.DTOs
{
    [Table("Tasks")]
    public class TaskDTO
    {
        [Key]
        public Guid TaskId { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        [ForeignKey("Projects")]
        public int ProjectId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}

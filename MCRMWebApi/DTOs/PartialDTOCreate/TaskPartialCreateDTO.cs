using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCRMWebApi.DTOs
{
    public class TaskPartialCreateDTO
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        public DateTime DueDate { get; set; }
    }
}

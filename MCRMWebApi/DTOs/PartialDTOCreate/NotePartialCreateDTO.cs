using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCRMWebApi.DTOs
{
    public class NotePartialCreateDTO
    {
        public string? Text { get; set; }
        public int ProjectId { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? IsCompleted { get; set; }
        public Guid OwnerId { get; set; }
    }
}

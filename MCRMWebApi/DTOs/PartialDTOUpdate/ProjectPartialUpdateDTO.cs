using System.ComponentModel.DataAnnotations;

namespace MCRMWebApi.DTOs.PartialDTOUpdate
{
    public class ProjectPartialUpdateDTO
    {
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public string? ProjectLocation { get; set; }
        public string? ProjectLocationCity { get; set; }
        public string? Region { get; set; }
        public Guid? ClientID { get; set; }
        public Guid? UserId { get; set; }
    }
}

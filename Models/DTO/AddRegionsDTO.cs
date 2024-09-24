using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class AddRegionsDTO
    {
        [Required]
        [MaxLength(7)]
        [MinLength(3)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}

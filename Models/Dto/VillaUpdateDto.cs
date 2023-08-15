using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_testing2.Models.Dto
{
    public class VillaUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string? Details { get; set; }

        [Required]
        public string? ImageUrl { get; set; }

        [Range(0, int.MaxValue)]
        [Required]
        public int? Tenants { get; set; }

        [Range(0, int.MaxValue)]
        [Required]
        public int? SizeMeters { get; set; }

        [Required]
        public decimal? Fee { get; set; }

        public DateTime Creation { get; set; }

        public DateTime Update { get; set; }

    }
}

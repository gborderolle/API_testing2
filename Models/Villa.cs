using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API_testing1.Services;
using API_testing2.Models.Dto;

namespace API_testing2.Models
{
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Details { get; set; }

        public string? ImageUrl { get; set; }

        [Range(0, int.MaxValue)]
        public int? Tenants { get; set; }

        [Range(0, int.MaxValue)]
        public int? SizeMeters { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Fee { get; set; }

        public DateTime Creation { get; set; }

        public DateTime Update { get; set; }

        public VillaDto ToDTO()
        {
            return Utls.mapper.Map<VillaDto>(this);
        }

    }
}

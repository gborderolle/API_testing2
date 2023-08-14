using API_testing1.Services;
using API_testing2.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_testing2.Models
{
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Details{ get; set; }
        public string? ImageUrl { get; set; }
        public int? Tenants { get; set; }
        public int? SizeMeters { get; set; }
        public decimal? Fee { get; set; }
        public DateTime Creation { get; set; }
        public DateTime Update { get; set; }

        internal VillaDto ToDTO()
        {
            return Utls.mapper.Map<VillaDto>(this);
        }

        internal async Task<VillaDto> ToDTOAsync()
        {
            return Utls.mapper.Map<VillaDto>(this);
        }
    }
}

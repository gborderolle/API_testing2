using System.ComponentModel.DataAnnotations;

namespace API_testing2.Models.Dto
{
    /// <summary>
    /// Entidad:
    /// FK tiene el Id externo y el objeto con un datanotation: fk del mismo Id externo
    /// 
    /// DTOs:
    /// La idea es que sean los que se devuelven al front (devuelve los endpoints); no devolver la entidad misma
    /// Mantienen los Required y los MaxLenth
    /// No tienen los Datetime corporativos (create y update)
    /// CreateDTO: no lleva Id; UpdateDTO sí lleva Id (requerido)
    /// 
    /// Recordar implementar el AutoMap para cada relación entidad-DTO
    /// </summary>
    public class VillaCreateDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string? Details { get; set; }

        public string? ImageUrl { get; set; }

        [Range(0, int.MaxValue)]
        public int? Tenants { get; set; }

        [Range(0, int.MaxValue)]
        public int? SizeMeters { get; set; }

        [Required]
        public decimal? Fee { get; set; }

        public DateTime Creation { get; set; }

        public DateTime Update { get; set; }

    }
}

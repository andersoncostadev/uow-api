using System.ComponentModel.DataAnnotations;

namespace Cooperchip.DiretoaoPonto.UoW.Api.Models
{
    public class FlightDTO
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "O camo {0} temo no máximo {1} caracteres!")]
        public string? Code { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O camo {0} temo no máximo {1} caracteres!")]
        public string? RoadMap { get; set; }

        public int? Capacity { get; set; } = 4;
        public int? Availability { get; set;} = 4;
    }
}

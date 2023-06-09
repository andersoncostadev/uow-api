using System.ComponentModel.DataAnnotations;

namespace Cooperchip.DiretoaoPonto.UoW.Api.Models
{
    public class PassengerDTO
    {
        [Key]
        [Required(ErrorMessage ="O camo {0} é obrigatório")]
        public long? Id { get; set; }

        [StringLength(50, ErrorMessage = "O camo {0} temo no máximo {1} caracteres!")]
        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        public string? Name { get; set; } = string.Empty;

        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        public long? FlightId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SistemaDeViajes.DTOs
{
    public class UpdateTransportistaDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(200, ErrorMessage = "El nombre no puede tener más de 200 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La tarifa es obligatoria.")]
        public decimal TarifaPorKm { get; set; }

        [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
        [StringLength(50, ErrorMessage = "El número de teléfono no puede tener más de 50 caracteres.")]
        public string Telefono { get; set; } = null!;

        public bool Estado { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SistemaDeViajes.DTOs
{
    public class UpdateEmpleadoDto
    {
        [Required(ErrorMessage = "El nombre de empleado es obligatorio.")]
        [StringLength(200, ErrorMessage = "El nombre de empleado no puede tener más de 200 caracteres.")]
        public string NombreEmpleado { get; set; } = null!;

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(200, ErrorMessage = "La dirección no puede tener más de 200 caracteres.")]

        public string Direccion { get; set; } = null!;

        [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
        [StringLength(50, ErrorMessage = "El número de teléfono no puede tener más de 50 caracteres.")]

        public string Telefono { get; set; } = null!;

        public bool Estado { get; set; }
    }
}

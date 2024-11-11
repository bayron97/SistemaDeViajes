using System.ComponentModel.DataAnnotations;
namespace SistemaDeViajes.DTOs
{ 
    public class UpdateSucursalDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(200, ErrorMessage = "El nombre no puede tener más de 200 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(200, ErrorMessage = "La dirección no puede tener más de 200 caracteres.")]
        public string Direccion { get; set; } = null!;
    }
}

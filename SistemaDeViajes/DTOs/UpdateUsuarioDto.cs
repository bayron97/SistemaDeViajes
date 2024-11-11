using System.ComponentModel.DataAnnotations;

namespace SistemaDeViajes.DTOs
{
    public class UpdateUsuarioDto
    {

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(200, ErrorMessage = "El nombre usuario no puede tener más de 200 caracteres.")]
        public string NombreUsuario { get; set; } = null!;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [StringLength(200, ErrorMessage = "El correo no puede tener más de 200 caracteres.")]
        public string Correo { get; set; } = null!;


        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(25, ErrorMessage = "La contraseña no puede tener más de 25 caracteres.")]

        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "El rol es obligatorio.")]
        [StringLength(50, ErrorMessage = "El rol no puede tener más de 50 caracteres.")]

        public string Rol { get; set; } = null!;
    }
}

namespace SistemaDeViajes.DTOs
{
    public class GetUsuarioDto
    {

        public int UsuarioId { get; set; }

        public string NombreUsuario { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Rol { get; set; } = null!;
    }
}

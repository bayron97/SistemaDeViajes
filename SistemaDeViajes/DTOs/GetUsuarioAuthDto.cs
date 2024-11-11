namespace SistemaDeViajes.DTOs
{
    public class GetUsuarioAuthDto
    {
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Correo { get; set; } = null!;
    }
}

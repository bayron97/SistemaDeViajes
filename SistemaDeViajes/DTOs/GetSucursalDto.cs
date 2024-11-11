namespace SistemaDeViajes.DTOs
{
    public class GetSucursalDto
    {
        public int SucursalId { get; set; }

        public string Nombre { get; set; } = null!;

        public string Direccion { get; set; } = null!;
    }
}

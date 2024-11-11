namespace SistemaDeViajes.DTOs
{
    public class GetViajeDto
    {
        public int ViajeId { get; set; }
        public DateTime Fecha { get; set; }
        public int SucursalId { get; set; }
        public int UsuarioId { get; set; }
        public int TransportistaId { get; set; }
        public decimal TotalDistancia { get; set; }
        public decimal TotalCosto { get; set; }
        public List<GetDetalleViajeDto> DetalleViajes { get; set; } = new();
    }
}

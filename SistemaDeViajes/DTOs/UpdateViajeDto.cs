namespace SistemaDeViajes.DTOs
{
    public class UpdateViajeDto
    {
        public DateTime Fecha { get; set; }
        public int SucursalId { get; set; }
        public int UsuarioId { get; set; }
        public int TransportistaId { get; set; }
        public decimal TotalDistancia { get; set; }
        public decimal TotalCosto { get; set; }
        public List<UpdateDetalleViajeDto> DetalleViajes { get; set; } = new();
    }
}

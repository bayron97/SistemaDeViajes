namespace SistemaDeViajes.DTOs
{
    public class UpdateDetalleViajeDto
    {
        public int DetalleId { get; set; }
        public int EmpleadoId { get; set; }
        public decimal DistanciaKm { get; set; }
    }
}

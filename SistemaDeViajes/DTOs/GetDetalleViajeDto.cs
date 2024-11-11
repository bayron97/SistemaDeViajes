namespace SistemaDeViajes.DTOs
{
    public class GetDetalleViajeDto
    {
        public int DetalleId { get; set; }
        public int EmpleadoId { get; set; }
        public decimal DistanciaKm { get; set; }
    }
}

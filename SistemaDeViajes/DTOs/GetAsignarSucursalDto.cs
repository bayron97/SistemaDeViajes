namespace SistemaDeViajes.DTOs
{
    public class GetAsignarSucursalDto
    {
        public int AsignacionId { get; set; }
        public int EmpleadoId { get; set; }
        public int SucursalId { get; set; }
        public decimal DistanciaKm { get; set; }
        public string NombreEmpleado { get; set; } = null!;
        public string NombreSucursal { get; set; } = null!;
    }
}

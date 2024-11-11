
namespace SistemaDeViajes.Models;

public partial class DetalleViaje
{
    public int DetalleId { get; set; }

    public int ViajeId { get; set; }

    public int EmpleadoId { get; set; }

    public decimal DistanciaKm { get; set; }

    public virtual Viaje Viaje { get; set; } = null!;
}


namespace SistemaDeViajes.Models;

public partial class AsignarSucursale
{
    public int AsignacionId { get; set; }

    public int EmpleadoId { get; set; }

    public int SucursalId { get; set; }

    public decimal DistanciaKm { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual Sucursale Sucursal { get; set; } = null!;
}

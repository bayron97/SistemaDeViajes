
namespace SistemaDeViajes.Models;

public partial class Empleado
{
    public int EmpleadoId { get; set; }

    public string NombreEmpleado { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<AsignarSucursale> AsignarSucursales { get; } = new List<AsignarSucursale>();
}

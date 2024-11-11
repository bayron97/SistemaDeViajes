
namespace SistemaDeViajes.Models;

public partial class Sucursale
{
    public int SucursalId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public virtual ICollection<AsignarSucursale> AsignarSucursales { get; } = new List<AsignarSucursale>();

    public virtual ICollection<Viaje> Viajes { get; } = new List<Viaje>();
}

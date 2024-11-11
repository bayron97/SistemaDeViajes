
namespace SistemaDeViajes.Models;

public partial class Viaje
{
    public int ViajeId { get; set; }

    public DateTime Fecha { get; set; }

    public int SucursalId { get; set; }

    public int UsuarioId { get; set; }

    public int TransportistaId { get; set; }

    public decimal TotalDistancia { get; set; }

    public decimal TotalCosto { get; set; }

    public virtual ICollection<DetalleViaje> DetalleViajes { get; } = new List<DetalleViaje>();

    public virtual Sucursale Sucursal { get; set; } = null!;

    public virtual Transportista Transportista { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}

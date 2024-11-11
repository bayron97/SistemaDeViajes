
namespace SistemaDeViajes.Models;

public partial class Transportista
{
    public int TransportistaId { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal TarifaPorKm { get; set; }

    public string Telefono { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Viaje> Viajes { get; } = new List<Viaje>();
}

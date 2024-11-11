namespace SistemaDeViajes.DTOs
{
    public class GetTransportistaDto
    {
        public int TransportistaId { get; set; }

        public string Nombre { get; set; } = null!;

        public decimal TarifaPorKm { get; set; }

        public string Telefono { get; set; } = null!;

        public bool Estado { get; set; }
    }
}

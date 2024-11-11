using SistemaDeViajes.DTOs;

public interface ITransportista
{
    Task<IEnumerable<GetTransportistaDto>> GetTransportistasAsync();
    Task<GetTransportistaDto> GetTransportistaByIdAsync(int id);
    Task<GetTransportistaDto> CreateTransportistaAsync(UpdateTransportistaDto newTransportista);
    Task<bool> UpdateTransportistaAsync(int id, UpdateTransportistaDto updatedTransportista);
    Task<bool> DeleteTransportistaAsync(int id);
}

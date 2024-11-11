using SistemaDeViajes.DTOs;

namespace SistemaDeViajes.Interfaces
{
    public interface IViaje
    {
        Task<GetViajeDto> GetViajeByIdAsync(int id);
        Task<IEnumerable<GetViajeDto>> GetViajesAsync();
        Task<GetViajeDto> CreateViajeAsync(UpdateViajeDto newViaje);
        Task<bool> UpdateViajeAsync(int id, UpdateViajeDto updatedViaje);
        Task<bool> DeleteViajeAsync(int id);
    }
}

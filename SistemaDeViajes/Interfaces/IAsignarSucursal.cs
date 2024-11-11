using SistemaDeViajes.DTOs;

namespace SistemaDeViajes.Interfaces
{
    public interface IAsignarSucursal
    {
        Task<IEnumerable<GetAsignarSucursalDto>> GetAsignacionesAsync();
        Task<GetAsignarSucursalDto> GetAsignacionByIdAsync(int id);
        Task<GetAsignarSucursalDto> CreateAsignacionAsync(UpdateAsignarSucursalDto newAsignacion);
        Task<bool> UpdateAsignacionAsync(int id, UpdateAsignarSucursalDto updatedAsignacion);
        Task<bool> DeleteAsignacionAsync(int id);
    }
}

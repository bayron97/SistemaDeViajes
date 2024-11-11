using SistemaDeViajes.DTOs;

namespace SistemaDeViajes.Interfaces
{
    public interface IEmpleado
    {
        Task<IEnumerable<GetEmpleadoDto>> GetEmpleadosAsync();
        Task<GetEmpleadoDto> GetEmpleadoByIdAsync(int id);
        Task<GetEmpleadoDto> CreateEmpleadoAsync(UpdateEmpleadoDto newEmpleado);
        Task<bool> UpdateEmpleadoAsync(int id, UpdateEmpleadoDto updatedEmpleado);
        Task<bool> DeleteEmpleadoAsync(int id);
    }
}

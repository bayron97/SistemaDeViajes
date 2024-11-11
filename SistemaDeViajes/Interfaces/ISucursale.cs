using SistemaDeViajes.DTOs;
namespace SistemaDeViajes.Interfaces
{
    public interface ISucursale
    {
        Task<IEnumerable<DTOs.GetSucursalDto>> GetSucursalesAsync();
        Task<DTOs.GetSucursalDto?> GetSucursalByIdAsync(int id);
        Task<GetSucursalDto> CreateSucursalAsync(UpdateSucursalDto newSucursal);

        Task<bool> UpdateSucursalAsync(int id, DTOs.UpdateSucursalDto updateSucursal);
        Task<bool> DeleteSucursalAsync(int id);
    }
}

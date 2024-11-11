using SistemaDeViajes.DTOs;

namespace SistemaDeViajes.Interfaces
{
    public interface IUsuario
    {
        Task<GetUsuarioAuthDto?> LoginAsync(LoginUsuarioDto loginDto);
        Task<IEnumerable<GetUsuarioDto>> GetUsuariosAsync();
        Task<GetUsuarioDto?> GetUsuarioByIdAsync(int id);
        Task<GetUsuarioDto> CreateUsuarioAsync(UpdateUsuarioDto newUsuario);

        Task<bool> UpdateUsuarioAsync(int id, UpdateUsuarioDto updateUsuario);
        Task<bool> DeleteUsuarioAsync(int id);
    }
}

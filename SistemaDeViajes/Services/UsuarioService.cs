using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaDeViajes.Context;
using SistemaDeViajes.DTOs;
using SistemaDeViajes.Interfaces;
using SistemaDeViajes.Models;

namespace SistemaDeViajes.Services
{
    public class UsuarioService : IUsuario
    {
        private readonly SiViajeContext _context;

        private readonly IMapper _mapper;

        public UsuarioService(SiViajeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<GetUsuarioAuthDto?> LoginAsync(LoginUsuarioDto loginDto)
        {

          
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u =>
                    u.NombreUsuario == loginDto.Credencial ||
                    u.Correo == loginDto.Credencial);

      
            if (usuario == null) return null;

          
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, usuario.Password);
            if (!isPasswordValid) return null;

            
            return _mapper.Map<GetUsuarioAuthDto>(usuario);
        }


        public async Task<GetUsuarioDto> CreateUsuarioAsync(UpdateUsuarioDto newUsuario)
        {

            try
            {
                
                var usuarioEntity = _mapper.Map<Usuario>(newUsuario);

                var usuarioExistente = await _context.Usuarios
                    .AnyAsync(u => u.NombreUsuario == newUsuario.NombreUsuario || u.Correo == newUsuario.Correo);

                if (usuarioExistente)
                {
                    throw new InvalidOperationException("El nombre de usuario o correo ya está en uso.");
                }

                
                usuarioEntity.Password = BCrypt.Net.BCrypt.HashPassword(newUsuario.Password);

             
                await _context.Usuarios.AddAsync(usuarioEntity);
                await _context.SaveChangesAsync();

               
                return _mapper.Map<GetUsuarioDto>(usuarioEntity);
            }
            catch (InvalidOperationException ex)
            {
              
                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception ex)
            {
                
                throw new Exception("Ocurrió un error al crear el usuario", ex);
            }
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<GetUsuarioDto?> GetUsuarioByIdAsync(int id)
        {

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(s => s.UsuarioId == id);

            if (usuario == null)
            {
                return null;
            }

            return _mapper.Map<GetUsuarioDto>(usuario);
        }

        public async Task<IEnumerable<GetUsuarioDto>> GetUsuariosAsync()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return _mapper.Map<IEnumerable<GetUsuarioDto>>(usuarios);
        }

        public async Task<bool> UpdateUsuarioAsync(int id, UpdateUsuarioDto updateUsuario)
        {
            try
            {
                var existingUsuario = await _context.Usuarios.FindAsync(id);
                if (existingUsuario == null)
                {
                    return false;
                }


                _mapper.Map(updateUsuario, existingUsuario);

                existingUsuario.Password = BCrypt.Net.BCrypt.HashPassword(updateUsuario.Password);

                _context.Entry(existingUsuario).State = EntityState.Modified;


                await _context.SaveChangesAsync();

                return true;
            }

            catch (DbUpdateException dbEx)
            {

                Console.WriteLine($"Error de actualización en la base de datos: {dbEx.Message}");
                return false;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return false;
            }
        }
    }
}

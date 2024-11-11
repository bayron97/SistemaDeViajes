using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDeViajes.DTOs;
using SistemaDeViajes.Interfaces;

namespace SistemaDeViajes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuarioService;

        public UsuarioController(IUsuario usuarioService)
        {

            _usuarioService = usuarioService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioDto loginDto)
        {
            try
            {
                var usuarioAuth = await _usuarioService.LoginAsync(loginDto);

                if (usuarioAuth == null)
                {
                    return Unauthorized(new { message = "Credenciales inválidas" });
                }

                return Ok(usuarioAuth);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error en el servidor.");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);

            if (usuario == null)
                return NotFound("Usuario no encontrado");

            return Ok(usuario);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] UpdateUsuarioDto newUsuario)
        {
            try
            {
                var usuarioCreado = await _usuarioService.CreateUsuarioAsync(newUsuario);
                return CreatedAtAction(nameof(GetUsuarioById), new { id = usuarioCreado.UsuarioId }, usuarioCreado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UpdateUsuarioDto updatedUsuario)
        {
            try
            {

                var success = await _usuarioService.UpdateUsuarioAsync(id, updatedUsuario);

                if (!success)
                {
                    return NotFound();
                }

                return NoContent();

            }

            catch (DbUpdateException dbEx)
            {

                return StatusCode(500, "Error al actualizar el usuario en la base de datos.");
            }

            catch (Exception ex)
            {

                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var success = await _usuarioService.DeleteUsuarioAsync(id);
            if (!success)
                return NotFound($"No se encontró el usuario con ID {id}.");

            return NoContent();
        }
    }
}

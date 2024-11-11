using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDeViajes.DTOs;
using SistemaDeViajes.Interfaces;

namespace SistemaDeViajes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalController : ControllerBase
    {
        private readonly ISucursale _sucursalService;

        public SucursalController(ISucursale sucursalService)
        {

            _sucursalService = sucursalService;

        }

        [HttpGet]
        public async Task<IActionResult> GetSucursales()
        {
            var sucursales = await _sucursalService.GetSucursalesAsync();
            return Ok(sucursales);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSucursalById(int id)
        {
            var sucursal = await _sucursalService.GetSucursalByIdAsync(id);

            if (sucursal == null)
                return NotFound("Sucursal no encontrada");

            return Ok(sucursal);
        }


        [HttpPost]
        public async Task<IActionResult> CreateSucursal([FromBody] UpdateSucursalDto newSucursal)
        {
            try
            {
                var createdSucursal = await _sucursalService.CreateSucursalAsync(newSucursal);
                return CreatedAtAction(nameof(GetSucursalById), new { id = createdSucursal.SucursalId }, createdSucursal);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSucursal(int id, [FromBody] UpdateSucursalDto updatedSucursal)
        {

            try
            {

                var success = await _sucursalService.UpdateSucursalAsync(id, updatedSucursal);

                if (!success)
                {
                    return NotFound();
                }

                return NoContent();

            }


            catch (DbUpdateException dbEx)
            {

                return StatusCode(500, "Error al actualizar la sucursal en la base de datos.");
            }

            catch (Exception ex)
            {

                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSucursal(int id)
        {
            var success = await _sucursalService.DeleteSucursalAsync(id);
            if (!success)
                return NotFound($"No se encontró la sucursal con ID {id}.");

            return NoContent();
        }
    }
}

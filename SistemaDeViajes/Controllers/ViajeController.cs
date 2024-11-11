using Microsoft.AspNetCore.Mvc;
using SistemaDeViajes.DTOs;
using SistemaDeViajes.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ViajeController : ControllerBase
{
    private readonly IViaje _viaje;

    public ViajeController(IViaje viaje)
    {
        _viaje = viaje;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetViajeDto>>> GetViajes()
    {
        return Ok(await _viaje.GetViajesAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetViajeDto>> GetViajeById(int id)
    {
        var viaje = await _viaje.GetViajeByIdAsync(id);
        if (viaje == null) return NotFound("Viaje no encontrado");
        return Ok(viaje);
    }

    [HttpPost]
    public async Task<ActionResult<GetViajeDto>> CreateViaje(UpdateViajeDto newViaje)
    {
        try
        {
           
            var viajeCreado = await _viaje.CreateViajeAsync(newViaje);
            return CreatedAtAction(nameof(GetViajeById), new { id = viajeCreado.ViajeId }, viajeCreado);
        }
        catch (UnauthorizedAccessException ex)
        {
            
            return StatusCode(403, new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ocurrió un error inesperado." });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateViaje(int id, UpdateViajeDto updatedViaje)
    {
        var success = await _viaje.UpdateViajeAsync(id, updatedViaje);
        if (!success) return NotFound("Viaje no encontrado");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteViaje(int id)
    {
        var success = await _viaje.DeleteViajeAsync(id);
        if (!success) return NotFound("Viaje no encontrado");
        return NoContent();
    }
}

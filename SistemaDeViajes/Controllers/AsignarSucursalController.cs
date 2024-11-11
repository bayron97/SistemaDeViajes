using Microsoft.AspNetCore.Mvc;
using SistemaDeViajes.DTOs;
using SistemaDeViajes.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class AsignarSucursaleController : ControllerBase
{
    private readonly IAsignarSucursal _asignarSucursal;

    public AsignarSucursaleController(IAsignarSucursal asignarSucursal)
    {
        _asignarSucursal = asignarSucursal;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAsignarSucursalDto>>> GetAsignaciones()
    {
        return Ok(await _asignarSucursal.GetAsignacionesAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetAsignarSucursalDto>> GetAsignacionById(int id)
    {
        var asignacion = await _asignarSucursal.GetAsignacionByIdAsync(id);
        if (asignacion == null)
            return NotFound();
        return Ok(asignacion);
    }

    [HttpPost]
    public async Task<ActionResult<GetAsignarSucursalDto>> CreateAsignacion(UpdateAsignarSucursalDto newAsignacion)
    {
        try
        {
            var asignacionCreada = await _asignarSucursal.CreateAsignacionAsync(newAsignacion);
            return CreatedAtAction(nameof(GetAsignacionById), new { id = asignacionCreada.AsignacionId }, asignacionCreada);
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
    public async Task<IActionResult> UpdateAsignacion(int id, UpdateAsignarSucursalDto updatedAsignacion)
    {
        var success = await _asignarSucursal.UpdateAsignacionAsync(id, updatedAsignacion);
        if (!success)
            return NotFound("Asignacion no encontrada");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsignacion(int id)
    {
        var success = await _asignarSucursal.DeleteAsignacionAsync(id);
        if (!success)
            return NotFound();
        return NoContent();
    }
}

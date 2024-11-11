using Microsoft.AspNetCore.Mvc;
using SistemaDeViajes.DTOs;
using SistemaDeViajes.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class EmpleadoController : ControllerBase
{
    private readonly IEmpleado _empleado;

    public EmpleadoController(IEmpleado empleado)
    {
        _empleado = empleado;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetEmpleadoDto>>> GetEmpleados()
    {
        return Ok(await _empleado.GetEmpleadosAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetEmpleadoDto>> GetEmpleadoById(int id)
    {
        var empleado = await _empleado.GetEmpleadoByIdAsync(id);
        if (empleado == null)
            return NotFound("Empleado no encontrado");
        return Ok(empleado);
    }

    [HttpPost]
    public async Task<ActionResult<GetEmpleadoDto>> CreateEmpleado(UpdateEmpleadoDto newEmpleado)
    {
        try
        {
            var createdEmpleado = await _empleado.CreateEmpleadoAsync(newEmpleado);
            return CreatedAtAction(nameof(GetEmpleadoById), new { id = createdEmpleado.EmpleadoId }, createdEmpleado);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmpleado(int id, UpdateEmpleadoDto updatedEmpleado)
    {
        var success = await _empleado.UpdateEmpleadoAsync(id, updatedEmpleado);
        if (!success)
            return NotFound("Empleado no encontrado");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmpleado(int id)
    {
        var success = await _empleado.DeleteEmpleadoAsync(id);
        if (!success)
            return NotFound("Empleado no encontrado");
        return NoContent();
    }
}

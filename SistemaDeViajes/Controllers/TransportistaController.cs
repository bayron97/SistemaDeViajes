using Microsoft.AspNetCore.Mvc;
using SistemaDeViajes.DTOs;

[ApiController]
[Route("api/[controller]")]
public class TransportistaController : ControllerBase
{
    private readonly ITransportista _transportista;

    public TransportistaController(ITransportista transportista)
    {
        _transportista = transportista;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetTransportistaDto>>> GetTransportistas()
    {
        return Ok(await _transportista.GetTransportistasAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetTransportistaDto>> GetTransportistaById(int id)
    {
        var transportista = await _transportista.GetTransportistaByIdAsync(id);
        if (transportista == null)
            return NotFound("Transportista no encontrado");
        return Ok(transportista);
    }

    [HttpPost]
    public async Task<ActionResult<GetTransportistaDto>> CreateTransportista(UpdateTransportistaDto newTransportista)
    {
        try
        {
            var createdTransportista = await _transportista.CreateTransportistaAsync(newTransportista);
            return CreatedAtAction(nameof(GetTransportistaById), new { id = createdTransportista.TransportistaId }, createdTransportista);

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
    public async Task<IActionResult> UpdateTransportista(int id, UpdateTransportistaDto updatedTransportista)
    {
        var success = await _transportista.UpdateTransportistaAsync(id, updatedTransportista);
        if (!success) return NotFound("Transportista no encontrado");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransportista(int id)
    {
        var success = await _transportista.DeleteTransportistaAsync(id);
        if (!success) return NotFound("Transportista no encontrado");
        return NoContent();
    }
}

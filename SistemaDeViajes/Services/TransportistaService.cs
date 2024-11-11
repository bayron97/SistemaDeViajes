using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaDeViajes.Context;
using SistemaDeViajes.DTOs;
using SistemaDeViajes.Models;

public class TransportistaService : ITransportista
{
    private readonly SiViajeContext _context;
    private readonly IMapper _mapper;

    public TransportistaService(SiViajeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetTransportistaDto> CreateTransportistaAsync(UpdateTransportistaDto newTransportista)
    {
        var transportistaEntity = _mapper.Map<Transportista>(newTransportista);

       
        var telefonoExistente = await _context.Transportistas
            .AnyAsync(u => u.Telefono == newTransportista.Telefono);

        if (telefonoExistente)
        {
            throw new InvalidOperationException("El número de teléfono ya está en uso.");
        }

        await _context.Transportistas.AddAsync(transportistaEntity);
        await _context.SaveChangesAsync();
        return _mapper.Map<GetTransportistaDto>(transportistaEntity);
    }

    public async Task<bool> DeleteTransportistaAsync(int id)
    {
        var transportistaEntity = await _context.Transportistas.FirstOrDefaultAsync(t => t.TransportistaId == id);
        if (transportistaEntity == null) return false;

        _context.Transportistas.Remove(transportistaEntity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<GetTransportistaDto> GetTransportistaByIdAsync(int id)
    {
        var transportista = await _context.Transportistas.FirstOrDefaultAsync(t => t.TransportistaId == id);
        return _mapper.Map<GetTransportistaDto>(transportista);
    }

    public async Task<IEnumerable<GetTransportistaDto>> GetTransportistasAsync()
    {
        var transportistas = await _context.Transportistas.ToListAsync();
        return _mapper.Map<IEnumerable<GetTransportistaDto>>(transportistas);
    }

    public async Task<bool> UpdateTransportistaAsync(int id, UpdateTransportistaDto updatedTransportista)
    {
        var transportistaEntity = await _context.Transportistas.FirstOrDefaultAsync(t => t.TransportistaId == id);
        if (transportistaEntity == null) return false;

        _mapper.Map(updatedTransportista, transportistaEntity);
        await _context.SaveChangesAsync();
        return true;
    }

}

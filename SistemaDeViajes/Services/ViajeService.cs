using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaDeViajes.Context;
using SistemaDeViajes.DTOs;
using SistemaDeViajes.Interfaces;
using SistemaDeViajes.Models;

public class ViajeService : IViaje
{
    private readonly SiViajeContext _context;
    private readonly IMapper _mapper;

    public ViajeService(SiViajeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetViajeDto> CreateViajeAsync(UpdateViajeDto newViaje)
    {
        
       var usuario = await _context.Usuarios
    .Where(u => u.UsuarioId == newViaje.UsuarioId)
    .Select(u => u.Rol)
    .FirstOrDefaultAsync();

        if (usuario == null)
        {
            throw new InvalidOperationException("Usuario no encontrado.");
        }

        
        if (usuario != "Gerente de tienda")
        {
            throw new UnauthorizedAccessException("Solo los Gerentes de tienda pueden registrar viajes.");
        }


        
        if (newViaje.TotalDistancia > 100)
            throw new InvalidOperationException("La suma de las distancias no puede superar los 100 km.");

        
        var fechaViaje = newViaje.Fecha.Date;
        foreach (var detalle in newViaje.DetalleViajes)
        {
            bool colaboradorYaViajado = await _context.DetalleViajes
                .AnyAsync(d => d.EmpleadoId == detalle.EmpleadoId && d.Viaje.Fecha.Date == fechaViaje);

            if (colaboradorYaViajado)
            {
                throw new InvalidOperationException($"El colaborador con ID {detalle.EmpleadoId} ya tiene un viaje registrado en esta fecha.");
            }
        }

        var viajeEntity = _mapper.Map<Viaje>(newViaje);

        await _context.Viajes.AddAsync(viajeEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<GetViajeDto>(viajeEntity);
    }

    public async Task<IEnumerable<GetViajeDto>> GetViajesAsync()
    {
        var viajes = await _context.Viajes
            .Include(v => v.DetalleViajes)
            .ToListAsync();
        return _mapper.Map<IEnumerable<GetViajeDto>>(viajes);
    }

    public async Task<GetViajeDto> GetViajeByIdAsync(int id)
    {
        var viaje = await _context.Viajes
            .Include(v => v.DetalleViajes)
            .FirstOrDefaultAsync(v => v.ViajeId == id);

        return _mapper.Map<GetViajeDto>(viaje);
    }

    public async Task<bool> UpdateViajeAsync(int id, UpdateViajeDto updatedViaje)
    {
        var viajeEntity = await _context.Viajes
            .Include(v => v.DetalleViajes)
            .FirstOrDefaultAsync(v => v.ViajeId == id);

        if (viajeEntity == null) return false;

        _mapper.Map(updatedViaje, viajeEntity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteViajeAsync(int id)
    {
        var viaje = await _context.Viajes.FirstOrDefaultAsync(v => v.ViajeId == id);
        if (viaje == null) return false;

        _context.Viajes.Remove(viaje);
        await _context.SaveChangesAsync();
        return true;
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaDeViajes.Context;
using SistemaDeViajes.DTOs;
using SistemaDeViajes.Interfaces;
using SistemaDeViajes.Models;

public class AsignarSucursaleService : IAsignarSucursal
{
    private readonly SiViajeContext _context;
    private readonly IMapper _mapper;

    public AsignarSucursaleService(SiViajeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAsignarSucursalDto>> GetAsignacionesAsync()
    {
        var asignaciones = await _context.AsignarSucursales
            .Include(a => a.Empleado)
            .Include(a => a.Sucursal)
            .ToListAsync();

        return _mapper.Map<IEnumerable<GetAsignarSucursalDto>>(asignaciones);
    }

    public async Task<GetAsignarSucursalDto> GetAsignacionByIdAsync(int id)
    {
        var asignacion = await _context.AsignarSucursales
            .Include(a => a.Empleado)
            .Include(a => a.Sucursal)
            .FirstOrDefaultAsync(a => a.AsignacionId == id);

        return _mapper.Map<GetAsignarSucursalDto>(asignacion);
    }

    public async Task<GetAsignarSucursalDto> CreateAsignacionAsync(UpdateAsignarSucursalDto newAsignacion)
    {
        var asignacionExistente = await _context.AsignarSucursales
        .AnyAsync(a => a.EmpleadoId == newAsignacion.EmpleadoId && a.SucursalId == newAsignacion.SucursalId);

        if (asignacionExistente)
        {
            throw new InvalidOperationException("El empleado ya tiene esta sucursal asignada.");
        }

        
        if (newAsignacion.DistanciaKm <= 0 || newAsignacion.DistanciaKm > 50)
        {
            throw new InvalidOperationException("La distancia debe estar entre 1 y 50 km.");
        }

        var asignacionEntity = _mapper.Map<AsignarSucursale>(newAsignacion);
        await _context.AsignarSucursales.AddAsync(asignacionEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<GetAsignarSucursalDto>(asignacionEntity);
    }

    public async Task<bool> UpdateAsignacionAsync(int id, UpdateAsignarSucursalDto updatedAsignacion)
    {
        var asignacionEntity = await _context.AsignarSucursales.FirstOrDefaultAsync(a => a.AsignacionId == id);
        if (asignacionEntity == null)
            return false;

        _mapper.Map(updatedAsignacion, asignacionEntity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsignacionAsync(int id)
    {
        var asignacionEntity = await _context.AsignarSucursales.FirstOrDefaultAsync(a => a.AsignacionId == id);
        if (asignacionEntity == null)
            return false;

        _context.AsignarSucursales.Remove(asignacionEntity);
        await _context.SaveChangesAsync();
        return true;
    }
}

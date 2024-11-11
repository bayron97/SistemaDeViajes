using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaDeViajes.Context;
using SistemaDeViajes.DTOs;
using SistemaDeViajes.Interfaces;
using SistemaDeViajes.Models;

public class EmpleadoService : IEmpleado
{
    private readonly SiViajeContext _context;

    private readonly IMapper _mapper;

    public EmpleadoService(SiViajeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<GetEmpleadoDto> CreateEmpleadoAsync(UpdateEmpleadoDto newEmpleado)
    {
        var empleadoEntity = _mapper.Map<Empleado>(newEmpleado);

  
        var telefonoExistente = await _context.Empleados
            .AnyAsync(u => u.Telefono == newEmpleado.Telefono);

        if (telefonoExistente)
        {
            throw new InvalidOperationException("El número de teléfono ya está en uso.");
        }
        await _context.Empleados.AddAsync(empleadoEntity);
        await _context.SaveChangesAsync();
        return _mapper.Map<GetEmpleadoDto>(empleadoEntity);
    }


    public async Task<bool> DeleteEmpleadoAsync(int id)
    {
        var empleadoEntity = await _context.Empleados.FirstOrDefaultAsync(e => e.EmpleadoId == id);
        if (empleadoEntity == null)
            return false;

        _context.Empleados.Remove(empleadoEntity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<GetEmpleadoDto> GetEmpleadoByIdAsync(int id)
    {
        var empleado = await _context.Empleados.FirstOrDefaultAsync(e => e.EmpleadoId == id);
        return _mapper.Map<GetEmpleadoDto>(empleado);
    }

    public async Task<IEnumerable<GetEmpleadoDto>> GetEmpleadosAsync()
    {
        var empleados = await _context.Empleados.ToListAsync();
        return _mapper.Map<IEnumerable<GetEmpleadoDto>>(empleados);
    }

    public async Task<bool> UpdateEmpleadoAsync(int id, UpdateEmpleadoDto updatedEmpleado)
    {
        var empleadoEntity = await _context.Empleados.FirstOrDefaultAsync(e => e.EmpleadoId == id);
        if (empleadoEntity == null)
            return false;

        _mapper.Map(updatedEmpleado, empleadoEntity);
        await _context.SaveChangesAsync();
        return true;
    }

}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaDeViajes.Context;
using SistemaDeViajes.DTOs;
using SistemaDeViajes.Interfaces;
using SistemaDeViajes.Models;

namespace SistemaDeViajes.Services
{
    public class SucursaleService : ISucursale
    {
        private readonly SiViajeContext _context;

        private readonly IMapper _mapper;

        public SucursaleService(SiViajeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetSucursalDto> CreateSucursalAsync(UpdateSucursalDto newSucursal)
        {

            var sucursalEntity = _mapper.Map<Sucursale>(newSucursal);

            var sucursalExistente = await _context.Sucursales
       .AnyAsync(u => u.Nombre == newSucursal.Nombre);

            if (sucursalExistente)
            {
                throw new InvalidOperationException("El nombre de la sucursal ya está en uso.");
            }


            await _context.Sucursales.AddAsync(sucursalEntity);
            await _context.SaveChangesAsync();


            return _mapper.Map<GetSucursalDto>(sucursalEntity);
        }

        public async Task<bool> DeleteSucursalAsync(int id)
        {

            var sucursal = await _context.Sucursales.FindAsync(id);
            if (sucursal == null)
                return false;

            _context.Sucursales.Remove(sucursal);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DTOs.GetSucursalDto?> GetSucursalByIdAsync(int id)
        {
            var sucursal = await _context.Sucursales.FirstOrDefaultAsync(s => s.SucursalId == id);

            if (sucursal == null)
            {
                return null;
            }

            return _mapper.Map<GetSucursalDto>(sucursal);

        }

        public async Task<IEnumerable<GetSucursalDto>> GetSucursalesAsync()
        {

            var sucursales = await _context.Sucursales.ToListAsync();
            return _mapper.Map<IEnumerable<GetSucursalDto>>(sucursales);
        }

        public async Task<bool> UpdateSucursalAsync(int id, DTOs.UpdateSucursalDto updatedSucursal)
        {

            try
            {
                var existingSucursal = await _context.Sucursales.FindAsync(id);
                if (existingSucursal == null)
                {
                    return false;
                }


                _mapper.Map(updatedSucursal, existingSucursal);

                _context.Entry(existingSucursal).State = EntityState.Modified;


                await _context.SaveChangesAsync();

                return true;
            }

            catch (DbUpdateException dbEx)
            {

                Console.WriteLine($"Error de actualización en la base de datos: {dbEx.Message}");
                return false;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return false;
            }
        }

    }
}

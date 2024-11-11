using AutoMapper;
using SistemaDeViajes.DTOs;
using SistemaDeViajes.Models;

namespace SistemaDeViajes
{
    public class MappingProfile : Profile
    {
       public MappingProfile()
        {
            CreateMap<UpdateSucursalDto,Sucursale >();
            CreateMap<Sucursale, GetSucursalDto > ();
            CreateMap<UpdateUsuarioDto, Usuario>();
            CreateMap<Usuario, GetUsuarioDto>();
            CreateMap<Usuario, GetUsuarioAuthDto>();
            CreateMap<UpdateEmpleadoDto, Empleado>();
            CreateMap<Empleado, GetEmpleadoDto>();
            CreateMap<UpdateTransportistaDto, Transportista>();
            CreateMap<Transportista, GetTransportistaDto>();
            CreateMap<AsignarSucursale, GetAsignarSucursalDto>()
            .ForMember(dest => dest.NombreEmpleado, opt => opt.MapFrom(src => src.Empleado.NombreEmpleado))
            .ForMember(dest => dest.NombreSucursal, opt => opt.MapFrom(src => src.Sucursal.Nombre));

            CreateMap<UpdateAsignarSucursalDto, AsignarSucursale>();
            CreateMap<UpdateViajeDto, Viaje>();
            CreateMap<Viaje, GetViajeDto>();
            CreateMap<UpdateDetalleViajeDto, DetalleViaje>();
            CreateMap<DetalleViaje, GetDetalleViajeDto>();


        }
    }
}

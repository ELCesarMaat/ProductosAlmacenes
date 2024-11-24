using AutoMapper;
using ProductosAlmacenes.DTOs.Almacen;
using ProductosAlmacenes.DTOs.Movimiento;
using ProductosAlmacenes.DTOs.Producto;
using ProductosAlmacenes.DTOs.Ubicacion;
using ProductosAlmacenes.Model;

namespace ProductosAlmacenes.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Almacen, CreadoAlmacenDTO>();
            CreateMap<NuevoAlmacenDTO, Almacen>();
            CreateMap<UbicacionAlmacen, UbicacionDTO>();

            CreateMap<NuevoUbicacionDTO, UbicacionAlmacen>();

            CreateMap<NuevoProductoDTO, Producto>();
            CreateMap<Producto, CreadoProductoDTO>();

            CreateMap<NuevoMovimientoDTO, Movimiento>();
            CreateMap<Movimiento, CreadoMovimientoDTO>()
                .ForMember(dest => dest.NombreProducto, opt => opt.MapFrom(src => src.Producto.Nombre))
                .ForMember(dest => dest.NombreAlmacen, opt => opt.MapFrom(src => src.Ubicacion.Almacen.Nombre))
                .ForMember(dest => dest.TipoMovimiento, opt => opt.MapFrom(src => src.TipoMovimiento.Nombre));


        }
    }
}

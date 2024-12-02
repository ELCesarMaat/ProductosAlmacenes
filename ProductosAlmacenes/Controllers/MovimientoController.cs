using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductosAlmacenes.DTOs.Movimiento;
using ProductosAlmacenes.DTOs.Producto;

namespace ProductosAlmacenes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController(AlmacenDbContext db, IMapper mapper) : ControllerBase
    {
        private readonly AlmacenDbContext _dbContext = db;
        private readonly IMapper _mapper = mapper;

        [HttpPost("NuevoMovimiento")]
        public async Task<ActionResult<CreadoMovimientoDTO>> NuevoMovimiento(NuevoMovimientoDTO pMovimiento)
        {
            //1 Entrada
            //2 Salida
            var nuevoMovimiento = _mapper.Map<Movimiento>(pMovimiento);
            _dbContext.Movimientos.Add(nuevoMovimiento);
            await _dbContext.SaveChangesAsync();

            nuevoMovimiento = _dbContext.Movimientos
                .Include(m => m.Ubicacion)
                .ThenInclude(ubicacion => ubicacion.Almacen)
                .Include(m => m.Producto)
                .Include(movimiento => movimiento.TipoMovimiento)
                .FirstOrDefault(m => m.MovimientoID == nuevoMovimiento.MovimientoID);

            if (nuevoMovimiento == null)
                return BadRequest("Error al guardar");

            var creadoMov = new CreadoMovimientoDTO
            {
                Cantidad = nuevoMovimiento.Cantidad,
                FechaActualizacion = DateTime.Now,
                MovimientoID = nuevoMovimiento.MovimientoID,
                TipoMovimiento = nuevoMovimiento.TipoMovimiento.Nombre,
                UbicacionID = nuevoMovimiento.UbicacionID,
                NombreAlmacen = nuevoMovimiento.Ubicacion.Almacen.Nombre,
                NombreProducto = nuevoMovimiento.Producto.Nombre
            };

            return Ok(creadoMov);
        }

        [HttpPost("ObtenerMovimientosDeUnAlmacen")]
        public async Task<ActionResult<List<CreadoMovimientoDTO>>> MovimientosDeUnAlmacen(int IdAlmacen,
            DateTime pInicio, DateTime pFin)
        {
            var movimientos = await _dbContext.Movimientos
                .Include(m => m.Ubicacion)
                .ThenInclude(u => u.Almacen)
                .Include(m => m.Producto)
                .Include(m => m.TipoMovimiento)
                .Where(x => x.Ubicacion.AlmacenID == IdAlmacen && x.Ubicacion.Activo &&
                            x.FechaActualizacion >= pInicio && x.FechaActualizacion <= pFin)
                .OrderBy(m => m.FechaActualizacion)
                .ToListAsync();

            return Ok(_mapper.Map<List<CreadoMovimientoDTO>>(movimientos));
        }

        [HttpPost("ObtenerMovimientos")]
        public async Task<ActionResult<List<CreadoMovimientoDTO>>> ObtenerMovimientos(DateTime pInicio,
            DateTime pFin)
        {
            var movimientos = await _dbContext.Movimientos
                .Include(m => m.Ubicacion)
                .ThenInclude(u => u.Almacen)
                .Include(m => m.Producto)
                .Include(m => m.TipoMovimiento)
                .Where(x => x.Ubicacion.Activo &&
                            x.FechaActualizacion >= pInicio && x.FechaActualizacion <= pFin)
                .OrderBy(m => m.FechaActualizacion)
                .ToListAsync();
            return Ok(_mapper.Map<List<CreadoMovimientoDTO>>(movimientos));
        }
        [HttpGet("obtenerexistenciasdeproducto")]
        public async Task<ActionResult<List<ExistenciasPorAlmacenDTO>>> ObtenerExistenciasPorProducto(int productoId)
        {
            // Obtener los movimientos para el producto especificado
            var movimientos = await _dbContext.Movimientos
                .Include(m => m.Ubicacion)
                .ThenInclude(u => u.Almacen) // Incluimos la información del almacén
                .Where(m => m.ProductoID == productoId && m.Ubicacion.Activo) // Filtramos por el ProductoID y ubicaciones activas
                .ToListAsync();

            // Agrupar los movimientos por AlmacenID y calcular las existencias
            var existenciasPorAlmacen = movimientos
                .GroupBy(m => m.Ubicacion.AlmacenID)
                .Select(group => new ExistenciasPorAlmacenDTO
                {
                    AlmacenID = group.Key,
                    AlmacenNombre = group.FirstOrDefault()?.Ubicacion.Almacen.Nombre,
                    ExistenciaTotal = group.Sum(m => m.TipoMovimientoID== 1? m.Cantidad : -m.Cantidad) // Si es entrada, sumamos; si es salida, restamos
                })
                .ToList();

            return Ok(existenciasPorAlmacen);
        }

    }
    public class ExistenciasPorAlmacenDTO
    {
        public int AlmacenID { get; set; }
        public string AlmacenNombre { get; set; }
        public int ExistenciaTotal { get; set; }
    }

}
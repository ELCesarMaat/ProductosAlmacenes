using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductosAlmacenes.DTOs.Producto;
using ProductosAlmacenes.Model;

namespace ProductosAlmacenes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController(AlmacenDbContext db, IMapper mapper) : ControllerBase
    {
        private readonly AlmacenDbContext _dbContext = db;
        private readonly IMapper _mapper = mapper;


        [HttpPost("NuevoProducto")]
        public async Task<ActionResult<CreadoProductoDTO>> NuevoProducto(NuevoProductoDTO pProduct)
        {
            try
            {
                var nuevoProducto = _mapper.Map<Producto>(pProduct);
                _dbContext.Productos.Add(nuevoProducto);
                await _dbContext.SaveChangesAsync();

                var productoCreado = _mapper.Map<CreadoProductoDTO>(nuevoProducto);

                return Ok(productoCreado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerProductos")]
        public async Task<ActionResult<List<CreadoProductoDTO>>> NuevoProducto()
        {
            try
            {
                var productos = _dbContext.Productos.AsNoTracking();

                return Ok(_mapper.Map<List<CreadoProductoDTO>>(productos));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerProductoPorId")]
        public async Task<ActionResult<CreadoProductoDTO>> NuevoProducto(int productoId)
        {
            try
            {
                var producto = await _dbContext.Productos.FirstOrDefaultAsync(p => p.ProductoID == productoId);

                if (producto == null)
                    return Ok("Producto no encontrado");

                return Ok(_mapper.Map<CreadoProductoDTO>(producto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
using Microsoft.EntityFrameworkCore;
using ProductosAlmacenes.Model;

namespace ProductosAlmacenes
{
    public class AlmacenDbContext(DbContextOptions<AlmacenDbContext> options) : DbContext(options)
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Almacen> Almacenes { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }
        public DbSet<UbicacionAlmacen> UbicacionAlmacenes { get; set; }
        public DbSet<TipoMovimiento> TipoMovimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TipoMovimiento>().HasData(
                new TipoMovimiento { TipoMovimientoID = 1, Nombre = "Entrada" },
                new TipoMovimiento { TipoMovimientoID = 2, Nombre = "Salida" }
            );
        }
    }
}

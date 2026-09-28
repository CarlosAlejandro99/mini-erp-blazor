using Microsoft.EntityFrameworkCore;

namespace hola_mundo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<EmpleadoEntity> Empleados { get; set; }

    public DbSet<DepartamentoEntity> Departamentos { get; set; }

    public DbSet<PuestoEntity> Puestos { get; set; }
}

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

public class EmpleadoEntity
{
    public int Id { get; set; }

    public string NumeroEmpleado { get; set; } = "";

    public string Nombre { get; set; } = "";

    public string Genero { get; set; } = "";

    public DateTime? FechaCumpleanos { get; set; }

    public string CURP { get; set; } = "";

    public string Telefono { get; set; } = "";

    public string Puesto { get; set; } = "";

    public string Email { get; set; } = "";
}

public class DepartamentoEntity
{
    public int Id { get; set; }

    public string Nombre { get; set; } = "";

    public List<PuestoEntity> Puestos { get; set; } = new();
}

public class PuestoEntity
{
    public int Id { get; set; }

    public string Nombre { get; set; } = "";

    public int DepartamentoId { get; set; }

    public DepartamentoEntity? Departamento { get; set; }
}
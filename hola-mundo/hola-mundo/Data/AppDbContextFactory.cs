using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace hola_mundo.Data;

/// Solo la usan las herramientas de EF (Add-Migration, Update-Database, dotnet ef).
/// Lee la misma cadena "DefaultConnection" que la app, para no repetirla en el código.
/// Orden de prioridad: variable de entorno ConnectionStrings__DefaultConnection
/// &gt; appsettings.Development.json &gt; appsettings.json.
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "No se encontró la cadena de conexión 'DefaultConnection' en appsettings.json " +
                "ni en la variable de entorno ConnectionStrings__DefaultConnection.");

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}

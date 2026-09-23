using hola_mundo.Client.Models;

namespace hola_mundo.Client.Services;

public interface IEmpleadoService
{
    Task<List<Empleado>> ObtenerTodosAsync();

    Task AgregarAsync(Empleado empleado);

    Task<bool> ExisteNumeroEmpleadoAsync(string numeroEmpleado);
}
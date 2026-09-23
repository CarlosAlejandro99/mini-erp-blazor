using hola_mundo.Client.Models;

namespace hola_mundo.Client.Services;

public interface IDepartamentoService
{
    Task<List<Departamento>> ObtenerTodosAsync();

    Task AgregarAsync(Departamento departamento);
}
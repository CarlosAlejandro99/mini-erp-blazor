using hola_mundo.Client.Models;

namespace hola_mundo.Client.Services;

public interface IPuestoService
{
    Task<List<Puesto>> ObtenerTodosAsync();

    Task<List<Puesto>> ObtenerPorDepartamentoAsync(int departamentoId);

    Task AgregarAsync(Puesto puesto);
}

using hola_mundo.Client.Models;

namespace hola_mundo.Client.Services;

public interface IEmpleadoService
{
    List<Empleado> ObtenerTodos();

    void Agregar(Empleado empleado);
}
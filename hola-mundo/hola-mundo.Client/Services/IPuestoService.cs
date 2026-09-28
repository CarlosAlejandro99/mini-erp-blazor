using hola_mundo.Client.Models;

namespace hola_mundo.Client.Services;

// "Contrato" de lo que se puede hacer con los puestos desde la pantalla.
// La implementación real está en PuestoService.cs.
public interface IPuestoService
{
    // Trae todos los puestos de todos los departamentos
    Task<List<Puesto>> ObtenerTodosAsync();

    // Trae solo los puestos de un departamento (lo usa el formulario de empleados)
    Task<List<Puesto>> ObtenerPorDepartamentoAsync(int departamentoId);

    // Guarda un puesto nuevo dentro de un departamento
    Task AgregarAsync(Puesto puesto);
}
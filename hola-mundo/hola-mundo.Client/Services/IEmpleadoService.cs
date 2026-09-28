using hola_mundo.Client.Models;

namespace hola_mundo.Client.Services;


/// "Contrato" de lo que se puede hacer con los empleados desde la pantalla.
/// Las páginas y componentes solo conocen esta interfaz (no saben cómo se hace por dentro),
/// así que si mañana los datos vinieran de otro lado, no habría que tocar las pantallas.
/// La implementación real está en EmpleadoService.cs.

public interface IEmpleadoService
{
    ///Trae la lista completa de empleados    
    Task<List<Empleado>> ObtenerTodosAsync();

    /// Guarda un empleado nuevo
    Task AgregarAsync(Empleado empleado);

    /// Dice si ya existe un empleado con ese número (true = ya está ocupado)
    Task<bool> ExisteNumeroEmpleadoAsync(string numeroEmpleado);
}
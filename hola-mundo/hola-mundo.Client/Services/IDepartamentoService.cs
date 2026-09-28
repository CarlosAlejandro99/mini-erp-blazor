using hola_mundo.Client.Models;

namespace hola_mundo.Client.Services;

/// "Contrato" de lo que se puede hacer con los departamentos (áreas) desde la pantalla.
/// La implementación real está en DepartamentoService.cs.

public interface IDepartamentoService
{
    ///Trae todos los departamentos registrados
    Task<List<Departamento>> ObtenerTodosAsync();

    /// Guarda un departamento nuevo
    Task AgregarAsync(Departamento departamento);
}
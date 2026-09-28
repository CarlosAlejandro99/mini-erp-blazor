using System.Net.Http.Json;
using hola_mundo.Client.Models;

namespace hola_mundo.Client.Services;

/// El "mensajero" de los departamentos: le pide y le manda datos al servidor
/// (api/departamentos). Funciona igual que EmpleadoService, pero más sencillo.

public class DepartamentoService : IDepartamentoService
{
    // Herramienta para hacer peticiones web; Blazor la entrega sola (ver Program.cs).
    private readonly HttpClient _httpClient;

    public DepartamentoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    ///Pide la lista de departamentos al servidor
    public async Task<List<Departamento>> ObtenerTodosAsync()
    {
        // GET a "api/departamentos" y convierte el JSON recibido en List<Departamento>.
        var departamentos = await _httpClient.GetFromJsonAsync<List<Departamento>>(
            "api/departamentos"
        );

        // Si no llegó nada (null), se devuelve una lista vacía para evitar errores después.
        return departamentos ?? new List<Departamento>();
    }

    /// Manda un departamento nuevo al servidor para que lo guarde
    public async Task AgregarAsync(Departamento departamento)
    {
        // POST a "api/departamentos" mandando el departamento convertido a JSON.
        var response = await _httpClient.PostAsJsonAsync(
            "api/departamentos",
            departamento
        );

        // Si el servidor respondió con error (por ejemplo, "ese departamento ya existe"),
        // se lanza una excepción para que la pantalla pueda avisarle al usuario.
        response.EnsureSuccessStatusCode();
    }
}
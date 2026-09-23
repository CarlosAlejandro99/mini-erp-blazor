using System.Net.Http.Json;
using hola_mundo.Client.Models;

namespace hola_mundo.Client.Services;

public class EmpleadoService : IEmpleadoService
{
    private readonly HttpClient _httpClient;

    public EmpleadoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Empleado>> ObtenerTodosAsync()
    {
        var empleados = await _httpClient.GetFromJsonAsync<List<Empleado>>(
            "api/empleados"
        );

        return empleados ?? new List<Empleado>();
    }

    public async Task AgregarAsync(Empleado empleado)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/empleados",
            empleado
        );

        response.EnsureSuccessStatusCode();
    }

    public async Task<bool> ExisteNumeroEmpleadoAsync(string numeroEmpleado)
    {
        var existe = await _httpClient.GetFromJsonAsync<bool>(
            $"api/empleados/existe/{Uri.EscapeDataString(numeroEmpleado)}"
        );

        return existe;
    }
}
using System.Net.Http.Json;
using hola_mundo.Client.Models;

namespace hola_mundo.Client.Services;

public class DepartamentoService : IDepartamentoService
{
    private readonly HttpClient _httpClient;

    public DepartamentoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Departamento>> ObtenerTodosAsync()
    {
        var departamentos = await _httpClient.GetFromJsonAsync<List<Departamento>>(
            "api/departamentos"
        );

        return departamentos ?? new List<Departamento>();
    }

    public async Task AgregarAsync(Departamento departamento)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/departamentos",
            departamento
        );

        response.EnsureSuccessStatusCode();
    }
}
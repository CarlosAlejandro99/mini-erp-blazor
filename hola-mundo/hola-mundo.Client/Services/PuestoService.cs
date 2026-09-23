using System.Net.Http.Json;
using hola_mundo.Client.Models;

namespace hola_mundo.Client.Services;

public class PuestoService : IPuestoService
{
    private readonly HttpClient _httpClient;

    public PuestoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Puesto>> ObtenerTodosAsync()
    {
        var puestos = await _httpClient.GetFromJsonAsync<List<Puesto>>(
            "api/puestos"
        );

        return puestos ?? new List<Puesto>();
    }

    public async Task<List<Puesto>> ObtenerPorDepartamentoAsync(
        int departamentoId)
    {
        var puestos = await _httpClient.GetFromJsonAsync<List<Puesto>>(
            $"api/puestos/departamento/{departamentoId}"
        );

        return puestos ?? new List<Puesto>();
    }

    public async Task AgregarAsync(Puesto puesto)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/puestos",
            puesto
        );

        response.EnsureSuccessStatusCode();
    }
}
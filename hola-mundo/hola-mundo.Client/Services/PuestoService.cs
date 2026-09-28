using System.Net.Http.Json;
using hola_mundo.Client.Models;

namespace hola_mundo.Client.Services;

/// El "mensajero" de los puestos: le pide y le manda datos al servidor (api/puestos).

public class PuestoService : IPuestoService
{
    // Herramienta para hacer peticiones web; Blazor la entrega sola (ver Program.cs).
    private readonly HttpClient _httpClient;

    public PuestoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    
    //Pide todos los puestos, sin importar de qué departamento sean
    /// <returns></returns>
    public async Task<List<Puesto>> ObtenerTodosAsync()
    {
        var puestos = await _httpClient.GetFromJsonAsync<List<Puesto>>(
            "api/puestos"
        );

        // Si no llegó nada (null), se devuelve una lista vacía para evitar errores después.
        return puestos ?? new List<Puesto>();
    }

    /// Pide solo los puestos de un departamento. Se usa en el formulario de empleados:
    /// al elegir un área, se llama a esto para llenar la lista de puestos de esa área.
    
    public async Task<List<Puesto>> ObtenerPorDepartamentoAsync(
        int departamentoId)
    {
        // El Id va dentro de la dirección, por ejemplo "api/puestos/departamento/3".
        // No hace falta Uri.EscapeDataString porque es un número (int), no texto libre.
        var puestos = await _httpClient.GetFromJsonAsync<List<Puesto>>(
            $"api/puestos/departamento/{departamentoId}"
        );

        return puestos ?? new List<Puesto>();
    }

    /// Manda un puesto nuevo al servidor para que lo guarde
    public async Task AgregarAsync(Puesto puesto)
    {
        // Ojo: el puesto debe traer su DepartamentoId, porque el servidor rechaza
        // puestos que no pertenezcan a un departamento existente.
        var response = await _httpClient.PostAsJsonAsync(
            "api/puestos",
            puesto
        );

        // Lanza excepción si el servidor respondió con error (departamento inexistente,
        // puesto repetido, etc.), para que la pantalla avise en vez de fallar en silencio.
        response.EnsureSuccessStatusCode();
    }
}
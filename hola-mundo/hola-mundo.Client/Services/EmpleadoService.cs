using System.Net.Http.Json;
using hola_mundo.Client.Models;

namespace hola_mundo.Client.Services;

/// El "mensajero" de los empleados: no guarda nada por sí mismo, solo le hace
/// peticiones HTTP al servidor (api/empleados) y devuelve lo que este responde.

public class EmpleadoService : IEmpleadoService
{
    // HttpClient es la herramienta de .NET para hacer peticiones web.
    // Se pide en el constructor y Blazor la entrega sola (está registrada en Program.cs).
    // Ya viene configurada con la dirección base del sitio, por eso abajo basta con
    // escribir "api/empleados" y no la dirección completa.
    private readonly HttpClient _httpClient;

    public EmpleadoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    ///Pide al servidor todos los empleados y los convierte en objetos Empleado
    public async Task<List<Empleado>> ObtenerTodosAsync()
    {
        // GetFromJsonAsync hace dos cosas en una: manda un GET a "api/empleados" y
        // convierte el JSON que llega en una lista de Empleado.
        var empleados =
            await _httpClient.GetFromJsonAsync<List<Empleado>>(
                "api/empleados"
            );

        // Si el servidor respondió "vacío" (null), se regresa una lista vacía
        // en vez de null, para que quien la use no tenga que revisar si es null.
        //
        // NOTA: el servidor manda el correo con el nombre "email", pero el modelo Empleado
        // lo llama "Correo". Como los nombres no coinciden, el correo puede quedar vacío
        // al leer la lista. (Al guardar sí funciona, porque abajo se renombra a mano.)
        return empleados ?? new List<Empleado>();
    }

    /// Manda un empleado nuevo al servidor para que lo guarde en la base de datos
    public async Task AgregarAsync(Empleado empleado)
    {
        // Se arma un objeto "a la medida" con solo los datos que el servidor espera.
        // Sirve para dos cosas:
        //  1) NO se mandan DepartamentoId ni PuestoId, porque el servidor no los guarda
        //     (solo guarda el texto del puesto).
        //  2) "Email = empleado.Correo" renombra el campo: la pantalla le dice "Correo",
        //     pero el servidor lo espera con el nombre "Email".
        var empleadoParaEnviar = new
        {
            empleado.NumeroEmpleado,
            empleado.Nombre,
            empleado.Genero,
            empleado.FechaCumpleanos,
            empleado.CURP,
            empleado.Telefono,
            empleado.Puesto,
            Email = empleado.Correo
        };

        // PostAsJsonAsync convierte el objeto a JSON y lo manda con un POST a "api/empleados".
        var response =
            await _httpClient.PostAsJsonAsync(
                "api/empleados",
                empleadoParaEnviar
            );

        // Si el servidor respondió con error (400, 409, 500...), esta línea lanza una
        // excepción. Sin ella, un fallo pasaría en silencio y la pantalla creería
        // que el empleado sí se guardó.
        response.EnsureSuccessStatusCode();
    }

    /// Le pregunta al servidor si ese número de empleado ya está registrado
    public async Task<bool> ExisteNumeroEmpleadoAsync(
        string numeroEmpleado)
    {
        // Uri.EscapeDataString protege la dirección: si el número trajera un carácter
        // raro (espacio, "/", "?"...), se convierte a un formato seguro para que no
        // rompa la URL ni cambie a qué dirección se está llamando.
        var existe =
            await _httpClient.GetFromJsonAsync<bool>(
                $"api/empleados/existe/{Uri.EscapeDataString(numeroEmpleado)}"
            );

        return existe;
    }
}
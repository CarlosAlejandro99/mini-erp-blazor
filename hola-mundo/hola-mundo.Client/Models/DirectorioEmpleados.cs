namespace hola_mundo.Client.Models;

/// Guarda la lista de empleados de la página "Empleados" y toda la lógica de
/// búsqueda, cambio de vista (tarjetas/tabla) y paginación.
///
/// La idea es que "Empleados.razor" no tenga que calcular nada de esto: solo
/// crea un objeto de esta clase, le pasa la lista que trae del servicio, y en
/// el HTML usa sus propiedades (@Directorio.EmpleadosFiltrados, etc.) y sus
/// métodos (@onclick="Directorio.PaginaSiguiente").
public class DirectorioEmpleados
{
    /// Cuántas tarjetas/filas se muestran en cada página
    private const int EmpleadosPorPagina = 4;

    /// Lista completa de empleados, tal como la devolvió la base de datos (sin filtrar)
    private List<Empleado> _empleados;

    /// Texto que el usuario escribió en el buscador.
    private string _terminoBusqueda = string.Empty;

    /// Página en la que está el usuario ahora mismo (empieza en 1).
    private int _paginaActual = 1;

    public DirectorioEmpleados(List<Empleado> empleados)
    {
        _empleados = empleados;
    }

    ///Vista elegida: "tarjetas" o "tabla".
    public string VistaActual { get; set; } = "tarjetas";

    /// Cuántos empleados hay en total, sin importar la búsqueda.
    public int TotalEmpleados => _empleados.Count;

    /// Texto de búsqueda. Cada vez que el usuario escribe algo nuevo, 
    /// se encarga sola de regresar a la página 1 (si no, podrías quedar "atrapado"
    /// en una página que ya no existe porque la búsqueda dejó menos resultados).
    public string TerminoBusqueda
    {
        get => _terminoBusqueda;
        set
        {
            _terminoBusqueda = value ?? string.Empty;
            _paginaActual = 1;
        }
    }

    /// Página actual (de solo lectura desde afuera; se cambia con los métodos de abajo).
    public int PaginaActual => _paginaActual;

    /// Lista de empleados que coinciden con la búsqueda.
    /// Si no hay texto escrito, regresa a todos los empleados.
    public List<Empleado> EmpleadosFiltrados
    {
        get
        {
            var termino = _terminoBusqueda.Trim();

            // Sin texto de búsqueda -> se muestran todos, no hace falta filtrar.
            if (termino.Length == 0)
            {
                return _empleados;
            }

            // Con texto de búsqueda -> se revisa si el término aparece en
            // cualquiera de estos campos del empleado (sin importar mayúsculas/minúsculas).
            return _empleados
                .Where(e => new[] { e.Nombre, e.Puesto, e.Correo, e.NumeroEmpleado, e.CURP, e.Telefono, e.Genero }
                    .Any(campo => campo.Contains(termino, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }
    }

    //Cuántas páginas hacen falta para mostrar todos los resultados filtrados.
    public int TotalPaginas =>
        // Ceiling redondea hacia arriba: 5 empleados / 4 por página = 1.25 -> 2 páginas.
        // Math.Max evita que dé 0 páginas cuando no hay resultados (mejor mostrar "página 1 de 1").
        Math.Max(1, (int)Math.Ceiling(EmpleadosFiltrados.Count / (double)EmpleadosPorPagina));

    //Solo los empleados que le tocan a la página actual (ya filtrados).
    public List<Empleado> EmpleadosPaginados =>
        EmpleadosFiltrados
            .Skip((_paginaActual - 1) * EmpleadosPorPagina)  // se salta las páginas anteriores...
            .Take(EmpleadosPorPagina)                        // ...y toma solo lo que cabe en esta página.
            .ToList();

    ///En qué número de la lista completa empieza la página actual (para numerar filas en la tabla).
    public int InicioDePagina => (_paginaActual - 1) * EmpleadosPorPagina;

    /// Cuántos puestos distintos hay entre todos los empleados (para la tarjeta de estadísticas).
    public int PuestosUnicos =>
        _empleados.Select(e => e.Puesto).Distinct().Count();

    /// Reemplaza la lista de empleados (se usa después de agregar uno nuevo) y regresa a la página 1.
    public void ActualizarEmpleados(List<Empleado> empleados)
    {
        _empleados = empleados;
        _paginaActual = 1;
    }

    /// Borra el texto del buscador (el botón "✕" del buscador llama a esto).
    public void LimpiarBusqueda()
    {
        TerminoBusqueda = string.Empty;
    }

    /// Da el nombre de clase CSS de Bootstrap para resaltar el botón de vista que está activo.
    public string ClaseBoton(string vista) =>
        VistaActual == vista ? "btn-primary" : "btn-outline-primary";

    /// Retrocede una página, salvo que ya esté en la primera.
    public void PaginaAnterior()
    {
        if (_paginaActual > 1)
        {
            _paginaActual--;
        }
    }

    /// vanza una página, salvo que ya esté en la última.
    public void PaginaSiguiente()
    {
        if (_paginaActual < TotalPaginas)
        {
            _paginaActual++;
        }
    }
}

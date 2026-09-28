namespace hola_mundo.Client.Models;

/// Un puesto de trabajo (ej. "Analista de Sistemas"). Siempre pertenece a un Departamento, y cada Departamento puede tener muchos Puestos.
public class Puesto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = "";

    /// A qué departamento pertenece este puesto (para llenar el <c>&lt;select&gt;</c> en cascada del formulario)
    public int DepartamentoId { get; set; }
}
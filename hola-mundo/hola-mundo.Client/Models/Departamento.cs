namespace hola_mundo.Client.Models;

/// Un área de la empresa (ej. "Recursos Humanos", "Sistemas"). Cada empleado pertenece a una sola área, y cada área puede tener muchos empleados.
public class Departamento
{
    public int Id { get; set; }

    public string Nombre { get; set; } = "";
}
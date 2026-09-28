namespace hola_mundo.Data;

public class EmpleadoEntity
{
    public int Id { get; set; }

    public string NumeroEmpleado { get; set; } = "";

    public string Nombre { get; set; } = "";

    public string Genero { get; set; } = "";

    public DateTime? FechaCumpleanos { get; set; }

    public string CURP { get; set; } = "";

    public string Telefono { get; set; } = "";

    public string Puesto { get; set; } = "";

    public string Email { get; set; } = "";
}

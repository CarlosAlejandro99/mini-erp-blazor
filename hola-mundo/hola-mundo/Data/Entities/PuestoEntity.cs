namespace hola_mundo.Data;

public class PuestoEntity
{
    public int Id { get; set; }

    public string Nombre { get; set; } = "";

    public int DepartamentoId { get; set; }

    public DepartamentoEntity? Departamento { get; set; }
}

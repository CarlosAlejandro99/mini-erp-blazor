namespace hola_mundo.Data;

public class DepartamentoEntity
{
    public int Id { get; set; }

    public string Nombre { get; set; } = "";

    public List<PuestoEntity> Puestos { get; set; } = new();
}

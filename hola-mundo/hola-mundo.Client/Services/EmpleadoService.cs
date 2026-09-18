
using hola_mundo.Client.Models;

namespace hola_mundo.Client.Services;

public class EmpleadoService : IEmpleadoService
{
    private readonly List<Empleado> _empleados = new()
    {
        new Empleado
        {
            Nombre = "Carlos Núñez",
            Puesto = "Administrador",
            Correo = "carlos@empresa.com"
        },

        new Empleado
        {
            Nombre = "Ana López",
            Puesto = "Desarrolladora",
            Correo = "ana@empresa.com"
        },

        new Empleado
        {
            Nombre = "José Martínez",
            Puesto = "Soporte técnico",
            Correo = "jose@empresa.com"
        },

        new Empleado
        {
            Nombre = "María González",
            Puesto = "Analista",
            Correo = "maria@empresa.com"
        }
    };

    public List<Empleado> ObtenerTodos()
    {
        return _empleados;
    }

    public void Agregar(Empleado empleado)
    {
        _empleados.Add(empleado);
    }
}
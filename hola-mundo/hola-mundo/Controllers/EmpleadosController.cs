using hola_mundo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hola_mundo.Controllers;

[ApiController]
[Route("api/empleados")]
public class EmpleadosController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmpleadosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/empleados
    [HttpGet]
    public async Task<ActionResult<List<EmpleadoEntity>>> ObtenerTodos()
    {
        var empleados = await _context.Empleados
            .AsNoTracking()
            .OrderBy(e => e.Nombre)
            .ToListAsync();

        return Ok(empleados);
    }

    // GET: api/empleados/existe/1234
    [HttpGet("existe/{numeroEmpleado}")]
    public async Task<ActionResult<bool>> ExisteNumeroEmpleado(
        string numeroEmpleado)
    {
        var existe = await _context.Empleados
            .AnyAsync(e => e.NumeroEmpleado == numeroEmpleado);

        return Ok(existe);
    }

    // POST: api/empleados
    [HttpPost]
    public async Task<ActionResult<EmpleadoEntity>> Agregar(
        EmpleadoEntity empleado)
    {
        var camposObligatorios = new (string Valor, string Mensaje)[]
        {
            (empleado.NumeroEmpleado, "El número de empleado es obligatorio."),
            (empleado.Nombre, "El nombre es obligatorio."),
            (empleado.Genero, "El género es obligatorio."),
            (empleado.CURP, "La CURP es obligatoria."),
            (empleado.Telefono, "El teléfono es obligatorio."),
            (empleado.Puesto, "El puesto es obligatorio."),
            (empleado.Email, "El correo es obligatorio."),
        };

        foreach (var (valor, mensaje) in camposObligatorios)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return BadRequest(mensaje);
            }
        }

        empleado.NumeroEmpleado =
            empleado.NumeroEmpleado.Trim();

        empleado.Nombre =
            empleado.Nombre.Trim();

        empleado.Genero =
            empleado.Genero.Trim();

        empleado.CURP =
            empleado.CURP.Trim().ToUpper();

        empleado.Telefono =
            empleado.Telefono.Trim();

        empleado.Puesto =
            empleado.Puesto.Trim();

        empleado.Email =
            empleado.Email.Trim();

        var existe = await _context.Empleados
            .AnyAsync(e =>
                e.NumeroEmpleado ==
                empleado.NumeroEmpleado);

        if (existe)
        {
            return Conflict(
                "Ese número de empleado ya existe.");
        }

        _context.Empleados.Add(empleado);

        await _context.SaveChangesAsync();

        return Ok(empleado);
    }
}
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
        if (string.IsNullOrWhiteSpace(empleado.NumeroEmpleado))
        {
            return BadRequest(
                "El número de empleado es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(empleado.Nombre))
        {
            return BadRequest(
                "El nombre es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(empleado.Genero))
        {
            return BadRequest(
                "El género es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(empleado.CURP))
        {
            return BadRequest(
                "La CURP es obligatoria.");
        }

        if (string.IsNullOrWhiteSpace(empleado.Telefono))
        {
            return BadRequest(
                "El teléfono es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(empleado.Puesto))
        {
            return BadRequest(
                "El puesto es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(empleado.Email))
        {
            return BadRequest(
                "El correo es obligatorio.");
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
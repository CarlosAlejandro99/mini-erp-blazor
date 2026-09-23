using hola_mundo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hola_mundo.Controllers;

[ApiController]
[Route("api/departamentos")]
public class DepartamentosController : ControllerBase
{
    private readonly AppDbContext _context;

    public DepartamentosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/departamentos
    [HttpGet]
    public async Task<ActionResult<List<DepartamentoEntity>>> ObtenerTodos()
    {
        var departamentos = await _context.Departamentos
            .AsNoTracking()
            .OrderBy(d => d.Nombre)
            .ToListAsync();

        return Ok(departamentos);
    }

    // POST: api/departamentos
    [HttpPost]
    public async Task<ActionResult<DepartamentoEntity>> Agregar(
        DepartamentoEntity departamento)
    {
        if (string.IsNullOrWhiteSpace(departamento.Nombre))
        {
            return BadRequest("El nombre del departamento es obligatorio.");
        }

        departamento.Nombre = departamento.Nombre.Trim();

        var existe = await _context.Departamentos
            .AnyAsync(d => d.Nombre == departamento.Nombre);

        if (existe)
        {
            return Conflict("Ese departamento ya existe.");
        }

        _context.Departamentos.Add(departamento);

        await _context.SaveChangesAsync();

        return Ok(departamento);
    }
}
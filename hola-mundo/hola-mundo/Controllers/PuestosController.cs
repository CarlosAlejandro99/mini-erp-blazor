using hola_mundo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hola_mundo.Controllers;

[ApiController]
[Route("api/puestos")]
public class PuestosController : ControllerBase
{
    private readonly AppDbContext _context;

    public PuestosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/puestos
    [HttpGet]
    public async Task<ActionResult<List<PuestoEntity>>> ObtenerTodos()
    {
        var puestos = await _context.Puestos
            .AsNoTracking()
            .OrderBy(p => p.Nombre)
            .Select(p => new
            {
                p.Id,
                p.Nombre,
                p.DepartamentoId
            })
            .ToListAsync();

        return Ok(puestos);
    }

    // GET: api/puestos/departamento/1
    [HttpGet("departamento/{departamentoId:int}")]
    public async Task<ActionResult<List<PuestoEntity>>> ObtenerPorDepartamento(
        int departamentoId)
    {
        var puestos = await _context.Puestos
            .AsNoTracking()
            .Where(p => p.DepartamentoId == departamentoId)
            .OrderBy(p => p.Nombre)
            .Select(p => new
            {
                p.Id,
                p.Nombre,
                p.DepartamentoId
            })
            .ToListAsync();

        return Ok(puestos);
    }

    // POST: api/puestos
    [HttpPost]
    public async Task<ActionResult<PuestoEntity>> Agregar(
        PuestoEntity puesto)
    {
        if (string.IsNullOrWhiteSpace(puesto.Nombre))
        {
            return BadRequest("El nombre del puesto es obligatorio.");
        }

        if (puesto.DepartamentoId <= 0)
        {
            return BadRequest("Debes seleccionar un departamento.");
        }

        var departamentoExiste = await _context.Departamentos
            .AnyAsync(d => d.Id == puesto.DepartamentoId);

        if (!departamentoExiste)
        {
            return BadRequest("El departamento seleccionado no existe.");
        }

        puesto.Nombre = puesto.Nombre.Trim();

        var existe = await _context.Puestos
            .AnyAsync(p =>
                p.DepartamentoId == puesto.DepartamentoId &&
                p.Nombre == puesto.Nombre);

        if (existe)
        {
            return Conflict(
                "Ese puesto ya existe en ese departamento.");
        }

        _context.Puestos.Add(puesto);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            puesto.Id,
            puesto.Nombre,
            puesto.DepartamentoId
        });
    }
}
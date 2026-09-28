using System.ComponentModel.DataAnnotations;

namespace hola_mundo.Client.Models;

/// <summary>
/// Validación a la medida para usar como [FechaNoFutura] sobre una propiedad DateTime.
/// DataAnnotations no trae una regla lista para "la fecha no puede ser futura", así que
/// se escribe aquí una vez y se reutiliza donde haga falta (por ahora, en Empleado.FechaCumpleanos).
/// </summary>
public class FechaNoFuturaAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        // Si el campo viene vacío, [Required] es quien debe quejarse, no este atributo.
        if (value is null)
        {
            return true;
        }

        // Solo tiene sentido comparar fechas; cualquier otro tipo de dato se rechaza.
        if (value is DateTime fecha)
        {
            return fecha.Date <= DateTime.Today;
        }

        return false;
    }
}

using System.ComponentModel.DataAnnotations;

namespace hola_mundo.Client.Models;

public class Empleado
{
    [Required(ErrorMessage = "El número de empleado es obligatorio.")]
    [RegularExpression(
        @"^[0-9]{4,8}$",
        ErrorMessage = "El número de empleado debe contener entre 4 y 8 dígitos."
    )]
    public string NumeroEmpleado { get; set; } = "";

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(
        60,
        ErrorMessage = "El nombre no puede superar los 60 caracteres."
    )]
    [RegularExpression(
        @"^[\p{L}\p{M}]+(?:\s+[\p{L}\p{M}]+)*$",
        ErrorMessage = "El nombre solo puede contener letras y espacios."
    )]
    public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "El género es obligatorio.")]
    [RegularExpression(
        @"^(Femenino|Masculino|No especificar)$",
        ErrorMessage = "Selecciona un género válido."
    )]
    public string Genero { get; set; } = "";

    [Required(ErrorMessage = "La fecha de cumpleaños es obligatoria.")]
    [DataType(DataType.Date)]
    [FechaNoFutura(
        ErrorMessage = "La fecha de cumpleaños no puede ser futura."
    )]
    public DateTime? FechaCumpleanos { get; set; }

    [Required(ErrorMessage = "La CURP es obligatoria.")]
    [RegularExpression(
        @"^[A-ZÑ&]{4}[0-9]{6}[HM][A-Z]{5}[0-9A-Z][0-9]$",
        ErrorMessage = "La CURP debe tener 18 caracteres y un formato válido."
    )]
    public string CURP { get; set; } = "";

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [RegularExpression(
        @"^[0-9]{10}$",
        ErrorMessage = "El teléfono debe contener exactamente 10 dígitos."
    )]
    public string Telefono { get; set; } = "";

    // ==========================================
    // ÁREA / DEPARTAMENTO
    // ==========================================

    [Range(
        1,
        int.MaxValue,
        ErrorMessage = "Debes seleccionar un área."
    )]
    public int DepartamentoId { get; set; }

    // ==========================================
    // PUESTO
    // ==========================================

    [Range(
        1,
        int.MaxValue,
        ErrorMessage = "Debes seleccionar un puesto."
    )]
    public int PuestoId { get; set; }

    // Conservamos esta propiedad porque
    // el backend actual todavía la utiliza.
    [Required(ErrorMessage = "El puesto es obligatorio.")]
    [StringLength(
        60,
        ErrorMessage = "El puesto no puede superar los 60 caracteres."
    )]
    [RegularExpression(
        @"^[\p{L}\p{M}0-9]+(?:[\s.\-/]+[\p{L}\p{M}0-9]+)*$",
        ErrorMessage = "El puesto contiene caracteres no válidos."
    )]
    public string Puesto { get; set; } = "";

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [StringLength(
        100,
        ErrorMessage = "El correo no puede superar los 100 caracteres."
    )]
    [EmailAddress(
        ErrorMessage = "Ingresa un correo electrónico válido."
    )]
    public string Correo { get; set; } = "";
}


// ==========================================
// VALIDACIÓN DE FECHA
// ==========================================

public class FechaNoFuturaAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is null)
            return true;

        if (value is DateTime fecha)
            return fecha.Date <= DateTime.Today;

        return false;
    }
}
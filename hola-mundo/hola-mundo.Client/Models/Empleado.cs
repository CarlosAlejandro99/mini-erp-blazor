using System.ComponentModel.DataAnnotations;

namespace hola_mundo.Client.Models;


/// Representa un empleado del directorio. Los atributos de cada propiedad
/// (Required, RegularExpression, etc.) son "DataAnnotations": EditForm los revisa
/// solo, sin que el formulario tenga que escribir ningún "if" para validarlos.

public class Empleado
{
    [Required(ErrorMessage = "El número de empleado es obligatorio.")]
    // ^ inicio y $ fin del texto, [0-9]{4,8} = solo dígitos, entre 4 y 8 de ellos.
    // O sea: rechaza letras y rechaza números demasiado cortos o demasiado largos.
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
    // \p{L} = cualquier letra de cualquier idioma (incluye acentos, ñ, etc.),
    // \p{M} = acentos/diacríticos que a veces van "pegados" a la letra anterior.
    // El patrón completo pide: una o más letras, luego (espacio + más letras) repetido
    // las veces que haga falta. Así se permiten nombres compuestos pero no números ni símbolos.
    [RegularExpression(
        @"^[\p{L}\p{M}]+(?:\s+[\p{L}\p{M}]+)*$",
        ErrorMessage = "El nombre solo puede contener letras y espacios."
    )]
    public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "El género es obligatorio.")]
    // Solo permite exactamente una de estas tres palabras (así el <select> del formulario
    // no puede mandar un valor inventado).
    [RegularExpression(
        @"^(Femenino|Masculino|No especificar)$",
        ErrorMessage = "Selecciona un género válido."
    )]
    public string Genero { get; set; } = "";

    [Required(ErrorMessage = "La fecha de cumpleaños es obligatoria.")]
    [DataType(DataType.Date)]
    // FechaNoFutura es una validación hecha a la medida (está en Validation/FechaNoFuturaAttribute.cs)
    // porque DataAnnotations no trae de fábrica un atributo para "fecha no puede ser futura".
    [FechaNoFutura(
        ErrorMessage = "La fecha de cumpleaños no puede ser futura."
    )]
    public DateTime? FechaCumpleanos { get; set; }

    [Required(ErrorMessage = "La CURP es obligatoria.")]
    // Formato oficial de la CURP mexicana, siempre 18 caracteres:
    // 4 letras (o Ñ/&) + 6 dígitos (fecha de nacimiento) + H o M (sexo)
    // + 5 letras (entidad y consonantes internas) + 1 dígito o letra + 1 dígito de control.
    [RegularExpression(
        @"^[A-ZÑ&]{4}[0-9]{6}[HM][A-Z]{5}[0-9A-Z][0-9]$",
        ErrorMessage = "La CURP debe tener 18 caracteres y un formato válido."
    )]
    public string CURP { get; set; } = "";

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    // Exactamente 10 dígitos, sin espacios, guiones ni paréntesis.
    [RegularExpression(
        @"^[0-9]{10}$",
        ErrorMessage = "El teléfono debe contener exactamente 10 dígitos."
    )]
    public string Telefono { get; set; } = "";

    [Required(ErrorMessage = "El puesto es obligatorio.")]
    [StringLength(
        60,
        ErrorMessage = "El puesto no puede superar los 60 caracteres."
    )]
    // Letras/números seguidos, y entre palabras se permite un espacio, punto, guion o "/"
    // (para nombres de puesto como "Aux. Contable" o "Analista Jr/Sr").
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
    // EmailAddress ya viene incluido en .NET; revisa que tenga forma de correo (algo@algo.algo).
    [EmailAddress(
        ErrorMessage = "Ingresa un correo electrónico válido."
    )]
    public string Correo { get; set; } = "";

    // DepartamentoId y PuestoId empiezan en 0 (opción "Selecciona un área/puesto" del <select>).
    // Range(1, ...) hace que 0 cuente como "no seleccionado" y dispare el error de validación.
    [Range(
        1,
        int.MaxValue,
        ErrorMessage = "Debes seleccionar un área."
    )]
    public int DepartamentoId { get; set; }

    [Range(
        1,
        int.MaxValue,
        ErrorMessage = "Debes seleccionar un puesto."
    )]
    public int PuestoId { get; set; }
}

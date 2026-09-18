
using System.ComponentModel.DataAnnotations;

namespace hola_mundo.Client.Models;

public class Empleado
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "El puesto es obligatorio.")]
    public string Puesto { get; set; } = "";

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Ingresa un correo válido.")]
    public string Correo { get; set; } = "";
}
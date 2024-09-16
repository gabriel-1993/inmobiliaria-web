using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace InmobiliariaVargasHuancaTorrez.Models;

using System.ComponentModel.DataAnnotations.Schema;

public enum enRoles
{
    Administrador = 1,
    Empleado = 2,
}

public class Usuario
{
    public int Id { get; set; }





    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "El nombre debe tener entre 4 y 50 caracteres.")]
    [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "El apellido debe tener entre 4 y 50 caracteres.")]
    [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
    public string? Apellido { get; set; }

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    [StringLength(100, ErrorMessage = "El email no puede exceder los 100 caracteres.")]
    public string? Email { get; set; }

    public string? Clave { get; set; } 
    public string? Avatar { get; set; }
    public IFormFile? AvatarFile { get; set; }
    public int Rol { get; set; }
    public bool Estado { get; set; }

    [NotMapped]//Para EF
    public string RolNombre => Rol > 0 ? ((enRoles)Rol).ToString() : "";
}
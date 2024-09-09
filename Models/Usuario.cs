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
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? Email { get; set; }
    public string Clave { get; set; } = "";
    public string? Avatar { get; set; }
    public IFormFile? AvatarFile { get; set; }
    public int Rol { get; set; }
    public bool Estado { get; set; }

    [NotMapped]//Para EF
	public string RolNombre => Rol > 0 ? ((enRoles)Rol).ToString() : "";
}
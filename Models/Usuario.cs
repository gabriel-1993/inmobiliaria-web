namespace InmobiliariaVargasHuancaTorrez.Models;

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
    public string? Clave { get; set; }
    public string? Avatar { get; set; }
    public int Rol { get; set; }
    public bool Estado { get; set; }
}
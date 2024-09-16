using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace InmobiliariaVargasHuancaTorrez.Models;

public class Contrato
{
    public Inmueble? Inmueble { get; set; }

    public Inquilino? Inquilino { get; set; }

    public int Id { get; set; }

    public int Id_Inquilino { get; set; }

    public int Id_Inmueble { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public double MontoAlquiler { get; set; }

    public DateTime? FechaTerminacion { get; set; }

    public double? Multa { get; set; }

    public bool Estado { get; set; }


}
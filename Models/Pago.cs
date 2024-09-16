using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace InmobiliariaVargasHuancaTorrez.Models;

public class Pago
{
    public int Id { get; set; }
    public int Id_Contrato { get; set; }
    public int NumeroPago { get; set; }
    public DateTime FechaPago { get; set; }

    [Required(ErrorMessage = "El detalle es obligatorio")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El detalle solo puede contener letras y espacios")]
    public string? Detalle { get; set; }
    public double Importe { get; set; }
    public bool Estado { get; set; }
}
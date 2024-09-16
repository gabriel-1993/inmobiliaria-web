using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace InmobiliariaVargasHuancaTorrez.Models;

public class TipoInmueble
{
    public int Id { get; set; }

    [Required(ErrorMessage = "La descripcion es obligatoria")]
    [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "La descripcion solo puede contener letras y espacios.")]
    public string? Descripcion { get; set; }
    public bool Estado { get; set; }
}
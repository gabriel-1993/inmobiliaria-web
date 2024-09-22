using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace InmobiliariaVargasHuancaTorrez.Models;

public class Inmueble
{
    public Propietario? Propietario { get; set; }
    public TipoInmueble? Tipo { get; set; }

    public int Id { get; set; }
    public int Id_Propietario { get; set; }
    public int Id_Tipo { get; set; }

    [Required(ErrorMessage = "La direccion es obligatoria")]
    [StringLength(255, MinimumLength = 5, ErrorMessage = "La direccion debe tener entre 5 y 255 caracteres.")]
    public string? Direccion { get; set; }

    public string? Uso { get; set; }

    [Required(ErrorMessage = "La cantidad de ambientes es obligatoria")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad de ambientes debe ser mayor a 0")]
    public int CantidadAmbientes { get; set; }

    [Required(ErrorMessage = "Las coordenadas son obligatorias")]
    [RegularExpression(@"^(\-?\d+(\.\d+)?),\s*(\-?\d+(\.\d+)?)$", ErrorMessage = "Las coordenadas deben estar en el formato 'latitud, longitud'.")]
    public string? Coordenadas { get; set; }
   
    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El precio no debe ser igual a 0")]
    public double Precio { get; set; }
    public bool Disponible { get; set; }
    public bool Estado { get; set; }

    public override string ToString()
    {
        var res = $"{Direccion} ";
        return res;
    }
}
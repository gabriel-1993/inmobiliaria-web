using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace InmobiliariaVargasHuancaTorrez.Models
{
    public class Inquilino
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio")]
        [StringLength(8, MinimumLength = 6, ErrorMessage = "El DNI debe tener entre 6 y 8 caracteres.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El DNI solo puede contener números.")]
        public string? Dni { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "El apellido debe tener entre 4 y 50 caracteres.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "El nombre debe tener entre 4 y 50 caracteres.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Phone(ErrorMessage = "El teléfono no es válido")]
        public string? Telefono { get; set; }

        [Phone(ErrorMessage = "El teléfono secundario no es válido")]
        public string? TelefonoSecundario { get; set; }

        public bool Estado { get; set; }

        public override string ToString()
        {
            var res = $"{Nombre} {Apellido} ";
            if (!string.IsNullOrEmpty(Dni))
            {
                res += $"({Dni})";
            }
            return res;
        }
    }
}


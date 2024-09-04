namespace InmobiliariaVargasHuancaTorrez.Models;

public class Contrato
{
    public Inmueble? Inmueble {get; set;}

    public Inquilino? Inquilino {get; set;}

    public int Id { get; set; }

    public int Id_Inquilino { get; set; }

    public int Id_Inmueble { get; set; }

    public DateTime FechaInicio { get; set; }

    //permitir null para cuando aun no llege este dato mostrar el item igual
    public DateTime FechaFin { get; set; }

    public double MontoAlquiler { get; set; }

    public DateTime? FechaTerminacion { get; set; }

    public double? Multa { get; set; }

    public bool Estado { get; set; }



  // Método para calcular la multa
        public double CalcularMulta()
        {
            if (!FechaTerminacion.HasValue)
            {
                return 0;
            }

            var tiempoTotal = (FechaFin - FechaInicio).TotalDays;
            var tiempoTranscurrido = (FechaTerminacion.Value - FechaInicio).TotalDays;

            if (tiempoTranscurrido < tiempoTotal / 2)
            {
                // Multa de 2 meses de alquiler si se cumplió menos de la mitad del tiempo original de alquiler
                return MontoAlquiler * 2;
            }
            else
            {
                // Multa de 1 mes de alquiler si se cumplió más de la mitad del tiempo original de alquiler
                return MontoAlquiler;
            }
        }

}
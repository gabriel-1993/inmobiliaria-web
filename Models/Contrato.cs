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



}
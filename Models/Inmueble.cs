namespace InmobiliariaVargasHuancaTorrez.Models;

public class Inmueble
{
    public int Id_inmueble { get; set; }

    public int Id_propietario { get; set; } 

    public int Id_tipo {get;set;}

    public string Direccion { get; set; } = "";

    public string Uso { get; set; } = "";

    public int CantidadAmbientes { get; set; } 

    public string Coordenadas { get; set; } = "";

    public double Precio { get; set; } 

    public bool Estado { get; set; }

    

}


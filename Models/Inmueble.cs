namespace InmobiliariaVargasHuancaTorrez.Models;

public class Inmueble
{
    public Propietario? Propietario { get; set; }
    public TipoInmueble? Tipo { get; set; }
    public int Id { get; set; }
    public int Id_Propietario { get; set; }
    public int Id_Tipo { get; set; }
    public string? Direccion { get; set; }
    public string? Uso { get; set; }
    public int CantidadAmbientes { get; set; }
    public string? Coordenadas { get; set; }
    public double Precio { get; set; }
    public bool Disponible { get; set; }
    public bool Estado { get; set; }

    public override string ToString()
    {
        var res = $"{Direccion} ";
        return res;
    }
}
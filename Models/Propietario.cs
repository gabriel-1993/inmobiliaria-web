namespace InmobiliariaVargasHuancaTorrez.Models;

public class Propietario
{
    public int Id { get; set; }

    public string Dni { get; set; } = "";

    public string Apellido { get; set; } = "";

    public string Nombre { get; set; } = "";

    public string Telefono { get; set; } = "";

    public string Direccion { get; set; } = "";

    public bool Estado { get; set; }

    public override string ToString()
    {
        var res = $"{Nombre} {Apellido} ";
        if(!String.IsNullOrEmpty(Dni)){
            res += $"({Dni})";
        }
        return res;
    }
}



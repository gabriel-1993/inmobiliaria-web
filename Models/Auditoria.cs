namespace InmobiliariaVargasHuancaTorrez.Models;

public class Auditoria
{
public int Id { get; set; }
public int Id_Usuario { get; set; }
public int? Id_Contrato { get; set; }
public int? Id_Pago { get; set; }
public string Accion { get; set; } = "";
public DateTime FechaHora { get; set; }

}

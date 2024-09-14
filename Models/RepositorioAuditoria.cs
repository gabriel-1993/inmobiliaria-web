using MySql.Data.MySqlClient;

namespace InmobiliariaVargasHuancaTorrez.Models;

public class RepositorioAuditoria
{
    string ConectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

    
    public int Agregar(int id_Usuario,int? id_Contrato,int? id_Pago,string  accion, DateTime fechaHora)
    {

        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"INSERT INTO auditoria (Id_Usuario, Id_Contrato, Id_Pago, Accion, FechaHora
) 
                VALUES (@idusuario, @idcontrato, @idpago, @accion, @fechahora);
            SELECT LAST_INSERT_ID()";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idusuario", id_Usuario);
                command.Parameters.AddWithValue("@idcontrato", id_Contrato);
                command.Parameters.AddWithValue("@idpago", id_Pago);
                command.Parameters.AddWithValue("@accion", accion);
                command.Parameters.AddWithValue("@fechahora", fechaHora);

                connection.Open();
                res = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
        }
        return res;
    }


    


}
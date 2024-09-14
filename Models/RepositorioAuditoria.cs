using MySql.Data.MySqlClient;

namespace InmobiliariaVargasHuancaTorrez.Models;

public class RepositorioAuditoria
{
    string ConectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";


    public int Agregar(int id_Usuario, int? id_Contrato, int? id_Pago, string accion, DateTime fechaHora)
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


    public List<Auditoria> ObtenerPorPago(int Id_Pago)
    {

        List<Auditoria> auditorias = new List<Auditoria>();

        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"SELECT * FROM auditoria
                        WHERE Id_Pago = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", Id_Pago);

                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var auditoria = new Auditoria
                    {
                        Id = reader.GetInt32("Id"),
                        Id_Usuario = reader.GetInt32("Id_Usuario"),
                        Id_Contrato = reader.IsDBNull(reader.GetOrdinal("Id_Contrato")) ? (int?)null : reader.GetInt32("Id_Contrato"),
                        Id_Pago = reader.GetInt32("Id_Pago"),
                        Accion = reader.GetString("Accion"),
                        FechaHora = reader.GetDateTime("FechaHora")
                    };
                    auditorias.Add(auditoria);
                }
                connection.Close();
            }
        }
        return auditorias;
    }






    public List<Auditoria> ObtenerPorContrato(int Id_Contrato)
    {

        List<Auditoria> auditorias = new List<Auditoria>();

        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"SELECT * FROM auditoria
                        WHERE Id_Contrato = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", Id_Contrato);

                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var auditoria = new Auditoria
                    {
                        Id = reader.GetInt32("Id"),
                        Id_Usuario = reader.GetInt32("Id_Usuario"),
                        Id_Contrato = reader.GetInt32("Id_Contrato"),
                        Id_Pago = reader.IsDBNull(reader.GetOrdinal("Id_Pago")) ? (int?)null : reader.GetInt32("Id_Pago"),
                        Accion = reader.GetString("Accion"),
                        FechaHora = reader.GetDateTime("FechaHora")
                    };
                    auditorias.Add(auditoria);
                }
                connection.Close();
            }
        }
        return auditorias;
    }




}
using MySql.Data.MySqlClient;

namespace InmobiliariaVargasHuancaTorrez.Models;

public class RepositorioPago
{
    string ConectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

    public List<Pago> ObtenerPorContrato(int idContrato)
    {
        List<Pago> pagos = new List<Pago>();
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"SELECT
                p.Id,
                p.Id_Contrato,
                p.NumeroPago,
                p.FechaPago,
                p.Detalle,
                p.Importe,
                p.Estado
            FROM
                pagos p
            WHERE
                p.Id_Contrato = {idContrato}";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    pagos.Add(new Pago
                    {
                        Id = reader.GetInt32(nameof(Pago.Id)),
                        Id_Contrato = reader.GetInt32(nameof(Pago.Id_Contrato)),
                        NumeroPago = reader.GetInt32(nameof(Pago.NumeroPago)),
                        FechaPago = reader.GetDateTime(nameof(Pago.FechaPago)),
                        Detalle = reader.GetString(nameof(Pago.Detalle)),
                        Importe = reader.GetDouble(nameof(Pago.Importe)),
                        Estado = reader.GetBoolean(nameof(Pago.Estado))
                    });
                }
                connection.Close();
            }
            return pagos;
        }
    }

    public Pago? Obtener(int id)
    {
        Pago? res = null;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"SELECT 
                {nameof(Pago.Id)}, 
                {nameof(Pago.Id_Contrato)}, 
                {nameof(Pago.NumeroPago)}, 
                {nameof(Pago.FechaPago)}, 
                {nameof(Pago.Detalle)}, 
                {nameof(Pago.Importe)}, 
                {nameof(Pago.Estado)} 
            FROM pagos WHERE {nameof(Pago.Id)} = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    res = new Pago
                    {
                        Id = reader.GetInt32(nameof(Pago.Id)),
                        Id_Contrato = reader.GetInt32(nameof(Pago.Id_Contrato)),
                        NumeroPago = reader.GetInt32(nameof(Pago.NumeroPago)),
                        FechaPago = reader.GetDateTime(nameof(Pago.FechaPago)),
                        Detalle = reader.GetString(nameof(Pago.Detalle)),
                        Importe = reader.GetDouble(nameof(Pago.Importe)),
                        Estado = reader.GetBoolean(nameof(Pago.Estado))
                    };
                }
                connection.Close();
            }
            return res;
        }
    }

    public int Agregar(Pago pago)
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"INSERT INTO pagos (Id_Contrato, NumeroPago, FechaPago, Detalle, Importe, Estado) 
                VALUES (@idContrato, @numeropago, @fechapago, @detalle, @importe, 1);
            SELECT LAST_INSERT_ID()";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idContrato", pago.Id_Contrato);
                command.Parameters.AddWithValue("@numeropago", pago.NumeroPago);
                command.Parameters.AddWithValue("@fechapago", pago.FechaPago);
                command.Parameters.AddWithValue("@detalle", pago.Detalle);
                command.Parameters.AddWithValue("@importe", pago.Importe);

                connection.Open();
                res = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
        }
        return res;
    }

    public int Modificar(Pago pago)
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"UPDATE pagos SET 
                Id_Contrato = @idContrato, 
                NumeroPago = @numeropago, 
                Detalle = @detalle, 
                Importe = @importe 
            WHERE Id = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idContrato", pago.Id_Contrato);
                command.Parameters.AddWithValue("@numeropago", pago.NumeroPago);
                command.Parameters.AddWithValue("@detalle", pago.Detalle);
                command.Parameters.AddWithValue("@importe", pago.Importe);
                command.Parameters.AddWithValue("@id", pago.Id);

                connection.Open();
                res = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return res;
    }

    public int Desactivar(int id)
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = "UPDATE pagos SET Estado = 0 WHERE Id = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                res = command.ExecuteNonQuery();
                connection.Close();
            }
            return res;
        }
    }

    public int Activar(int id)
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = "UPDATE pagos SET Estado = 1 WHERE Id = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                res = command.ExecuteNonQuery();
                connection.Close();
            }
            return res;
        }
    }

    public int ObtenerNumeroPagoMax(int idContrato)
    {
        int maxNumeroPago = 0;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            string query = @"SELECT IFNULL(MAX(NumeroPago), 0) AS MaxNumeroPago 
                         FROM Pagos 
                         WHERE Estado = 1 AND Id_Contrato = @idContrato";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idContrato", idContrato);
                connection.Open();
                var result = command.ExecuteScalar();
                if (result != DBNull.Value && Convert.ToInt32(result) > 0)
                {
                    maxNumeroPago = Convert.ToInt32(result);
                }
                connection.Close();
            }
        }
        return maxNumeroPago;
    }





}
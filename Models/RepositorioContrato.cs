using MySql.Data.MySqlClient;

namespace InmobiliariaVargasHuancaTorrez.Models;

public class RepositorioContrato
{
    string ConectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

    public List<Contrato> ObtenerTodos()
    {
        List<Contrato> listaContratos = new List<Contrato>();

        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"SELECT {nameof(Contrato.Id)}, {nameof(Contrato.Id_Inquilino)}, {nameof(Contrato.Id_Inmueble)}, {nameof(Contrato.FechaInicio)},  {nameof(Contrato.FechaTerminacion)},  {nameof(Contrato.Estado)}
				FROM contratos";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listaContratos.Add(new Contrato
                    {
                        Id = reader.GetInt32(nameof(Contrato.Id)),
                        Id_Inquilino = reader.GetInt32(nameof(Contrato.Id_Inquilino)),
                        Id_Inmueble = reader.GetInt32(nameof(Contrato.Id_Inmueble)),
                        FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio)),
                        FechaTerminacion = reader.IsDBNull(reader.GetOrdinal("FechaTerminacion")) ? (DateTime?)null : reader.GetDateTime("FechaTerminacion"),
                        Estado = reader.GetBoolean(nameof(Contrato.Estado))
                    });
                }
                connection.Close();
            }
            return listaContratos;
        }
    }

    public Contrato? Obtener(int id)
    {
        Contrato? res = null;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"SELECT {nameof(Contrato.Id)}, {nameof(Contrato.Id_Inquilino)}, {nameof(Contrato.Id_Inmueble)}, {nameof(Contrato.FechaInicio)}, {nameof(Contrato.FechaFin)}, {nameof(Contrato.MontoAlquiler)}, {nameof(Contrato.FechaTerminacion)}, {nameof(Contrato.Multa)}, {nameof(Contrato.Estado)} 
                       FROM contratos
                       WHERE {nameof(Contrato.Id)} = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    res = new Contrato
                    {
                        Id = reader.GetInt32(nameof(Contrato.Id)),
                        Id_Inquilino = reader.GetInt32(nameof(Contrato.Id_Inquilino)),
                        Id_Inmueble = reader.GetInt32(nameof(Contrato.Id_Inmueble)),
                        FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio)),
                        FechaFin = reader.GetDateTime(nameof(Contrato.FechaFin)),
                        MontoAlquiler = reader.GetDouble(nameof(Contrato.MontoAlquiler)),
                        FechaTerminacion = reader.IsDBNull(reader.GetOrdinal(nameof(Contrato.FechaTerminacion))) ? (DateTime?)null : reader.GetDateTime(nameof(Contrato.FechaTerminacion)),
                        Multa = reader.GetDouble(nameof(Contrato.Multa)),
                        Estado = reader.GetBoolean(nameof(Contrato.Estado))
                    };
                }
                connection.Close();
            }
        }
        return res;
    }
    // public Contrato? Obtener(int id)
    // {
    //     Contrato? res = null;
    //     using (MySqlConnection connection = new MySqlConnection(ConectionString))
    //     {
    //         var query = $@"SELECT {nameof(Contrato.Id)}, {nameof(Contrato.Id_Inquilino)}, {nameof(Contrato.Id_Inmueble)}, {nameof(Contrato.FechaInicio)},  {nameof(Contrato.FechaFin)},  {nameof(Contrato.MontoAlquiler)},  {nameof(Contrato.FechaTerminacion), {nameof(Contrato.Multa)}, {nameof(Contrato.Estado)}} 
    // 			FROM contratos
    // 			WHERE {nameof(Contrato.Id)} = @id";
    //         using (MySqlCommand command = new MySqlCommand(query, connection))
    //         {
    //             command.Parameters.AddWithValue("@id", id);
    //             connection.Open();
    //             var reader = command.ExecuteReader();
    //             if (reader.Read())
    //             {
    //                 res = new Contrato
    //                 {
    //                     Id = reader.GetInt32(nameof(Contrato.Id)),
    //                     Id_Inquilino = reader.GetInt32(nameof(Contrato.Id_Inquilino)),
    //                     Id_Inmueble = reader.GetString(nameof(Contrato.Id_Inmueble)),
    //                     FechaInicio = reader.GetString(nameof(Contrato.FechaInicio)),
    //                     FechaFin = reader.GetString(nameof(Contrato.FechaFin)),
    //                     MontoAlquiler = reader.GetString(nameof(Contrato.MontoAlquiler)),
    //                     FechaTerminacion = reader.GetString(nameof(Contrato.FechaTerminacion)),
    //                     Estado = reader.GetBoolean(nameof(Contrato.Estado))
    //                 };
    //             }
    //             connection.Close();
    //         }
    //         return res;
    //     }
    // }

    public int Alta(Contrato contrato)
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"INSERT INTO contratos 
    			({nameof(Contrato.Id_Inquilino)}, {nameof(Contrato.Id_Inmueble)}, {nameof(Contrato.FechaInicio)},  {nameof(Contrato.FechaFin)},  {nameof(Contrato.MontoAlquiler)}, {nameof(Contrato.FechaTerminacion)}, {nameof(Contrato.Multa)}, {nameof(Contrato.Estado)} )
    			VALUES (@Id_Inquilino, @Id_Inmueble, @FechaInicio, @FechaFin, @MontoAlquiler);
    			SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                // id es autoincremental
                // al crear estado por defecto es 1 
                command.Parameters.AddWithValue("@Id_Inquilino", contrato.Id_Inquilino);
                command.Parameters.AddWithValue("@Id_Inmueble", contrato.Id_Inmueble);
                command.Parameters.AddWithValue("@FechaInicio", contrato.FechaInicio);
                command.Parameters.AddWithValue("@FechaFin", contrato.FechaFin);
                command.Parameters.AddWithValue("@MontoAlquiler", contrato.MontoAlquiler);

                connection.Open();
                res = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
        }
        return res;
    }

    public int Modificar(Contrato contrato)
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"UPDATE contratos SET
                    {nameof(Contrato.Id_Inquilino)} = @Id_Inquilino,
                    {nameof(Contrato.Id_Inmueble)} = @Id_Inmueble,
    				{nameof(Contrato.FechaInicio)} = @FechaInicio,
                    {nameof(Contrato.FechaFin)} = @FechaFin,
    				{nameof(Contrato.MontoAlquiler)} = @MontoAlquiler,
    				{nameof(Contrato.FechaTerminacion)} = @FechaTerminacion,
    				{nameof(Contrato.Multa)} = @Multa,
                    {nameof(Contrato.Estado)} = @Estado


    			WHERE {nameof(Contrato.Id)} = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id_Inquilino", contrato.Id_Inquilino);
                command.Parameters.AddWithValue("@Id_Inmueble", contrato.Id_Inmueble);
                command.Parameters.AddWithValue("@FechaInicio", contrato.FechaInicio);
                command.Parameters.AddWithValue("@FechaFin", contrato.FechaFin);
                command.Parameters.AddWithValue("@MontoAlquiler", contrato.MontoAlquiler);
                command.Parameters.AddWithValue("@FechaTerminacion", contrato.FechaTerminacion);
                command.Parameters.AddWithValue("@Multa", contrato.Multa);
                command.Parameters.AddWithValue("@Estado", contrato.Estado);

                connection.Open();
                res = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return res;
    }

    public int Baja(int id)
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"UPDATE contratos SET
                        {nameof(Contrato.Estado)} = @estado
                       WHERE {nameof(Contrato.Id)} = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@estado", 0); // Asignar 0 al parámetro @estado
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                res = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return res;
    }

    public int Habilitar(int id)
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"UPDATE contratos SET
                        {nameof(Contrato.Estado)} = @estado
                       WHERE {nameof(Contrato.Id)} = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@estado", 1); // Asignar 0 al parámetro @estado
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                res = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return res;
    }




}
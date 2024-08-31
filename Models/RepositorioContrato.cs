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
            var query = $@"
                        SELECT 
                                c.Id AS ContratoId,
                                c.Id_Inquilino AS ContratoId_Inquilino, 
                                c.Id_Inmueble AS ContratoId_Inmueble, 
                                c.FechaInicio,  
                                c.FechaTerminacion,  
                                c.Estado AS ContratoEstado,
                                i.Id AS InmuebleId,
                                i.Id_propietario AS InmuebleId_propietario,
                                i.Id_tipo AS InmuebleId_tipo,
                                i.Direccion AS InmuebleDireccion,
                                i.Uso AS InmuebleUso,
                                i.CantidadAmbientes AS InmuebleCantidadAmbientes,
                                i.Coordenadas AS InmuebleCoordenadas,
                                i.Precio AS InmueblePrecio,
                                i.Estado AS InmuebleEstado,
                                p.Id AS PropietarioId,
                                p.Nombre AS PropietarioNombre,
                                p.Apellido AS PropietarioApellido,
                                p.Telefono AS PropietarioTelefono,
                                inq.Id AS InquilinoId,
                                inq.Nombre AS InquilinoNombre,
                                inq.Apellido AS InquilinoApellido,
                                inq.Telefono AS InquilinoTelefono
                            FROM contratos c
                            JOIN inmuebles i ON c.Id_Inmueble = i.Id
                            JOIN propietarios p ON i.Id_propietario = p.Id
                            JOIN inquilinos inq ON c.Id_Inquilino = inq.Id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var contrato = new Contrato
                    {
                        Id = reader.GetInt32("ContratoId"),
                        Id_Inquilino = reader.GetInt32("ContratoId_Inquilino"),
                        Id_Inmueble = reader.GetInt32("ContratoId_Inmueble"),
                        FechaInicio = reader.GetDateTime("FechaInicio"),
                        FechaTerminacion = reader.IsDBNull(reader.GetOrdinal("FechaTerminacion")) ? (DateTime?)null : reader.GetDateTime("FechaTerminacion"),
                        Estado = reader.GetBoolean("ContratoEstado"),
                        Inquilino = new Inquilino
                        {
                            Id = reader.GetInt32("InquilinoId"),
                            Nombre = reader.GetString("InquilinoNombre"),
                            Apellido = reader.GetString("InquilinoApellido"),
                            Telefono = reader.GetString("InquilinoTelefono"),
                        },
                        Inmueble = new Inmueble
                        {
                            Id = reader.GetInt32("InmuebleId"),
                            Id_Propietario = reader.GetInt32("InmuebleId_propietario"),
                            Id_Tipo = reader.GetInt32("InmuebleId_tipo"),
                            Direccion = reader.GetString("InmuebleDireccion"),
                            Uso = reader.GetString("InmuebleUso"),
                            CantidadAmbientes = reader.GetInt32("InmuebleCantidadAmbientes"),
                            Coordenadas = reader.GetString("InmuebleCoordenadas"),
                            Precio = reader.GetDouble("InmueblePrecio"),
                            Estado = reader.GetBoolean("InmuebleEstado"),
                            Propietario = new Propietario
                            {
                                Id = reader.GetInt32("PropietarioId"),
                                Nombre = reader.GetString("PropietarioNombre"),
                                Apellido = reader.GetString("PropietarioApellido"),
                                Telefono = reader.GetString("PropietarioTelefono"),
                            }
                        }
                    };
                    listaContratos.Add(contrato);
                }
                connection.Close();
            }
        }
        return listaContratos;
    }


    public Contrato? Obtener(int id)
    {
        Contrato? contrato = null;

        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"
            SELECT 
                c.Id AS ContratoId,
                c.Id_Inquilino AS ContratoId_Inquilino, 
                c.Id_Inmueble AS ContratoId_Inmueble, 
                c.FechaInicio,  
                c.FechaTerminacion,  
                c.Multa,
                c.Estado AS ContratoEstado,
                i.Id AS InmuebleId,
                i.Id_propietario AS InmuebleId_propietario,
                i.Id_tipo AS InmuebleId_tipo,
                i.Direccion AS InmuebleDireccion,
                i.Uso AS InmuebleUso,
                i.CantidadAmbientes AS InmuebleCantidadAmbientes,
                i.Coordenadas AS InmuebleCoordenadas,
                i.Precio AS InmueblePrecio,
                i.Estado AS InmuebleEstado,
                p.Id AS PropietarioId,
                p.Nombre AS PropietarioNombre,
                p.Apellido AS PropietarioApellido,
                p.Telefono AS PropietarioTelefono,
                inq.Id AS InquilinoId,
                inq.Nombre AS InquilinoNombre,
                inq.Apellido AS InquilinoApellido,
                inq.Telefono AS InquilinoTelefono
            FROM contratos c
            JOIN inmuebles i ON c.Id_Inmueble = i.Id
            JOIN propietarios p ON i.Id_propietario = p.Id
            JOIN inquilinos inq ON c.Id_Inquilino = inq.Id
            WHERE c.Id = @id";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    contrato = new Contrato
                    {
                        Id = reader.GetInt32("ContratoId"),
                        Id_Inquilino = reader.GetInt32("ContratoId_Inquilino"),
                        Id_Inmueble = reader.GetInt32("ContratoId_Inmueble"),
                        FechaInicio = reader.GetDateTime("FechaInicio"),
                        FechaTerminacion = reader.IsDBNull(reader.GetOrdinal("FechaTerminacion")) ? (DateTime?)null : reader.GetDateTime("FechaTerminacion"),
                        Multa = reader.IsDBNull(reader.GetOrdinal("Multa")) ? (double?)null : reader.GetDouble("Multa"),
                        Estado = reader.GetBoolean("ContratoEstado"),
                        Inquilino = new Inquilino
                        {
                            Id = reader.GetInt32("InquilinoId"),
                            Nombre = reader.GetString("InquilinoNombre"),
                            Apellido = reader.GetString("InquilinoApellido"),
                            Telefono = reader.GetString("InquilinoTelefono"),
                        },
                        Inmueble = new Inmueble
                        {
                            Id = reader.GetInt32("InmuebleId"),
                            Id_Propietario = reader.GetInt32("InmuebleId_propietario"),
                            Id_Tipo = reader.GetInt32("InmuebleId_tipo"),
                            Direccion = reader.GetString("InmuebleDireccion"),
                            Uso = reader.GetString("InmuebleUso"),
                            CantidadAmbientes = reader.GetInt32("InmuebleCantidadAmbientes"),
                            Coordenadas = reader.GetString("InmuebleCoordenadas"),
                            Precio = reader.GetDouble("InmueblePrecio"),
                            Estado = reader.GetBoolean("InmuebleEstado"),
                            Propietario = new Propietario
                            {
                                Id = reader.GetInt32("PropietarioId"),
                                Nombre = reader.GetString("PropietarioNombre"),
                                Apellido = reader.GetString("PropietarioApellido"),
                                Telefono = reader.GetString("PropietarioTelefono"),
                            }
                        }
                    };
                }
                connection.Close();
            }
        }

        return contrato;
    }




    // public int Alta(Contrato contrato)
    // {
    //     int res = -1;
    //     using (MySqlConnection connection = new MySqlConnection(ConectionString))
    //     {
    //         var query = $@"INSERT INTO contratos 
    // 			({nameof(Contrato.Id_Inquilino)}, {nameof(Contrato.Id_Inmueble)}, {nameof(Contrato.FechaInicio)},  {nameof(Contrato.FechaFin)},  {nameof(Contrato.MontoAlquiler)}, {nameof(Contrato.FechaTerminacion)}, {nameof(Contrato.Multa)}, {nameof(Contrato.Estado)} )
    // 			VALUES (@Id_Inquilino, @Id_Inmueble, @FechaInicio, @FechaFin, @MontoAlquiler);
    // 			SELECT LAST_INSERT_ID();";
    //         using (MySqlCommand command = new MySqlCommand(query, connection))
    //         {
    //             // id es autoincremental
    //             // al crear estado por defecto es 1 
    //             command.Parameters.AddWithValue("@Id_Inquilino", contrato.Id_Inquilino);
    //             command.Parameters.AddWithValue("@Id_Inmueble", contrato.Id_Inmueble);
    //             command.Parameters.AddWithValue("@FechaInicio", contrato.FechaInicio);
    //             command.Parameters.AddWithValue("@FechaFin", contrato.FechaFin);
    //             command.Parameters.AddWithValue("@MontoAlquiler", contrato.MontoAlquiler);

    //             connection.Open();
    //             res = Convert.ToInt32(command.ExecuteScalar());
    //             connection.Close();
    //         }
    //     }
    //     return res;
    // }

public int Alta(Contrato contrato)
{
    int res = -1;
    using (MySqlConnection connection = new MySqlConnection(ConectionString))
    {
        var query = $@"INSERT INTO contratos 
            ({nameof(Contrato.Id_Inquilino)}, {nameof(Contrato.Id_Inmueble)}, {nameof(Contrato.FechaInicio)},  {nameof(Contrato.FechaFin)},  {nameof(Contrato.MontoAlquiler)}, {nameof(Contrato.FechaTerminacion)}, {nameof(Contrato.Multa)}, {nameof(Contrato.Estado)} )
            VALUES (@Id_Inquilino, @Id_Inmueble, @FechaInicio, @FechaFin, @MontoAlquiler, @FechaTerminacion, @Multa, @Estado);
            SELECT LAST_INSERT_ID();";

        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            // Agregar parámetros con las fechas parseadas a tipo Date
            command.Parameters.AddWithValue("@Id_Inquilino", contrato.Id_Inquilino);
            command.Parameters.AddWithValue("@Id_Inmueble", contrato.Id_Inmueble);
            command.Parameters.AddWithValue("@FechaInicio", contrato.FechaInicio.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@FechaFin", contrato.FechaFin.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@MontoAlquiler", contrato.MontoAlquiler);
            command.Parameters.AddWithValue("@FechaTerminacion", contrato.FechaTerminacion.HasValue ? contrato.FechaTerminacion.Value.ToString("yyyy-MM-dd") : (object)DBNull.Value);
            command.Parameters.AddWithValue("@Multa", contrato.Multa);
            command.Parameters.AddWithValue("@Estado", contrato.Estado);

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
                command.Parameters.AddWithValue("@estado", 1);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                res = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return res;
    }




}
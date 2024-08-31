using MySql.Data.MySqlClient;

namespace InmobiliariaVargasHuancaTorrez.Models;

public class RepositorioInmueble
{
    string ConectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

public List<Inmueble> ObtenerTodos()
{
    List<Inmueble> inmuebles = new List<Inmueble>();
    using (MySqlConnection connection = new MySqlConnection(ConectionString))
    {
        var query = $@"
            SELECT 
                i.{nameof(Inmueble.Id)}, 
                i.{nameof(Inmueble.Id_Propietario)}, 
                i.{nameof(Inmueble.Id_Tipo)}, 
                i.{nameof(Inmueble.Direccion)}, 
                i.{nameof(Inmueble.Uso)}, 
                i.{nameof(Inmueble.CantidadAmbientes)}, 
                i.{nameof(Inmueble.Coordenadas)}, 
                i.{nameof(Inmueble.Precio)}, 
                i.{nameof(Inmueble.Estado)}, 
                p.{nameof(Propietario.Id)} AS PropietarioId,
                p.{nameof(Propietario.Dni)} AS PropietarioDni,
                p.{nameof(Propietario.Nombre)} AS PropietarioNombre,
                p.{nameof(Propietario.Apellido)} AS PropietarioApellido,
                p.{nameof(Propietario.Telefono)} AS PropietarioTelefono,
                p.{nameof(Propietario.Direccion)} AS PropietarioDireccion,
                p.{nameof(Propietario.Estado)} AS PropietarioEstado
            FROM inmuebles i
            JOIN propietarios p ON i.{nameof(Inmueble.Id_Propietario)} = p.{nameof(Propietario.Id)}";
        
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var inmueble = new Inmueble
                {
                    Id = reader.GetInt32(nameof(Inmueble.Id)),
                    Id_Propietario = reader.GetInt32(nameof(Inmueble.Id_Propietario)),
                    Id_Tipo = reader.GetInt32(nameof(Inmueble.Id_Tipo)),
                    Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                    Uso = reader.GetString(nameof(Inmueble.Uso)),
                    CantidadAmbientes = reader.GetInt32(nameof(Inmueble.CantidadAmbientes)),
                    Coordenadas = reader.GetString(nameof(Inmueble.Coordenadas)),
                    Precio = reader.GetDouble(nameof(Inmueble.Precio)),
                    Estado = reader.GetBoolean(nameof(Inmueble.Estado)),
                    Propietario = new Propietario
                    {
                        Id = reader.GetInt32("PropietarioId"),
                        Dni = reader.GetString("PropietarioDni"),
                        Nombre = reader.GetString("PropietarioNombre"),
                        Apellido = reader.GetString("PropietarioApellido"),
                        Telefono = reader.GetString("PropietarioTelefono"),
                        Direccion = reader.GetString("PropietarioDireccion"),
                        Estado = reader.GetBoolean("PropietarioEstado")
                    }
                };
                inmuebles.Add(inmueble);
            }
            connection.Close();
        }
        return inmuebles;
    }
}



    public Inmueble? Obtener(int id)
    {
        Inmueble? inmueble = null;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"SELECT
                i.Id,
                i.Id_Propietario,
                i.Id_Tipo,
                i.Direccion,
                i.Uso,
                i.CantidadAmbientes,
                i.Coordenadas,
                i.Precio,
                i.Estado,
                p.Nombre,
                p.Apellido,
                p.Dni,
                t.Descripcion
            FROM
                inmuebles i
            INNER JOIN propietarios p ON
                i.Id_Propietario = p.Id
            INNER JOIN tipos_inmueble t ON
                i.Id_Tipo = t.Id
            WHERE
                i.Id = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    inmueble = new Inmueble
                    {
                        Id = reader.GetInt32(nameof(Inmueble.Id)),
                        Id_Propietario = reader.GetInt32(nameof(Inmueble.Id_Propietario)),
                        Id_Tipo = reader.GetInt32(nameof(Inmueble.Id_Tipo)),
                        Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                        Uso = reader.GetString(nameof(Inmueble.Uso)),
                        CantidadAmbientes = reader.GetInt32(nameof(Inmueble.CantidadAmbientes)),
                        Coordenadas = reader.GetString(nameof(Inmueble.Coordenadas)),
                        Precio = reader.GetInt32(nameof(Inmueble.Precio)),
                        Estado = reader.GetBoolean(nameof(Inmueble.Estado)),
                        Propietario = new Propietario
                        {
                            Nombre = reader.GetString(nameof(Propietario.Nombre)),
                            Apellido = reader.GetString(nameof(Propietario.Apellido)),
                            Dni = reader.GetString(nameof(Propietario.Dni))
                        },
                        Tipo = new TipoInmueble
                        {
                            Descripcion = reader.GetString(nameof(TipoInmueble.Descripcion))
                        }
                    };
                }
                connection.Close();
            }
            return inmueble;
        }
    }

    public int Agregar(Inmueble inmueble)
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"INSERT INTO inmuebles 
            (Id_Propietario, Id_Tipo, Direccion, Uso, CantidadAmbientes, Coordenadas, Precio, Estado) 
            VALUES (@id_propietario, @id_tipo, @direccion, @uso, @cantidadambientes, @coordenadas, @precio, 1);
            SELECT LAST_INSERT_ID()";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id_propietario", inmueble.Id_Propietario);
                command.Parameters.AddWithValue("@id_tipo", inmueble.Id_Tipo);
                command.Parameters.AddWithValue("@direccion", inmueble.Direccion);
                command.Parameters.AddWithValue("@uso", inmueble.Uso);
                command.Parameters.AddWithValue("@cantidadambientes", inmueble.CantidadAmbientes);
                command.Parameters.AddWithValue("@coordenadas", inmueble.Coordenadas);
                command.Parameters.AddWithValue("@precio", inmueble.Precio);
                connection.Open();
                res = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
        }
        return res;
    }

    public int Modificar(Inmueble inmueble)
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"UPDATE inmuebles SET
                Id_Propietario = @id_propietario,
                Id_Tipo = @id_tipo,
                Direccion = @direccion,
                Uso = @uso,
                CantidadAmbientes = @cantidadambientes,
                Coordenadas = @coordenadas,
                Precio = @precio
            WHERE Id = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id_propietario", inmueble.Id_Propietario);
                command.Parameters.AddWithValue("@id_tipo", inmueble.Id_Tipo);
                command.Parameters.AddWithValue("@direccion", inmueble.Direccion);
                command.Parameters.AddWithValue("@uso", inmueble.Uso);
                command.Parameters.AddWithValue("@cantidadambientes", inmueble.CantidadAmbientes);
                command.Parameters.AddWithValue("@coordenadas", inmueble.Coordenadas);
                command.Parameters.AddWithValue("@precio", inmueble.Precio);
                command.Parameters.AddWithValue("@id", inmueble.Id);

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
            var query = "UPDATE inmuebles SET Estado = 0 WHERE Id = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                res = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return res;
    }

    public int Activar(int id)
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = "UPDATE inmuebles SET Estado = 1 WHERE Id = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                res = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return res;
    }

}
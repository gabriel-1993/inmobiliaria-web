using MySql.Data.MySqlClient;

namespace InmobiliariaVargasHuancaTorrez.Models;

public class RepositorioTipoInmueble
{
    string ConectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

    public List<TipoInmueble> ObtenerTodos()
    {
        List<TipoInmueble> tipos = new List<TipoInmueble>();
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"SELECT {nameof(TipoInmueble.Id)}, {nameof(TipoInmueble.Descripcion)}, {nameof(TipoInmueble.Estado)} FROM tipos_inmueble";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    tipos.Add(new TipoInmueble
                    {
                        Id = reader.GetInt32(nameof(TipoInmueble.Id)),
                        Descripcion = reader.GetString(nameof(TipoInmueble.Descripcion)),
                        Estado = reader.GetBoolean(nameof(TipoInmueble.Estado))
                    });
                }
                connection.Close();
            }
            return tipos;
        }
    }

    public TipoInmueble? Obtener(int id)
    {
        TipoInmueble? tipo = null;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"SELECT {nameof(TipoInmueble.Id)}, {nameof(TipoInmueble.Descripcion)}, {nameof(TipoInmueble.Estado)} FROM tipos_inmueble WHERE {nameof(TipoInmueble.Id)} = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    tipo = new TipoInmueble
                    {
                        Id = reader.GetInt32(nameof(TipoInmueble.Id)),
                        Descripcion = reader.GetString(nameof(TipoInmueble.Descripcion)),
                        Estado = reader.GetBoolean(nameof(TipoInmueble.Estado))
                    };
                }
                connection.Close();
            }
            return tipo;
        }
    }

    public int Agregar(TipoInmueble tipo)
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"INSERT INTO tipos_inmueble 
            (Descripcion, Estado)
            VALUES (@descripcion, 1);
            SELECT LAST_INSERT_ID()";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@descripcion", tipo.Descripcion);
                connection.Open();
                res = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
        }
        return res;
    }

    public int Modificar(TipoInmueble tipo)
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"UPDATE tipos_inmueble SET
            {nameof(TipoInmueble.Descripcion)} = @descripcion
            WHERE {nameof(TipoInmueble.Id)} = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", tipo.Id);
                command.Parameters.AddWithValue("@descripcion", tipo.Descripcion);
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
            var query = $@"UPDATE tipos_inmueble SET Estado = 0 WHERE Id = @id";
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
            var query = $@"UPDATE tipos_inmueble SET Estado = 1 WHERE Id = @id";
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
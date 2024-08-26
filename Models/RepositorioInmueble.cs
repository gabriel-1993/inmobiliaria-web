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
      var query = $@"SELECT Id, Id_propietario, Id_tipo, Direccion, Uso, CantidadAmbientes, Coordenadas, Precio, Estado FROM inmuebles";
      using (MySqlCommand command = new MySqlCommand(query, connection))
      {
        connection.Open();
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
          inmuebles.Add(new Inmueble
                {
                    Id_inmueble = reader.GetInt32("Id"),
                    Id_propietario = reader.GetInt32("Id_propietario"),
                    Id_tipo = reader.GetInt32("Id_tipo"),
                    Direccion = reader.GetString("Direccion"),
                    Uso = reader.GetString("Uso"),
                    CantidadAmbientes = reader.GetInt32("CantidadAmbientes"),
                    Coordenadas = reader.GetString("Coordenadas"),
                    Precio = reader.GetInt32("Precio"),
                    Estado = reader.GetBoolean("Estado")
                });
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
        var query = "SELECT * FROM inmuebles WHERE id = @id";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                inmueble = new Inmueble
                {
                    // Cambia 'Id_inmueble' a 'Id' para que coincida con el nombre en la base de datos
                    Id_inmueble = reader.GetInt32("Id"),
                    Id_propietario = reader.GetInt32(nameof(Inmueble.Id_propietario)),
                    Id_tipo = reader.GetInt32(nameof(Inmueble.Id_tipo)),
                    Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                    Uso = reader.GetString(nameof(Inmueble.Uso)),
                    CantidadAmbientes = reader.GetInt32(nameof(Inmueble.CantidadAmbientes)),
                    Coordenadas = reader.GetString(nameof(Inmueble.Coordenadas)),
                    Precio = reader.GetInt32(nameof(Inmueble.Precio)),
                    Estado = reader.GetBoolean(nameof(Inmueble.Estado))
                };
            }
            connection.Close();
        }
    }
    return inmueble;
}


public int Agregar(Inmueble inmueble)
{
    int res = -1;

    // Verificar si el Id_tipo existe en la tabla tipos_inmueble
    if (!ExisteTipo(inmueble.Id_tipo))
    {
        throw new Exception("El tipo de inmueble especificado no existe.");
    }

    using (MySqlConnection connection = new MySqlConnection(ConectionString))
    {
        var query = $@"
        INSERT INTO inmuebles 
        (Id_propietario, Id_tipo, Direccion, Uso, CantidadAmbientes, Coordenadas, Precio, Estado) 
        VALUES 
        (@id_propietario, @id_tipo, @direccion, @uso, @cantidadambientes, @coordenadas, @precio, @estado);
        SELECT LAST_INSERT_ID()";
        
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@id_propietario", inmueble.Id_propietario);
            command.Parameters.AddWithValue("@id_tipo", inmueble.Id_tipo);
            command.Parameters.AddWithValue("@direccion", inmueble.Direccion);
            command.Parameters.AddWithValue("@uso", inmueble.Uso);
            command.Parameters.AddWithValue("@cantidadambientes", inmueble.CantidadAmbientes);
            command.Parameters.AddWithValue("@coordenadas", inmueble.Coordenadas);
            command.Parameters.AddWithValue("@precio", inmueble.Precio);
            command.Parameters.AddWithValue("@estado", inmueble.Estado);
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
            Id_propietario = @id_propietario,
            Id_tipo = @id_tipo,
            Direccion = @direccion,
            Uso = @uso,
            CantidadAmbientes = @cantidadambientes,
            Coordenadas = @coordenadas,
            Precio = @precio,
            Estado = @estado
        WHERE Id = @id_inmueble"; // Aquí aseguramos que la consulta SQL utiliza el parámetro @id_inmueble

        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@id_propietario", inmueble.Id_propietario);
            command.Parameters.AddWithValue("@id_tipo", inmueble.Id_tipo);
            command.Parameters.AddWithValue("@direccion", inmueble.Direccion);
            command.Parameters.AddWithValue("@uso", inmueble.Uso);
            command.Parameters.AddWithValue("@cantidadambientes", inmueble.CantidadAmbientes);
            command.Parameters.AddWithValue("@coordenadas", inmueble.Coordenadas);
            command.Parameters.AddWithValue("@precio", inmueble.Precio);
            command.Parameters.AddWithValue("@estado", inmueble.Estado);
            command.Parameters.AddWithValue("@id_inmueble", inmueble.Id_inmueble); // Asegurando que el ID del inmueble se pasa correctamente

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
      var query = "UPDATE inmuebles SET estado = 0 WHERE id = @id";
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
      var query = "UPDATE inmuebles SET estado = 1 WHERE id = @id";
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
  public bool ExisteTipo(int idTipo)
{
    using (MySqlConnection connection = new MySqlConnection(ConectionString))
    {
        var query = "SELECT COUNT(*) FROM tipos_inmueble WHERE Id = @idTipo";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@idTipo", idTipo);
            connection.Open();
            var count = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            return count > 0;
        }
    }
}

}
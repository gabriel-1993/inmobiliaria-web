using MySql.Data.MySqlClient;

namespace InmobiliariaVargasHuancaTorrez.Models;

public class RepositorioInquilino : RepositorioBase
{
  public RepositorioInquilino(IConfiguration configuration) : base(configuration)
  {
  }

  public List<Inquilino> ObtenerTodos()
  {
    List<Inquilino> inquilinos = new List<Inquilino>();
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
      var query = $@"SELECT {nameof(Inquilino.Id)}, {nameof(Inquilino.Dni)}, {nameof(Inquilino.Apellido)}, {nameof(Inquilino.Nombre)}, {nameof(Inquilino.Telefono)}, {nameof(Inquilino.TelefonoSecundario)}, {nameof(Inquilino.Estado)} FROM inquilinos";
      using (MySqlCommand command = new MySqlCommand(query, connection))
      {
        connection.Open();
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
          inquilinos.Add(new Inquilino
          {
            Id = reader.GetInt32(nameof(Inquilino.Id)),
            Dni = reader.GetString(nameof(Inquilino.Dni)),
            Apellido = reader.GetString(nameof(Inquilino.Apellido)),
            Nombre = reader.GetString(nameof(Inquilino.Nombre)),
            Telefono = reader.GetString(nameof(Inquilino.Telefono)),
            TelefonoSecundario = reader.IsDBNull(reader.GetOrdinal(nameof(Inquilino.TelefonoSecundario)))
                        ? null
                        : reader.GetString(reader.GetOrdinal(nameof(Inquilino.TelefonoSecundario))),
            Estado = reader.GetBoolean(nameof(Inquilino.Estado))
          });
        }
        connection.Close();
      }
      return inquilinos;
    }
  }

  public Inquilino? Obtener(int id)
  {
    Inquilino? inquilino = null;
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
      var query = "SELECT * FROM inquilinos WHERE id = @id";
      using (MySqlCommand command = new MySqlCommand(query, connection))
      {
        command.Parameters.AddWithValue("@id", id);
        connection.Open();
        var reader = command.ExecuteReader();
        if (reader.Read())
        {
          inquilino = new Inquilino
          {
            Id = reader.GetInt32(0),
            Dni = reader.GetString(1),
            Apellido = reader.GetString(2),
            Nombre = reader.GetString(3),
            Telefono = reader.GetString(4),
            TelefonoSecundario = reader.IsDBNull(reader.GetOrdinal(nameof(Inquilino.TelefonoSecundario)))
                        ? null
                        : reader.GetString(reader.GetOrdinal(nameof(Inquilino.TelefonoSecundario))),
            Estado = reader.GetBoolean(6)
          };
        }
        connection.Close();
      }
      return inquilino;
    }
  }

  public int Agregar(Inquilino inquilino)
  {
    int res = -1;
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
      var query = $@"INSERT INTO inquilinos 
      ({nameof(Inquilino.Dni)}, {nameof(Inquilino.Apellido)}, {nameof(Inquilino.Nombre)}, {nameof(Inquilino.Telefono)}, {nameof(Inquilino.TelefonoSecundario)}, {nameof(Inquilino.Estado)}) 
      VALUES (@dni, @apellido, @nombre, @telefono, @telefonosecundario, 1);
      SELECT LAST_INSERT_ID()";
      using (MySqlCommand command = new MySqlCommand(query, connection))
      {
        command.Parameters.AddWithValue("@dni", inquilino.Dni);
        command.Parameters.AddWithValue("@apellido", inquilino.Apellido);
        command.Parameters.AddWithValue("@nombre", inquilino.Nombre);
        command.Parameters.AddWithValue("@telefono", inquilino.Telefono);
        command.Parameters.AddWithValue("@telefonosecundario", inquilino.TelefonoSecundario);
        connection.Open();
        res = Convert.ToInt32(command.ExecuteScalar());
        connection.Close();
      }
    }
    return res;
  }

  public int Modificar(Inquilino inquilino)
  {
    int res = -1;
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
      var query = $@"UPDATE inquilinos SET
        {nameof(Inquilino.Dni)} = @dni,
        {nameof(Inquilino.Apellido)} = @apellido,
        {nameof(Inquilino.Nombre)} = @nombre,
        {nameof(Inquilino.Telefono)} = @telefono,
        {nameof(Inquilino.TelefonoSecundario)} = @telefonosecundario
      WHERE {nameof(Inquilino.Id)} = @id";
      using (MySqlCommand command = new MySqlCommand(query, connection))
      {
        command.Parameters.AddWithValue("@id", inquilino.Id);
        command.Parameters.AddWithValue("@dni", inquilino.Dni);
        command.Parameters.AddWithValue("@apellido", inquilino.Apellido);
        command.Parameters.AddWithValue("@nombre", inquilino.Nombre);
        command.Parameters.AddWithValue("@telefono", inquilino.Telefono);
        command.Parameters.AddWithValue("@telefonosecundario", inquilino.TelefonoSecundario);
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
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
      var query = "UPDATE inquilinos SET estado = 0 WHERE id = @id";
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
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
      var query = "UPDATE inquilinos SET estado = 1 WHERE id = @id";
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

  public int Cantidad()
  {
    int res = -1;
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
      var query = $@"SELECT COUNT(*) FROM inquilinos;";
      using (MySqlCommand command = new MySqlCommand(query, connection))
      {
        connection.Open();
        res = Convert.ToInt32(command.ExecuteScalar());
        connection.Close();
      }
    }
    return res;
  }

}
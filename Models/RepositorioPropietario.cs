using MySql.Data.MySqlClient;

namespace InmobiliariaVargasHuancaTorrez.Models;

public class RepositorioPropietario
{
    string ConectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

    public List<Propietario> ObtenerTodos()
    {
        List<Propietario> propietarios = new List<Propietario>();
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"SELECT {nameof(Propietario.Id)}, {nameof(Propietario.Dni)}, {nameof(Propietario.Apellido)}, {nameof(Propietario.Nombre)},  {nameof(Propietario.Telefono)},  {nameof(Propietario.Direccion)},  {nameof(Propietario.Estado)} 
				FROM propietarios";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    propietarios.Add(new Propietario
                    {
                        Id = reader.GetInt32(nameof(Propietario.Id)),
                        Dni = reader.GetString(nameof(Propietario.Dni)),
                        Apellido = reader.GetString(nameof(Propietario.Apellido)),
                        Nombre = reader.GetString(nameof(Propietario.Nombre)),
                        Telefono = reader.GetString(nameof(Propietario.Telefono)),
                        Direccion = reader.GetString(nameof(Propietario.Direccion)),
                        Estado = reader.GetBoolean(nameof(Propietario.Estado))
                    });
                }
                connection.Close();
            }
            return propietarios;
        }
    }


    public Propietario? Obtener(int id)
    {
        Propietario? res = null;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"SELECT {nameof(Propietario.Id)}, {nameof(Propietario.Dni)}, {nameof(Propietario.Apellido)}, {nameof(Propietario.Nombre)},  {nameof(Propietario.Telefono)},  {nameof(Propietario.Direccion)},  {nameof(Propietario.Estado)} 
				FROM propietarios
				WHERE {nameof(Propietario.Id)} = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    res = new Propietario
                    {
                        Id = reader.GetInt32(nameof(Propietario.Id)),
                        Dni = reader.GetString(nameof(Propietario.Dni)),
                        Apellido = reader.GetString(nameof(Propietario.Apellido)),
                        Nombre = reader.GetString(nameof(Propietario.Nombre)),
                        Telefono = reader.GetString(nameof(Propietario.Telefono)),
                        Direccion = reader.GetString(nameof(Propietario.Direccion)),
                        Estado = reader.GetBoolean(nameof(Propietario.Estado))
                    };
                }
                connection.Close();
            }
            return res;
        }
    }

    public int Alta(Propietario propietario)
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"INSERT INTO propietarios 
				({nameof(Propietario.Dni)}, {nameof(Propietario.Apellido)}, {nameof(Propietario.Nombre)},  {nameof(Propietario.Telefono)},  {nameof(Propietario.Direccion)} )
				VALUES (@Dni, @Apellido, @Nombre, @Telefono, @Direccion);
				SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                // id es autoincremental
                // al crear estado por defecto es 1 
                command.Parameters.AddWithValue("@dni", propietario.Dni);
                command.Parameters.AddWithValue("@apellido", propietario.Apellido);
                command.Parameters.AddWithValue("@nombre", propietario.Nombre);
                command.Parameters.AddWithValue("@telefono", propietario.Telefono);
                command.Parameters.AddWithValue("@direccion", propietario.Direccion);

                connection.Open();
                res = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
        }
        return res;
    }

    public int Modificar(Propietario propietario)
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"UPDATE propietarios SET
                    {nameof(Propietario.Dni)} = @dni,
                    {nameof(Propietario.Apellido)} = @apellido,
					{nameof(Propietario.Nombre)} = @nombre,
                    {nameof(Propietario.Telefono)} = @telefono,
					{nameof(Propietario.Direccion)} = @direccion
				WHERE {nameof(Propietario.Id)} = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", propietario.Id);
                command.Parameters.AddWithValue("@dni", propietario.Dni);
                command.Parameters.AddWithValue("@apellido", propietario.Apellido);
                command.Parameters.AddWithValue("@nombre", propietario.Nombre);
                command.Parameters.AddWithValue("@telefono", propietario.Telefono);
                command.Parameters.AddWithValue("@direccion", propietario.Direccion);
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
            var query = $@"UPDATE propietarios SET
                        {nameof(Propietario.Estado)} = @estado
                       WHERE {nameof(Propietario.Id)} = @id";
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
            var query = $@"UPDATE propietarios SET
                        {nameof(Propietario.Estado)} = @estado
                       WHERE {nameof(Propietario.Id)} = @id";
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
    
    public int Cantidad()
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"SELECT COUNT(*) FROM propietarios;";
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
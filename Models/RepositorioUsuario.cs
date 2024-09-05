using MySql.Data.MySqlClient;

namespace InmobiliariaVargasHuancaTorrez.Models;

public class RepositorioUsuario
{
    string ConectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

    public List<Usuario> ObtenerTodos()
    {
        List<Usuario> usuarios = new List<Usuario>();
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"SELECT {nameof(Usuario.Id)}, {nameof(Usuario.Nombre)}, {nameof(Usuario.Apellido)}, {nameof(Usuario.Email)}, {nameof(Usuario.Clave)}, {nameof(Usuario.Avatar)}, {nameof(Usuario.Rol)}, {nameof(Usuario.Estado)} FROM usuarios";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    usuarios.Add(new Usuario
                    {
                        Id = reader.GetInt32(nameof(Usuario.Id)),
                        Nombre = reader.GetString(nameof(Usuario.Nombre)),
                        Apellido = reader.GetString(nameof(Usuario.Apellido)),
                        Email = reader.GetString(nameof(Usuario.Email)),
                        Clave = reader.GetString(nameof(Usuario.Clave)),
                        Avatar = reader.GetString(nameof(Usuario.Avatar)),
                        Rol = reader.GetInt32(nameof(Usuario.Rol)),
                        Estado = reader.GetBoolean(nameof(Usuario.Estado))
                    });
                }
                connection.Close();
            }
            return usuarios;
        }
    }

    public Usuario? Obtener(int id)
    {
        Usuario? usuario = null;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = "SELECT * FROM usuarios WHERE id = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    usuario = new Usuario
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Email = reader.GetString(3),
                        Clave = reader.GetString(4),
                        Avatar = reader.GetString(5),
                        Rol = reader.GetInt32(6),
                        Estado = reader.GetBoolean(7)
                    };
                }
                connection.Close();
            }
            return usuario;
        }
    }

    public int Agregar(Usuario usuario)
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"INSERT INTO usuarios (nombre, apellido, email, clave, avatar, rol, estado) VALUES (@nombre, @apellido, @email, @clave, @avatar, @rol, 1);
            SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                command.Parameters.AddWithValue("@apellido", usuario.Apellido);
                command.Parameters.AddWithValue("@email", usuario.Email);
                command.Parameters.AddWithValue("@clave", usuario.Clave);
                command.Parameters.AddWithValue("@avatar", usuario.Avatar);
                command.Parameters.AddWithValue("@rol", usuario.Rol);
                connection.Open();
                res = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
        }
        return res;
    }

    public int Modificar(Usuario usuario)
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query = $@"UPDATE usuarios SET nombre = @nombre, apellido = @apellido, email = @email, clave = @clave, avatar = @avatar, rol = @rol WHERE id = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", usuario.Id);
                command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                command.Parameters.AddWithValue("@apellido", usuario.Apellido);
                command.Parameters.AddWithValue("@email", usuario.Email);
                command.Parameters.AddWithValue("@clave", usuario.Clave);
                command.Parameters.AddWithValue("@avatar", usuario.Avatar);
                command.Parameters.AddWithValue("@rol", usuario.Rol);
                connection.Open();
                res = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return res;
    }

    
}
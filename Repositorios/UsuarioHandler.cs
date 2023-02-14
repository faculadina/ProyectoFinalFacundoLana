using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionWebApi.Modelos
{
    internal static class UsuarioHandler
    {
        public static string cadenaConexion = "Data Source=DESKTOP-P515SRF\\SQLEXPRESS;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static Usuario ConsultarUsuario(long id)
        {
            Usuario usu = new Usuario();
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("Select * from Usuario where id = @id", connection);
                comando.Parameters.AddWithValue("@id", id);
                
                connection.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if(reader.HasRows)
                {
                    reader.Read();   
                    usu.Id = reader.GetInt64(0);   
                    usu.Nombre = reader.GetString(1);   
                    usu.Apellido = reader.GetString(2);  
                    usu.NombreUsuario = reader.GetString(3);
                    usu.Contraseña = reader.GetString(4);
                    usu.Mail = reader.GetString(5);
                    
                    

                }
                
                
            }
            return usu;
        }


        public static Usuario inicioDeSesion(string nombreUsuario, string contraseña)
        {
            Usuario usu = new Usuario();
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("Select * from Usuario where usuario.NombreUsuario = @nombreUsuario and usuario.Contraseña = @contraseña", connection);
                comando.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                comando.Parameters.AddWithValue("@contraseña", contraseña);
                connection.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    usu.Id = reader.GetInt64(0);
                    usu.Nombre = reader.GetString(1);
                    usu.Apellido = reader.GetString(2);
                    usu.NombreUsuario = reader.GetString(3);
                    usu.Contraseña = reader.GetString(4);
                    usu.Mail = reader.GetString(5);
                }
                else
                {
                    usu.Id = 0;
                    usu.Nombre = null;
                    usu.Apellido = null;
                    usu.NombreUsuario = null;
                    usu.Contraseña = null;
                    usu.Mail = null;
                }
            }

            return usu;
        }


        public static void modificarUsuario(Usuario usuario)
        {
            if(usuario != null)
            {
                using(SqlConnection connection = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand("update Usuario set Nombre = @nombre, Apellido = @apellido, NombreUsuario = @nombreUsuario, Contraseña = @contrasena, Mail = @mail where Id = @id;",connection);
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario);
                    cmd.Parameters.AddWithValue("@contrasena", usuario.Contraseña);
                    cmd.Parameters.AddWithValue("@mail", usuario.Mail);
                    cmd.Parameters.AddWithValue("@id", usuario.Id);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}

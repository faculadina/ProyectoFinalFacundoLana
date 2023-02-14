using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionWebApi.Modelos
{
    internal class ProductoHandler
    {

        public static string cadenaConexion = "Data Source=DESKTOP-P515SRF\\SQLEXPRESS;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static List<Producto> ConsultaProductoPorUsuario(long idUsuario)
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("select * from Producto where Producto.IdUsuario = @idUsuario", connection);
                comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                connection.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto prod = new Producto();
                        prod.Id = reader.GetInt64(0);
                        prod.Descripciones = reader.GetString(1);
                        prod.Costo = reader.GetDouble(2);
                        prod.PrecioVenta = reader.GetDouble(3);
                        prod.Stock = reader.GetInt32(4);
                        prod.IdUsuario = reader.GetInt64(5);
                        lista.Add(prod);
                    }
                }
            }
                

            return lista;
        }


        public static void crearProducto(Producto producto)
        {
                using(SqlConnection connection = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand("insert into Producto (Descripciones, costo, PrecioVenta, stock, IdUsuario) values (@descripciones,@costo,@precioVenta,@stock,@idUsuario)", connection);
                    cmd.Parameters.AddWithValue("@descripciones", producto.Descripciones);
                    cmd.Parameters.AddWithValue("@costo", producto.Costo);
                    cmd.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);
                    cmd.Parameters.AddWithValue("@stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@idUsuario", producto.IdUsuario);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
        }

        public static void modificarProducto (Producto p)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd;
                if(p.Costo > 0)
                {
                    cmd = new SqlCommand("update producto set Descripciones = @descripciones, Costo = @costo, PrecioVenta = @precioVenta, Stock = @stock, IdUsuario = @idUsuario where Id = @id", connection);
                }
                else
                {
                    cmd = new SqlCommand("update producto set Descripciones = @descripciones, PrecioVenta = @precioVenta, Stock = @stock, IdUsuario = @idUsuario where Id = @id", connection);
                }
                
                cmd.Parameters.AddWithValue("@descripciones",p.Descripciones);
                cmd.Parameters.AddWithValue("@costo", p.Costo);
                cmd.Parameters.AddWithValue("@precioVenta", p.PrecioVenta);
                cmd.Parameters.AddWithValue("@stock", p.Stock);
                cmd.Parameters.AddWithValue("@idUsuario", p.IdUsuario);
                cmd.Parameters.AddWithValue("@id", p.Id);
                connection.Open();
                int var = cmd.ExecuteNonQuery();
                if(var > 0)
                {
                    Console.WriteLine("Producto modificado");
                }
                else
                {
                    Console.WriteLine("Ese producto no existe");
                }
            }
        }


        public static void eliminarProducto(long idProducto)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmdProductoVendido = new SqlCommand("delete ProductoVendido where IdProducto = @idProducto", connection);
                cmdProductoVendido.Parameters.AddWithValue("@idProducto", idProducto);
                connection.Open();
                cmdProductoVendido.ExecuteNonQuery();
                SqlCommand cmdProducto = new SqlCommand("delete producto where id = @idProducto", connection);
                cmdProducto.Parameters.AddWithValue("@idProducto", idProducto);
                cmdProducto.ExecuteNonQuery();
            }
        }


        public static Producto obtenerProducto(long id)
        {
            Producto prod = new Producto();
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("select * from producto where id = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    reader.Read();
                    prod.Id = reader.GetInt64(0);
                    prod.Descripciones = reader.GetString(1);
                    prod.Costo = reader.GetSqlMoney(2).ToDouble();
                    prod.PrecioVenta = reader.GetSqlMoney(3).ToDouble();
                    prod.Stock = reader.GetInt32(4);
                    prod.IdUsuario = reader.GetInt64(5);
                }
            }
            return prod;

        }



        public static void updateStockProducto(long id, int cantidadVendidos)
        {
            Producto p = obtenerProducto(id);
            p.Stock -= cantidadVendidos;
            modificarProducto(p);
        }

    }
}

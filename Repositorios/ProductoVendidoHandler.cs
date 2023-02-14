using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionWebApi.Modelos
{
    internal class ProductoVendidoHandler
    {
        public static string cadenaConexion = "Data Source=DESKTOP-P515SRF\\SQLEXPRESS;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static List<ProductoVendido> consultaProductosVendidos(long idUsuario)
        {
            List<ProductoVendido> lista = new List<ProductoVendido>();
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("select * from ProductoVendido where ProductoVendido.IdProducto in (select Producto.Id from Producto where Producto.IdUsuario = @idUsuario)", connection);
                comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                connection.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductoVendido prod = new ProductoVendido();
                        prod.Id = reader.GetInt64(0);
                        prod.Stock = reader.GetInt32(1);
                        prod.IdProducto = reader.GetInt64(2);
                        prod.IdVenta = reader.GetInt64(3);
                        lista.Add(prod);

                    }
                }

                return lista;
            }

        }
        public static void insertarProductoVendido(ProductoVendido productoVendido)
        {
            using(SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("insert into ProductoVendido (Stock, IdProducto, IdVenta) Values(@stock, @idProducto, @idVenta)", connection);
                cmd.Parameters.AddWithValue("@stock", productoVendido.Stock);
                cmd.Parameters.AddWithValue("@idProducto", productoVendido.IdProducto);
                cmd.Parameters.AddWithValue("@idVenta", productoVendido.IdVenta);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }


    
    
    }
}

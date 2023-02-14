using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionWebApi.Modelos
{
    internal class VentaHandler
    {
        public static string cadenaConexion = "Data Source=DESKTOP-P515SRF\\SQLEXPRESS;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static List<Venta> consultaVentas(long idUsuario)
        {
            List<Venta> lista = new List<Venta>();
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("select * from venta where venta.idUsuario = @idUsuario", connection);
                comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                connection.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Venta venta = new Venta();
                        venta.Id = reader.GetInt64(0);
                        venta.Comentarios = reader.GetString(1);
                        venta.IdUsuario = reader.GetInt64(2);
                        lista.Add(venta);
                    }
                }
            }
            return lista;
        }

        public static long insertarVenta(Venta venta)
        {
            using(SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("insert into venta (Comentarios, IdUsuario) Values (@comentarios, @idUsuario); select @@identity", connection);
                cmd.Parameters.AddWithValue("@comentarios", venta.Comentarios);
                cmd.Parameters.AddWithValue("@idUsuario", venta.IdUsuario);
                connection.Open();
                return Convert.ToInt64(cmd.ExecuteScalar());
            }
        }

        public static void cargarVenta(long idUsuario, List<Producto> productosVendidos)
        {
            Venta venta = new Venta();
            venta.IdUsuario = idUsuario;
            venta.Comentarios = "nueva venta";
            long idVenta = insertarVenta(venta);
            foreach (var producto in productosVendidos)
            {
                ProductoVendido productoVendido = new ProductoVendido();
                productoVendido.Stock = Convert.ToInt32(producto.Stock);
                productoVendido.IdProducto = producto.Id;
                productoVendido.IdVenta = idVenta;
                ProductoVendidoHandler.insertarProductoVendido(productoVendido);
                ProductoHandler.updateStockProducto(productoVendido.IdProducto, Convert.ToInt32(producto.Stock));
            }
        }
    }
}

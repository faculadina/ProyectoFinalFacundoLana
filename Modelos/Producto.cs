using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace SistemaGestionWebApi.Modelos
{
    public class Producto
    {
        private long id;
        private string? descripciones;
        private double costo;
        private double? precioVenta;
        private int? stock;
        private long? idUsuario;

        public long Id { get => id; set => id = value; }
        [Required]
        public string? Descripciones { get => descripciones; set => descripciones = value; }
        public double Costo { get => costo; set => costo = value; }
        [Required]
        public double? PrecioVenta { get => precioVenta; set => precioVenta = value; }
        [Required]
        public int? Stock { get => stock; set => stock = value; }
        [Required]
        public long? IdUsuario { get => idUsuario; set => idUsuario = value; }
    }
}

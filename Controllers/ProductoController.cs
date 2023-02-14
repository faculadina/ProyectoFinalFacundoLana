using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionWebApi.Modelos;
using System.Diagnostics.Eventing.Reader;

namespace SistemaGestionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpPost]
        public void consultaProducto([FromBody]Producto producto)
        {
            ProductoHandler.crearProducto(producto);
        }

        [HttpPut]
        public void modificarProducto(Producto p)
        {
            ProductoHandler.modificarProducto(p);
        }

        [HttpDelete("{idProducto}")]
        public void eliminarProducto(long idProducto)
        {
            ProductoHandler.eliminarProducto(idProducto);
        }
    }


}

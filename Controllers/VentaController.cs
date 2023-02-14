using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionWebApi.Modelos;

namespace SistemaGestionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        [HttpPost("{idUsuario}")]
        public void cargarVenta(long idUsuario, [FromBody] List<Producto> productosVendidos)
        {
            VentaHandler.cargarVenta(idUsuario, productosVendidos);
        }
    }
}

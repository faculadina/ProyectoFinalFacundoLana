using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionWebApi.Modelos;

namespace SistemaGestionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        [HttpPut]

        public void modificarUsuario(Usuario usuario)
        {
            UsuarioHandler.modificarUsuario(usuario);
        }
        
    }
}

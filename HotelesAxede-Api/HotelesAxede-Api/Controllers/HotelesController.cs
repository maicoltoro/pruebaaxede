using HotelesAxede_Api.Clases;
using HotelesAxede_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelesAxede_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelesController : ControllerBase
    {
        private readonly MetodosHoteles _solicitudService;

        public HotelesController(MetodosHoteles solicitudService)
        {
            _solicitudService = solicitudService;
        }

        [HttpGet]
        public ActionResult<List<SpSolicitudes>> Listar (){
            var usuarios = _solicitudService.solicitudes();
            return Ok(usuarios);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            var resultado = _solicitudService.deleteUser(id);
            return Ok(resultado);
        }


        [HttpPost]
        public ActionResult<List<SpSolicitudes>> Guardar(Solicitude data)
        {
            var resultado = _solicitudService.createUser(data);
            return Ok(resultado);
        }
    }
}

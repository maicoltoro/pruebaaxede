using HotelesAxede_Api.Clases;
using HotelesAxede_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelesAxede_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlojamientoController : ControllerBase
    {

        private readonly Alojamiento _solicitudService;

        public AlojamientoController(Alojamiento solicitudService)
        {
            _solicitudService = solicitudService;
        }

        [HttpGet("{sede}")]
        public ActionResult<List<TpoAlojamiento>> Listar(string sede) {
            var usuarios = _solicitudService.Listar(sede);
            return Ok(usuarios);
        }
    }
}

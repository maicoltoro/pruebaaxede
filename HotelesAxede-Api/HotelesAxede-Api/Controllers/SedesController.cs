using HotelesAxede_Api.Clases;
using HotelesAxede_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelesAxede_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SedesController : ControllerBase
    {

        private readonly sedes _solicitudService;

        public SedesController(sedes solicitudService)
        {
            _solicitudService = solicitudService;
        }

        [HttpGet]
        public ActionResult<List<Sede>> listar() {
            var usuarios = _solicitudService.listar();
            return Ok(usuarios);
        }
    }
}

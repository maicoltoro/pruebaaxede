using HotelesAxede_Api.Clases;
using HotelesAxede_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelesAxede_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemporadaController : ControllerBase
    {
        private readonly Temporada _solicitudService;

        public TemporadaController(Temporada solicitudService)
        {
            _solicitudService = solicitudService;
        }

        [HttpGet]
        public ActionResult<List<TpoAlojamiento>> Listar()
        {
            var usuarios = _solicitudService.Listar();
            return Ok(usuarios);
        }
    }
}

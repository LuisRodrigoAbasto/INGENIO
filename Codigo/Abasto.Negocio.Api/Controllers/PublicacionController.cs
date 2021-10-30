using Abasto.Negocio.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Abasto.Negocio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionController : ControllerBase
    {
        [HttpGet]
        public IActionResult Buscar()
        {
            var lista = new IngPublicacionRepository().Get();
            return Ok(lista);
        }
    }
}

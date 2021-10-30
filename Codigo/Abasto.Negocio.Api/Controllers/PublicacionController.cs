using Abasto.Negocio.Core.Interfaces;
using Abasto.Negocio.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Abasto.Negocio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionController : ControllerBase
    {
        private readonly IIngPublicacionRepository _ingPublicacionRepository;
        public PublicacionController(IIngPublicacionRepository ingPublicacionRepository)
        {
            _ingPublicacionRepository = ingPublicacionRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Buscar()
        {
            var lista =await _ingPublicacionRepository.ToListAsync();
            return Ok(lista);
        }
    }
}

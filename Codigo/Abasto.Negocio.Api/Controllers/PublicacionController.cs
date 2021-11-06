using Abasto.Library.General;
using Abasto.Negocio.Core.Entities;
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
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var lista = await _ingPublicacionRepository.Get(id);
            return Ok(lista);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(IngPublicacion obj)
        {
            obj.MapToObject();
            await _ingPublicacionRepository.Add(obj);
            return Ok(obj.PubId);
        }
    }
}

using Abasto.Lib.Core.CustomEntities;
using Abasto.Lib.Core.DTOS;
using Abasto.Lib.Core.Entities;
using Abasto.Lib.Core.Interfaces;
using Abasto.Lib.Core.QueryFilters;
using Abasto.Lib.Infrastructure.Interfaces;
using Abasto.Negocio.Api.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Abasto.Negocio.Api.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoService _service;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public ContratoController(IContratoService service, IMapper mapper, IUriService uriService)
        {
            _service = service;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Retrieve all posts
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetContrato))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<ContratoDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetContrato([FromQuery] PostQueryFilter filters)
        {
            var posts = _service.GetContrato(filters);
            var postsDtos = _mapper.Map<IEnumerable<ContratoDto>>(posts);

            var metadata = new Metadata
            {
                TotalCount = posts.TotalCount,
                PageSize = posts.PageSize,
                CurrentPage = posts.CurrentPage,
                TotalPages = posts.TotalPages,
                HasNextPage = posts.HasNextPage,
                HasPreviousPage = posts.HasPreviousPage,
                NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetContrato))).ToString(),
                PreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetContrato))).ToString()
            };

            var response = new ApiResponse<IEnumerable<ContratoDto>>(postsDtos)
            {
                Meta = metadata
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _service.GetContrato(id);
            var postDto = _mapper.Map<ContratoDto>(post);
            var response = new ApiResponse<ContratoDto>(postDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ContratoDto postDto)
        {
            var post = _mapper.Map<Contrato>(postDto);

            await _service.InsertContrato(post);

            postDto = _mapper.Map<ContratoDto>(post);
            var response = new ApiResponse<ContratoDto>(postDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(string id, ContratoDto postDto)
        {
            var post = _mapper.Map<Contrato>(postDto);
            post.CodigoContrato = id;

            var result = await _service.UpdateContrato(post);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteContrato(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
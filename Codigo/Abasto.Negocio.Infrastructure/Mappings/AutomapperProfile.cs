using AutoMapper;
using Abasto.Negocio.Core.DTOs;
using Abasto.Negocio.Core.Entities;

namespace Abasto.Negocio.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();

            CreateMap<Security, SecurityDto>().ReverseMap();
        }
    }
}

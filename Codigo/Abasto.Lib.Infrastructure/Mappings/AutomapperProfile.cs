using AutoMapper;
using Abasto.Lib.Core.DTOS;
using Abasto.Lib.Core.Entities;

namespace Abasto.Lib.Infrastructure.Mappings
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

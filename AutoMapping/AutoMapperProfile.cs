using API.Models.DTO;
using AutoMapper;
using DemoAPIProject.Models.Domain;

namespace API.AutoMapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<RegionDTO,Region>().ReverseMap();
            CreateMap<AddRequestDTO,Region>().ReverseMap();
            CreateMap<UpdateReqDTO, Region>().ReverseMap();

            //When we have different property name
            //CreateMap<RegionDTO, Region>()
            //    .ForMember(r=>r.Name,opt => opt.MapFrom(m => m.FullName))
            //    .ReverseMap();
        }
    }
}

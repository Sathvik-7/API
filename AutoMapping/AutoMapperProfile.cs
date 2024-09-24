using API.Models.DTO;
using AutoMapper;
using DemoAPIProject.Models.Domain;

namespace API.AutoMapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            #region Region Table
            CreateMap<RegionDTO,Region>().ReverseMap();
            CreateMap<AddRegionsDTO,Region>().ReverseMap();
            CreateMap<UpdateRegionsDTO, Region>().ReverseMap();

            //When we have different property name
            //CreateMap<RegionDTO, Region>()
            //    .ForMember(r=>r.Name,opt => opt.MapFrom(m => m.FullName))
            //    .ReverseMap();
            #endregion

            #region Walks Table
            CreateMap<AddWalksDTO,Walk>().ReverseMap();
            CreateMap<WalkDTO,Walk>().ReverseMap();
            CreateMap<UpdateWalksDTO,Walk>().ReverseMap();
            #endregion

        }
    }
}

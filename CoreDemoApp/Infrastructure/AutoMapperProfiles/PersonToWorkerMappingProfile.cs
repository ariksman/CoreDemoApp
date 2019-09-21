using AutoMapper;
using CoreDemoApp.Domain.Model;
using Repository.Core.DataModel;

namespace CoreDemoApp.Infrastructure.AutoMapperProfiles
{
  public class PersonToWorkerMappingProfile : Profile
  {
    public PersonToWorkerMappingProfile()
    {
      CreateMap<Person, Worker>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
        .ReverseMap();
    }
  }
}
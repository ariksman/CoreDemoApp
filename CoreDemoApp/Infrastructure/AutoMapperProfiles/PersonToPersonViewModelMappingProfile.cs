using AutoMapper;
using CoreDemoApp.Domain.Model;
using CoreDemoApp.Views.MainWindow;

namespace CoreDemoApp.Infrastructure.AutoMapperProfiles
{
  public class PersonToPersonModelMappingProfile : Profile
  {
    public PersonToPersonModelMappingProfile()
    {
      CreateMap<Person, PersonModel>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ReverseMap();
    }
  }
}
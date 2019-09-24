using AutoMapper;
using CoreDemoApp.Domain.Model;
using CoreDemoApp.Views.MainWindow;

namespace CoreDemoApp.Infrastructure.AutoMapperProfiles
{
  public class PersonToPersonViewModelMappingProfile : Profile
  {
    public PersonToPersonViewModelMappingProfile()
    {
      CreateMap<Person, PersonViewModel>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ReverseMap();
    }
  }
}
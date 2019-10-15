using AutoMapper;
using CoreDemoApp.Domain.Model;
using CoreDemoApp.Views.MainWindow;
using Repository.Core.DataModel;

namespace CoreDemoApp.Infrastructure.AutoMapperProfiles
{
  public class PersonModelToWorkerMappingProfile : Profile
  {
    public PersonModelToWorkerMappingProfile()
    {
      CreateMap<PersonModel, Worker>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
        .ForMember(dest => dest.WorkerId, opt => opt.MapFrom(src => src.Id))
        .ReverseMap();
    }
  }
}
using AutoMapper;
using CoreDemoApp.Views.MainWindow;
using Repository.Core.DataModel;

namespace CoreDemoApp.Infrastructure.AutoMapperProfiles
{
  public class WorkerToPersonModelMappingProfile : Profile
  {
    public WorkerToPersonModelMappingProfile()
    {
      CreateMap<Worker, PersonModel>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.WorkPlace, opt => opt.MapFrom(src => src.Employer1.Name))
        .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WorkerId))
        .ReverseMap();
    }
  }
}
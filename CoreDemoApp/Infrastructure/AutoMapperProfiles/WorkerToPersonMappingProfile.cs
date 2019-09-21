using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CoreDemoApp.Domain;
using CoreDemoApp.Views.MainWindow;
using Repository.Core.DataModel;

namespace CoreDemoApp.Infrastructure.AutoMapperProfiles
{
  public class WorkerToPersonViewModelMappingProfile : Profile
  {
    public WorkerToPersonViewModelMappingProfile()
    {
      CreateMap<Worker, PersonViewModel>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.WorkPlace, opt => opt.MapFrom(src => src.Employer1.Name))
        .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WorkerId))
        .ReverseMap();
    }
  }
}

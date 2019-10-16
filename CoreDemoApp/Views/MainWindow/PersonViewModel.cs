using System;
using System.Collections.Generic;
using CoreDemoApp.Domain.Model;

namespace CoreDemoApp.Views.MainWindow
{
  public class PersonViewModel : ModelBase
  {
    public PersonModel PersonModel { get; set; }

    public PersonViewModel(PersonModel personModel)
    {
      PersonModel = personModel;
    }

    public static List<PersonViewModel> CreatePersonData(int count, Func<PersonModel, PersonViewModel> viewModelFunc)
    {
      var persons = new List<PersonViewModel>();
      var rand = new Random();
      for (int i = 0; i < count; i++)
      {
        var personModel = new PersonModel()
        {
          Id = Guid.NewGuid(),
          Name = "test " + i,
          Age = rand.Next(),
        };

        var newPerson = viewModelFunc(personModel);
        persons.Add(newPerson);
      }

      return persons;
    }

    #region Public properties
    public string Name
    {
      get => PersonModel.Name;
      set
      {
        if (PersonModel.Name == value)
        {
          return;
        }
        PersonModel.Name = value;
        NotifyPropertyChanged();
      }
    }

    public string WorkPlace
    {
      get => PersonModel.WorkPlace;
      set
      {
        if (PersonModel.WorkPlace == value)
        {
          return;
        }
        PersonModel.WorkPlace = value;
        NotifyPropertyChanged();
      }
    }

    public int Age
    {
      get => PersonModel.Age;
      set
      {
        if (PersonModel.Age == value)
        {
          return;
        }
        PersonModel.Age = value;
        NotifyPropertyChanged();
      }
    }

    public Guid Id
    {
      get => PersonModel.Id;
      set
      {
        if (PersonModel.Id == value)
        {
          return;
        }
        PersonModel.Id = value;
        NotifyPropertyChanged();
      }
    }
    #endregion
  }

}
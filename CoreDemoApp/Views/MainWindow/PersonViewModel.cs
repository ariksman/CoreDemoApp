using System;
using System.Collections.Generic;
using CoreDemoApp.Domain.Model;

namespace CoreDemoApp.Views.MainWindow
{
  public class PersonModel
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string WorkPlace { get; set; }
    public int Age { get; set; }

  }
  public class PersonViewModel : ModelBase
  {
    public PersonModel PersonModel { get; set; }
    private readonly Random _random;

    public PersonViewModel(PersonModel personModel)
    {
      PersonModel = personModel;
      _random = new Random();
    }

    public List<PersonViewModel> CreatePersonData(int count)
    {
      var persons = new List<PersonViewModel>();

      for (int i = 0; i < count; i++)
      {
        var newPerson = new PersonViewModel(new PersonModel())
        {
          Id = Guid.NewGuid(),
          Name = "test " + i,
          Age = _random.Next(),
        };

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
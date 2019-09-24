using System;
using System.Collections.Generic;

namespace CoreDemoApp.Views.MainWindow
{
  public class PersonViewModel : ModelBase
  {
    private readonly Random _random;

    public PersonViewModel()
    {
      _random = new Random();
    }

    public List<PersonViewModel> CreatePersonData(int count)
    {
      var persons = new List<PersonViewModel>();

      for (int i = 0; i < count; i++)
      {
        var newPerson = new PersonViewModel()
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
    private string _name;
    public string Name
    {
      get => _name;
      set
      {
        if (_name == value)
        {
          return;
        }
        _name = value;
        NotifyPropertyChanged();
      }
    }

    private string _workplace;
    public string WorkPlace
    {
      get => _workplace;
      set
      {
        if (_workplace == value)
        {
          return;
        }
        _workplace = value;
        NotifyPropertyChanged();
      }
    }

    private int _age;
    public int Age
    {
      get => _age;
      set
      {
        if (_age == value)
        {
          return;
        }
        _age = value;
        NotifyPropertyChanged();
      }
    }

    private Guid _id;
    public Guid Id
    {
      get => _id;
      set
      {
        if (_id == value)
        {
          return;
        }
        _id = value;
        NotifyPropertyChanged();
      }
    }
    #endregion
  }

}
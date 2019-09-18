using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;

namespace CoreDemoApp.ViewModels
{
  public class MainViewModel : ModelBase
  {
    public MainViewModel()
    {
      if (IsInDesignMode)
      {
        var persons = new PersonViewModel().CreatePersonData(20);
      }
    }


    #region Relay commands

    private RelayCommand _loadPersonsCommand;
    public RelayCommand LoadPersonsCommand => _loadPersonsCommand ??= new RelayCommand(LoadPersonsExecute);

    private RelayCommand _saveNewPersonsCommand;
    public RelayCommand SaveNewPersonsCommand => _saveNewPersonsCommand ??= new RelayCommand(SaveNewPersonsExecute);

    private RelayCommand _loadDatabaseCommand;
    public RelayCommand LoadDatabaseCommand => _loadDatabaseCommand ??= new RelayCommand(LoadDatabaseExecute);

    private RelayCommand _seedDatabaseCommand;
    public RelayCommand SeedDatabaseCommand => _seedDatabaseCommand ??= new RelayCommand(SeedDatabaseExecute);

    private RelayCommand _deletePersonCommand;
    public RelayCommand DeletePersonCommand => _deletePersonCommand ??= new RelayCommand(DeletePersonExecute);

    private void DeletePersonExecute()
    {
      //using (IUnitOfWork context = ServiceLocator.Current.GetInstance<IUnitOfWork>("LocalDatabase"))
      //{
      //  if (context.Workers.RemoveWorker(SelectedPerson.Id))
      //  {
      //    context.Complete();
      //    Persons.Remove(SelectedPerson);
      //  }
      //}
    }

    private void SeedDatabaseExecute()
    {
      //using (IUnitOfWork context = ServiceLocator.Current.GetInstance<IUnitOfWork>("LocalDatabase"))
      //{
      //  var dataSeeder = new DatabaseSeeder(context);
      //  dataSeeder.SeedDatabase();
      //}
    }

    private void LoadDatabaseExecute()
    {
      //var workers = new List<Worker>();
      //using (IUnitOfWork context = ServiceLocator.Current.GetInstance<IUnitOfWork>("LocalDatabase"))
      //{
      //  workers = context.Workers?.GetAllWorkersWithEmployers();
      //}

      //CreatePersonListFromSource(workers);

      //IsChecked = true;
    }

    private void CreatePersonListFromSource(IEnumerable<object> source)
    {
      //Persons = new ObservableCollection<Person>();

      //foreach (var worker in source)
      //{
      //  var newPerson = new Person()
      //  {
      //    Name = worker.Name,
      //    WorkPlace = worker.Employer1.Name,
      //    Age = worker.Age,
      //    Id = worker.WorkerId,
      //  };
      //  Persons.Add(newPerson);
      //}
    }

    private void SaveNewPersonsExecute()
    {
      //var newPerson = new Person()
      //{
      //  Age = _currentPersonAge,
      //  Name = _currentPersonName,
      //  Id = _currentPersonId,
      //};

      //Persons.Add(newPerson);
    }

    /// <summary>
    /// 
    /// </summary>
    private void LoadPersonsExecute()
    {
      //Persons = new ObservableCollection<Person>(new Person().CreatePersonData(25));

      //IsChecked = false;
    }

    #endregion

    #region Public collections

    private ObservableCollection<PersonViewModel> _persons;

    public ObservableCollection<PersonViewModel> Persons
    {
      get => _persons;
      set
      {
        if (_persons == value)
        {
          return;
        }

        _persons = value;
        NotifyPropertyChanged();
      }
    }

    #endregion

    #region Public properties

    private bool _isChecked;

    public bool IsChecked
    {
      get => _isChecked;
      set
      {
        _isChecked = value;
        NotifyPropertyChanged();
      }
    }

    private PersonViewModel _selectedPerson;

    public PersonViewModel SelectedPerson
    {
      get => _selectedPerson;
      set
      {
        if (_selectedPerson == value)
        {
          return;
        }

        _selectedPerson = value;

        if (_selectedPerson == null) return;

        CurrentPersonId = _selectedPerson.Id;
        CurrentPersonAge = _selectedPerson.Age;
        CurrentPersonName = _selectedPerson.Name;

        NotifyPropertyChanged();
      }
    }

    private int _currentPersonId;

    public int CurrentPersonId
    {
      get => _currentPersonId;
      set
      {
        if (_currentPersonId == value)
        {
          return;
        }

        _currentPersonId = value;
        NotifyPropertyChanged();
      }
    }

    private int _currentPersonAge;

    public int CurrentPersonAge
    {
      get => _currentPersonAge;
      set
      {
        if (_currentPersonAge == value)
        {
          return;
        }

        _currentPersonAge = value;
        NotifyPropertyChanged();
      }
    }

    private string _currentPersonName;

    public string CurrentPersonName
    {
      get => _currentPersonName;
      set
      {
        if (_currentPersonName == value)
        {
          return;
        }

        _currentPersonName = value;
        NotifyPropertyChanged();
      }
    }

    #endregion
  }
}

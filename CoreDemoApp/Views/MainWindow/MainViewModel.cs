using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using CoreDemoApp.Core.CQS;
using CoreDemoApp.Dialogs;
using CoreDemoApp.Domain;
using CSharpFunctionalExtensions;
using GalaSoft.MvvmLight.Command;
using Repository.Core.DataModel;

namespace CoreDemoApp.Views.MainWindow
{
  public class MainViewModel : ModelBase
  {
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IMapper _mapper;
    private readonly Func<IMessageDialogService> _messageDialogFunc;

    public MainViewModel(
      ICommandDispatcher commandDispatcher, 
      IQueryDispatcher queryDispatcher,
      IMapper mapper,
      Func<IMessageDialogService> messageDialogFunc)
    {
      _commandDispatcher = commandDispatcher;
      _queryDispatcher = queryDispatcher;
      _mapper = mapper;
      _messageDialogFunc = messageDialogFunc;

      if (IsInDesignMode)
      {
        var persons = new PersonViewModel().CreatePersonData(20);
      }
    }

    #region Relay commands

    private RelayCommand _clearPersonsCommand;
    public RelayCommand ClearPersonsCommand => _clearPersonsCommand ??= new RelayCommand(ClearPersonsCommandExecute);

    private RelayCommand _deleteDatabaseCommand;
    public RelayCommand DeleteDatabaseCommand => _deleteDatabaseCommand ??= new RelayCommand(DeleteDatabaseCommandExecute);

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
      var command = new RemoveSelectedPersonCommand(SelectedPerson?.Id);
      _commandDispatcher.Dispatch<RemoveSelectedPersonCommand, Result>(command)
        .OnSuccessTry(() =>
        {
          var personName = _selectedPerson.Name;
          var personId = _selectedPerson.Id;
          Persons.Remove(_selectedPerson);
          ItemCount = Persons.Count;

          _messageDialogFunc().ShowUserMessage(GetType().Name, $"Removed person: {personName}, id: {personId} from database");
        })
        .OnFailure(details =>
          _messageDialogFunc().ShowErrorMessage(GetType().Name, $"Unable to remove {_selectedPerson?.Name}", details));
    }

    private void SeedDatabaseExecute()
    {
      var command = new SeedDatabaseCommand(20);
      _commandDispatcher.Dispatch<SeedDatabaseCommand, Result>(command)
        .OnFailure(details => 
          _messageDialogFunc().ShowErrorMessage(GetType().Name, "Error while seeding database", details));
    }

    private void LoadDatabaseExecute()
    {
      var query = new LoadDataForListViewQuery();
      _queryDispatcher.Dispatch<LoadDataForListViewQuery, Result<List<Worker>>>(query)
        .OnSuccessTry(result =>
        {
          Persons = _mapper.Map<ObservableCollection<PersonViewModel>>(result);
          IsChecked = true;
          ItemCount = Persons.Count;

          _messageDialogFunc().ShowUserMessage(GetType().Name, $" Loaded {Persons.Count} items");
        });
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

    private void DeleteDatabaseCommandExecute()
    {
      var command = new RemoveAllDataFromDatabaseCommand();
      _commandDispatcher.Dispatch<RemoveAllDataFromDatabaseCommand, Result>(command)
        .OnSuccessTry(() =>
        {
          _messageDialogFunc().ShowUserMessage(GetType().Name, $"Database cleared");
        })
        .OnFailure(details =>
          _messageDialogFunc().ShowErrorMessage(GetType().Name, "Error while clearing database", details));
    }

    private void ClearPersonsCommandExecute()
    {
      Persons.Clear();
      SelectedPerson = null;
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

    private string _databaseConnectionPath;

    public string DatabaseConnectionPath
    {
      get => _databaseConnectionPath;
      set
      {
        _databaseConnectionPath = value;
        NotifyPropertyChanged();
      }
    }

    private int _itemCount;

    public int ItemCount
    {
      get => _itemCount;
      set
      {
        _itemCount = value;
        NotifyPropertyChanged();
      }
    }

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

        if (_selectedPerson == null)
        {
          CurrentPersonId = 0;
          CurrentPersonAge = 0;
          CurrentPersonName = string.Empty;
        }
        else
        {
          CurrentPersonId = _selectedPerson.Id;
          CurrentPersonAge = _selectedPerson.Age;
          CurrentPersonName = _selectedPerson.Name;
        }

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

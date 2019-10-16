using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using CoreDemoApp.Core.CQS;
using CoreDemoApp.Dialogs;
using CoreDemoApp.Domain;
using CoreDemoApp.Domain.Model;
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
    private readonly MainModel _mainModel;
    private readonly Func<IMessageDialogService> _messageDialogFunc;
    private readonly Func<PersonModel, PersonViewModel> _personViewModelFunc;

    public MainViewModel(
      ICommandDispatcher commandDispatcher,
      IQueryDispatcher queryDispatcher,
      IMapper mapper,
      MainModel mainModel,
      Func<IMessageDialogService> messageDialogFunc,
      Func<PersonModel, PersonViewModel> personViewModelFunc)
    {
      _commandDispatcher = commandDispatcher;
      _queryDispatcher = queryDispatcher;
      _mapper = mapper;
      _mainModel = mainModel;
      _messageDialogFunc = messageDialogFunc;
      _personViewModelFunc = personViewModelFunc;

      Persons = new ObservableCollection<PersonViewModel>();
      SelectedPerson = personViewModelFunc(new PersonModel());

      if (IsInDesignMode)
      {
        var persons = PersonViewModel.CreatePersonData(20, personViewModelFunc);
      }
    }

    #region Relay commands

    private RelayCommand _clearPersonsCommand;
    public RelayCommand ClearPersonsCommand => _clearPersonsCommand ??= new RelayCommand(ClearPersonsCommandExecute);

    private RelayCommand _deleteDatabaseCommand;

    public RelayCommand DeleteDatabaseCommand =>
      _deleteDatabaseCommand ??= new RelayCommand(DeleteDatabaseCommandExecute);

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
        .Tap(() =>
        {
          var personName = _selectedPerson.Name;
          var personId = _selectedPerson.Id;
          Persons.Remove(_selectedPerson);
          ItemCount = Persons.Count;

          _messageDialogFunc()
            .ShowUserMessage(GetType().Name, $"Removed person: {personName}, id: {personId} from database");
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
      var loadQuery = new LoadDataForListViewQuery();
      var databaseInfoQuery = new GetCurrentDatabaseConnectionQuery();

      _queryDispatcher.Dispatch<LoadDataForListViewQuery, Result<List<Worker>>>(loadQuery)
        .Tap(result =>
        {
          var personModels = _mapper.Map<List<PersonModel>>(result);
          foreach (var personModel in personModels)
          {
            Persons.Add(_personViewModelFunc(personModel));
          }
          IsChecked = true;
          ItemCount = Persons.Count;
        })
        .Tap(() =>
        {
          _queryDispatcher.Dispatch<GetCurrentDatabaseConnectionQuery, Result<string>>(databaseInfoQuery)
            .Tap((data => DatabaseConnectionPath = data));
        })
        .Tap(result =>
        {
          _messageDialogFunc().ShowUserMessage(GetType().Name, $" Loaded {result.Count} items");
        })
        .OnFailure(details =>
          _messageDialogFunc().ShowErrorMessage(GetType().Name, "Error while loading database", details));
    }

    private void SaveNewPersonsExecute()
    {
      var command = new AddPersonWithEmployer(_mapper.Map<Worker>(SelectedPerson.PersonModel));

      _commandDispatcher.Dispatch<AddPersonWithEmployer, Result>(command)
        .Tap(() =>
        {
          var viewModel = _personViewModelFunc(SelectedPerson.PersonModel);
          Persons.Add(viewModel);
        })
        .OnFailure(details =>
          _messageDialogFunc().ShowErrorMessage(GetType().Name, "Failed to add person into database", details));
    }

    private void DeleteDatabaseCommandExecute()
    {
      var command = new RemoveAllDataFromDatabaseCommand();
      _commandDispatcher.Dispatch<RemoveAllDataFromDatabaseCommand, Result>(command)
        .Tap(() => { _messageDialogFunc().ShowUserMessage(GetType().Name, $"Database cleared"); })
        .OnFailure(details =>
          _messageDialogFunc().ShowErrorMessage(GetType().Name, "Error while clearing database", details));
    }

    private void ClearPersonsCommandExecute()
    {
      Persons.Clear();
      SelectedPerson = null;
      ItemCount = 0;
    }

    #endregion

    #region Public collections

    public ObservableCollection<PersonViewModel> Persons { get; private set; }

    #endregion

    #region Public properties

    public string DatabaseConnectionPath
    {
      get => _mainModel.DatabaseConnectionPath;
      set
      {
        _mainModel.DatabaseConnectionPath = value;
        NotifyPropertyChanged();
      }
    }

    public int ItemCount
    {
      get => _mainModel.ItemCount;
      set
      {
        _mainModel.ItemCount = value;
        NotifyPropertyChanged();
      }
    }


    public bool IsChecked
    {
      get => _mainModel.IsChecked;
      set
      {
        _mainModel.IsChecked = value;
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

        //if (_selectedPerson == null)
        //{
        //  CurrentPersonId = Guid.Empty;
        //  CurrentPersonAge = 0;
        //  CurrentPersonName = string.Empty;
        //}
        //else
        //{
        //  CurrentPersonId = _selectedPerson.Id;
        //  CurrentPersonAge = _selectedPerson.Age;
        //  CurrentPersonName = _selectedPerson.Name;
        //}
        RaisePropertyChanged("CurrentPersonId");
        RaisePropertyChanged("CurrentPersonAge");
        RaisePropertyChanged("CurrentPersonName");
        RaisePropertyChanged();
      }
    }


    public Guid CurrentPersonId
    {
      get => SelectedPerson.Id;
      set
      {
        if (SelectedPerson.Id == value)
        {
          return;
        }

        SelectedPerson.Id = value;
        RaisePropertyChanged();
      }
    }


    public int CurrentPersonAge
    {
      get => SelectedPerson.Age;
      set
      {
        if (SelectedPerson.Age == value)
        {
          return;
        }

        SelectedPerson.Age = value;
        RaisePropertyChanged();
      }
    }


    public string CurrentPersonName
    {
      get => SelectedPerson.Name;
      set
      {
        if (SelectedPerson.Name == value)
        {
          return;
        }

        SelectedPerson.Name = value;
        RaisePropertyChanged();
      }
    }

    #endregion
  }
}
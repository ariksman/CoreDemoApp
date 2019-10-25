using System;
using System.Collections.Generic;
using AutoMapper;
using CoreDemoApp.Core.CQS;
using CoreDemoApp.Dialogs;
using CoreDemoApp.Domain;
using CoreDemoApp.Views.MainWindow;
using CSharpFunctionalExtensions;
using Xunit;
using Moq;
using Repository.Core.DataModel;

namespace CoreDemoApp.Tests
{
  public class MainViewModelTests
  {
    private Mock<ICommandDispatcher> _commandDispatcherMock;
    private Mock<IQueryDispatcher> _queryDispatcherMock;
    private Mock<IMapper> _mapperMock;
    private Mock<MainModel> _mainModelMock;
    private Mock<IMessageDialogService> _messageDialogMock;

    public MainViewModelTests()
    {
      _commandDispatcherMock = new Mock<ICommandDispatcher>();
      _queryDispatcherMock = new Mock<IQueryDispatcher>();
      _mapperMock = new Mock<IMapper>();
      _mainModelMock = new Mock<MainModel>();
      _messageDialogMock = new Mock<IMessageDialogService>();
    }

    [Fact]
    public void ClearPersonsCommand_When_Called_Empties_Persons_Collection()
    {
      var messageFunc = new Func<IMessageDialogService>(() => _messageDialogMock.Object);
      var personViewModelFunc = new Func<PersonModel, PersonViewModel>(model => new PersonViewModel(new PersonModel()));
      var viewModel = new MainViewModel(
        _commandDispatcherMock.Object,
        _queryDispatcherMock.Object,
        _mapperMock.Object,
        _mainModelMock.Object,
        messageFunc,
        personViewModelFunc
        );
      viewModel.Persons.Add(new PersonViewModel(new PersonModel()));

      viewModel.ClearPersonsCommand.Execute(null);

      Assert.Empty(viewModel.Persons);
    }

    [Fact]
    public void ClearPersonsCommand_When_Called_SelectedPerson_Is_Set_To_Null()
    {
      var messageFunc = new Func<IMessageDialogService>(() => _messageDialogMock.Object);
      var personViewModelFunc = new Func<PersonModel, PersonViewModel>(model => new PersonViewModel(new PersonModel()));
      var viewModel = new MainViewModel(
        _commandDispatcherMock.Object,
        _queryDispatcherMock.Object,
        _mapperMock.Object,
        _mainModelMock.Object,
        messageFunc,
        personViewModelFunc);
      viewModel.SelectedPerson = new PersonViewModel(new PersonModel());

      viewModel.ClearPersonsCommand.Execute(null);

      Assert.Null(viewModel.SelectedPerson);
    }

    [Fact]
    public void LoadDatabaseCommand_When_Called_Adds_Returned_Workers_To_Persons_Collection()
    {
      var personModel = new PersonModel() {Name = "testModel"};
      var personViewModel =  new PersonViewModel(personModel);
      var messageFunc = new Func<IMessageDialogService>(() => _messageDialogMock.Object);
      var personViewModelFunc = new Func<PersonModel, PersonViewModel>(model => personViewModel);
      _queryDispatcherMock.Setup(m =>
          m.Dispatch<LoadDataForListViewQuery, Result<List<Worker>>>(It.IsAny<LoadDataForListViewQuery>()))
        .Returns(Result.Ok(new List<Worker>()));
      _mapperMock.Setup(m => m.Map<List<PersonModel>>(It.IsAny<object>()))
        .Returns(new List<PersonModel>() {personModel});
      _queryDispatcherMock.Setup(m =>
          m.Dispatch<GetCurrentDatabaseConnectionQuery, Result<string>>(It.IsAny<GetCurrentDatabaseConnectionQuery>()))
        .Returns(Result.Ok("ok"));
      var viewModel = new MainViewModel(
        _commandDispatcherMock.Object,
        _queryDispatcherMock.Object,
        _mapperMock.Object,
        _mainModelMock.Object,
        messageFunc,
        personViewModelFunc);

      viewModel.LoadDatabaseCommand.Execute(null);

      Assert.Contains(personViewModel, viewModel.Persons);
    }
  }
}
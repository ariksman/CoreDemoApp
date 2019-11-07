using System;
using System.Collections.Generic;
using CoreDemoApp.Application;
using CoreDemoApp.Core.CQS;
using CoreDemoApp.Domain;
using CSharpFunctionalExtensions;
using Moq;
using Repository.Core;
using Repository.Core.DataModel;
using Xunit;

namespace CoreDemoApp.Tests
{
  public class LoadDataFromDatabaseCommandHandlerTests
  {
    private Mock<ICommandDispatcher> _commandDispatcherMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;

    public LoadDataFromDatabaseCommandHandlerTests()
    {
      _unitOfWorkMock = new Mock<IUnitOfWork>();
      _commandDispatcherMock = new Mock<ICommandDispatcher>();
    }

    [Fact]
    public void When_Invoked_With_Invalid_Persistence_Object_Throws_Exception()
    {
      Assert.Throws<ArgumentNullException>(() => new LoadDataForListViewQueryHandler(null));
    }

    [Fact]
    public void After_Successful_Transaction_Return_Success_Result_With_Data()
    {
      var command = new LoadDataForListViewQuery();
      var handler = new LoadDataForListViewQueryHandler(_unitOfWorkMock.Object);

      _unitOfWorkMock
        .Setup(m => m.Workers.GetAllWorkersWithEmployers())
        .Returns(new List<Worker>());

      var result = handler.Handle(command);

      Assert.True(result.IsSuccess);
      Assert.NotNull(result.Value);
    }

    [Fact]
    public void After_Failed_Transaction_Returns_Failed_Result()
    {
      var command = new LoadDataForListViewQuery();
      var handler = new LoadDataForListViewQueryHandler(_unitOfWorkMock.Object);

      _unitOfWorkMock
        .Setup(m => m.Workers.GetAllWorkersWithEmployers())
        .Returns(() => throw new InvalidOperationException());

      var result = handler.Handle(command);

      Assert.True(result.IsFailure);
    }
  }

  public class AddPersonWithEmployerCommandHandlerTests
  {
    private Mock<ICommandDispatcher> _commandDispatcherMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;

    public AddPersonWithEmployerCommandHandlerTests()
    {
      _commandDispatcherMock = new Mock<ICommandDispatcher>();
      _unitOfWorkMock = new Mock<IUnitOfWork>();
    }

    [Fact]
    public void When_Invoked_With_Invalid_Persistence_Object_Throws_Exception()
    {
      Assert.Throws<ArgumentNullException>(() => new AddPersonWithEmployerCommandHandler(null));
    }

    [Fact]
    public void Uses_Persistency_To_Add_New_Worker_Into_Database()
    {
      var command = new AddPersonWithEmployer(new Worker());
      var handler = new AddPersonWithEmployerCommandHandler(_unitOfWorkMock.Object);

      _unitOfWorkMock
        .Setup(m => m.Workers.AddWorker(It.IsAny<Worker>()))
        .Returns(() => true);

      handler.Handle(command);

      _unitOfWorkMock.Verify(m => m.Workers.AddWorker(It.IsAny<Worker>()), Times.Once);
    }

    [Fact]
    public void Completes_Transaction_After_Succesful_Run()
    {
      var command = new AddPersonWithEmployer(new Worker());
      var handler = new AddPersonWithEmployerCommandHandler(_unitOfWorkMock.Object);

      _unitOfWorkMock
        .Setup(m => m.Workers.AddWorker(It.IsAny<Worker>()))
        .Returns(() => true);

      handler.Handle(command);

      _unitOfWorkMock.Verify(m => m.Complete(), Times.Once);
    }

    [Fact]
    public void Returns_Failed_Result_When_Transaction_With_Database_Throws_Exception()
    {
      var command = new AddPersonWithEmployer(new Worker());
      var handler = new AddPersonWithEmployerCommandHandler(_unitOfWorkMock.Object);

      _unitOfWorkMock
        .Setup(m => m.Workers.AddWorker(It.IsAny<Worker>()))
        .Returns(() => throw new Exception());

      var result = handler.Handle(command);

      Assert.True(result.IsFailure);
    }

    [Fact]
    public void After_Succesful_Transaction_Completes_Operation()
    {
      var command = new AddPersonWithEmployer(new Worker());
      var handler = new AddPersonWithEmployerCommandHandler(_unitOfWorkMock.Object);

      _unitOfWorkMock
        .Setup(m => m.Workers.AddWorker(It.IsAny<Worker>()))
        .Returns(true);

      var result = handler.Handle(command);

      if (result.IsSuccess)
      {
        _unitOfWorkMock.Verify(m => m.Complete(), Times.Once);
      }
      else
      {
        _unitOfWorkMock.Verify(m => m.Complete(), Times.Never);
      }
    }
  }
}
using System;
using CoreDemoApp.Application;
using CoreDemoApp.Core.CQS;
using CoreDemoApp.Domain;
using Moq;
using Repository.Core;
using Repository.Core.DataModel;
using Xunit;

namespace CoreDemoApp.Tests
{
  public class RemoveSelectedPersonCommandHandlerTests
  {
    private Mock<ICommandDispatcher> _commandDispatcherMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;

    public RemoveSelectedPersonCommandHandlerTests()
    {
      _unitOfWorkMock = new Mock<IUnitOfWork>();
      _commandDispatcherMock = new Mock<ICommandDispatcher>();
    }

    [Fact]
    public void When_Invoked_With_Invalid_Persistence_Object_Throws_Exception()
    {
      Assert.Throws<ArgumentNullException>(() => new RemoveSelectedPersonCommandHandler(null));
    }

    [Fact]
    public void Gets_Given_Worker_From_DataBase()
    {
      var id = Guid.NewGuid();
      var worker = new Worker {WorkerId = id};
      var command = new RemoveSelectedPersonCommand(id);

      _unitOfWorkMock
        .Setup(m => m.Workers.GetById(It.IsAny<Guid>()))
        .Returns(worker);

      var handler = new RemoveSelectedPersonCommandHandler(_unitOfWorkMock.Object);
      handler.Handle(command);

      _unitOfWorkMock
        .Verify(
          m => m.Workers.GetById(It.Is<Guid>((args) => args.Equals(id))), Times.Once);
    }

    [Fact]
    public void Removes_Given_Worker_From_DataBase()
    {
      var id = Guid.NewGuid();
      var worker = new Worker { WorkerId = id };
      var command = new RemoveSelectedPersonCommand(id);

      _unitOfWorkMock
        .Setup(m => m.Workers.GetById(It.IsAny<Guid>()))
        .Returns(worker);
      _unitOfWorkMock
        .Setup(m => m.Workers.RemoveWorker(It.IsAny<Guid>()))
        .Returns(true);

      var handler = new RemoveSelectedPersonCommandHandler(_unitOfWorkMock.Object);
      handler.Handle(command);

      _unitOfWorkMock
        .Verify(
          m => m.Workers.RemoveWorker(It.Is<Worker>(args => args.WorkerId == id)));
    }

    [Fact]
    public void After_Successful_Transaction_Calls_Complete()
    {
      var id = Guid.NewGuid();
      var worker = new Worker { WorkerId = id };
      var command = new RemoveSelectedPersonCommand(id);

      _unitOfWorkMock
        .Setup(m => m.Workers.GetById(It.IsAny<Guid>()))
        .Returns(worker);
      _unitOfWorkMock
        .Setup(m => m.Workers.RemoveWorker(It.IsAny<Guid>()))
        .Returns(true);

      var handler = new RemoveSelectedPersonCommandHandler(_unitOfWorkMock.Object);
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
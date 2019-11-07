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
    public void When_Invoked_With_Invalid_Persistence_Object_Throw_Exception()
    {
      Assert.Throws<ArgumentNullException>(() => new AddPersonWithEmployerCommandHandler(null));
    }

    [Fact]
    public void Command_Uses_Persistency_To_Add_New_Worker_Into_Database()
    {
      var command = new AddPersonWithEmployer(new Worker());
      var handler = new AddPersonWithEmployerCommandHandler(_unitOfWorkMock.Object);

      _unitOfWorkMock
        .Setup(m => m.Workers.AddWorker(It.IsAny<Worker>()))
        .Returns(() => true);

      handler.Handle(command);

      _unitOfWorkMock.Verify(m => m.Workers.AddWorker(It.IsAny<Worker>()), Times.Once);
    }
  }
}
using System;
using CoreDemoApp.Application;
using CoreDemoApp.Core.CQS;
using Moq;
using Xunit;

namespace CoreDemoApp.Tests
{
  public class AddPersonWithEmployerCommandHandlerTests
  {
    private Mock<ICommandDispatcher> _commandDispatcherMock;

    public AddPersonWithEmployerCommandHandlerTests()
    {
      _commandDispatcherMock = new Mock<ICommandDispatcher>();
    }

    [Fact]
    public void When_Invoked_With_Invalid_Persistence_Object_Throw_Exception()
    {
      Assert.Throws<ArgumentNullException>(() => new AddPersonWithEmployerCommandHandler(null));
    }
  }
}
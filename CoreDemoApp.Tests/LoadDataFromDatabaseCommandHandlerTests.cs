using System;
using System.Collections.Generic;
using CoreDemoApp.Application;
using CoreDemoApp.Core.CQS;
using CoreDemoApp.Domain;
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
}
using System;
using Moq;
using NUnit.Framework;
using Repository.Core.Repositories;
using Repository.EFCore;

namespace Repository.IntegrationTests
{
  public class EfUnitOfWorkTests
  {
    private Mock<IWorkerRepository> _workerRepositoryMock;
    private Mock<IEmployerRepository> _employerRepository;

    [SetUp]
    public void Setup()
    {
      _workerRepositoryMock = new Mock<IWorkerRepository>();
      _employerRepository = new Mock<IEmployerRepository>();
    }

    [Test]
    public void On_Creation_Exception_Is_Thrown_If_Null_DatabaseConnection_Is_Ijected()
    {
      Assert.Throws<ArgumentNullException>(() => new EfUnitOfWork(
        null,
        (d) => _employerRepository.Object,
        (d) => _workerRepositoryMock.Object));
    }
  }
}
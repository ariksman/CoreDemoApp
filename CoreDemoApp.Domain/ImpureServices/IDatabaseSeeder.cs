using Repository.Core;

namespace CoreDemoApp.Domain.ImpureServices
{
  public interface IDatabaseSeeder
  {
    void SeedDatabase(IUnitOfWork unitOfWork);
  }
}
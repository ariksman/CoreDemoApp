using Repository.Core;

namespace CoreDemoApp.Domain
{
  public interface IDatabaseSeeder
  {
    void SeedDatabase(IUnitOfWork unitOfWork);
  }
}
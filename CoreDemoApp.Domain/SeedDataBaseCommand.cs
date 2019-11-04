using CoreDemoApp.Core.CQS;

namespace CoreDemoApp.Domain
{
  public class SeedDatabaseCommand : ICommand
  {
    public SeedDatabaseCommand(int size, string connectionString = null)
    {
      Size = size;
      ConnectionString = connectionString;
    }

    public int Size { get; }
    public string ConnectionString { get; }
  }
}
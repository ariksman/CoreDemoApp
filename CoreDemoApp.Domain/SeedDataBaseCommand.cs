using CoreDemoApp.Common.CQS;

namespace CoreDemoApp.Domain
{
  public class SeedDatabaseCommand : ICommand
  {
    public int Size { get; set; }
    public string ConnectionString { get; set; }

    public SeedDatabaseCommand(int size, string connectionString = null)
    {
      Size = size;
      ConnectionString = connectionString;
    }
  }
}
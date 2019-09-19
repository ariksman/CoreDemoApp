using CoreDemoApp.Common.CQS;

namespace CoreDemoApp.Domain
{
  public class SeedDatabaseCommand : ICommand
  {
    public int Size { get; set; }
    public string Connectionstring { get; set; }

    public SeedDatabaseCommand(int size, string connectionstring = null)
    {
      Size = size;
      Connectionstring = connectionstring;
    }
  }
}
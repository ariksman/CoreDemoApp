using CoreDemoApp.Core.CQS;
using Repository.Core.DataModel;

namespace CoreDemoApp.Domain
{
  public class AddPersonWithEmployer : ICommand
  {
    public Worker Worker { get; }

    public AddPersonWithEmployer(Worker worker)
    {
      Worker = worker;
    }
  }
}
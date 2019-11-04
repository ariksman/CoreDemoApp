using CoreDemoApp.Core.CQS;
using Repository.Core.DataModel;

namespace CoreDemoApp.Domain
{
  public class AddPersonWithEmployer : ICommand
  {
    public AddPersonWithEmployer(Worker worker)
    {
      Worker = worker;
    }

    public Worker Worker { get; }
  }
}
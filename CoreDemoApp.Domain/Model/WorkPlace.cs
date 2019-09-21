using CoreDemoApp.Domain.DDD;

namespace CoreDemoApp.Domain.Model
{
  public class WorkPlace : Entity
  {
    public WorkPlace()
    {
    }

    public string Name { get; set; }
  }
}
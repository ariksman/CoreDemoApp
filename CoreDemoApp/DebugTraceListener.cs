using System.Diagnostics;

namespace CoreDemoApp
{
  public class DebugTraceListener : TraceListener
  {
    public override void Write(string message)
    {
      Debugger.Break();
    }

    public override void WriteLine(string message)
    {
      Debugger.Break();
    }
  }
}
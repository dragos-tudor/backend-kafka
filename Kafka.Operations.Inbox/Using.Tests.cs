
global using Microsoft.Extensions.Logging.Abstractions;
global using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kafka.Operations.Inbox;

[TestClass]
public partial class InboxTests
{
  class InstrumentationServices : IInstrumentationServices
  {
    public ActivitySource GetActivitySource() => new ("inbox");
    public ILogger GetLogger() => NullLogger.Instance;
  }
}
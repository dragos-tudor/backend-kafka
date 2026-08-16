
global using Microsoft.Extensions.Logging.Abstractions;
global using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kafka.Operations.DeadLetter;

[TestClass]
public partial class DeadLetterTests
{
  class InstrumentationServices : IInstrumentationServices
  {
    public ActivitySource GetActivitySource() => new ("deadletter");
    public ILogger GetLogger() => NullLogger.Instance;
  }
}
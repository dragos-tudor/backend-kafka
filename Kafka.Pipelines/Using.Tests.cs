#pragma warning disable CA2000
#pragma warning disable CA2025

global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static System.Threading.CancellationTokenSource;
global using Shouldly;

namespace Kafka.Pipelines;

[TestClass]
public partial class PipelinesTests
{
  [TestMethod]
  public void FakeTest() {}
}
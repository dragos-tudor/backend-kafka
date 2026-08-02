#pragma warning disable CA2000
#pragma warning disable CA2025

global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static System.Threading.CancellationTokenSource;
global using Shouldly;

namespace Kafka.Engine;

[TestClass]
public partial class EngineTests
{
  [TestMethod]
  public void FakeTest() {}
}
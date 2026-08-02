
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kafka.Observability;

[TestClass]
public partial class ObservabilityTests
{
  internal static TracerProvider CreateTracerProvider(string serviceName, string sourcename) =>
    Sdk.CreateTracerProviderBuilder()
      .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
      .AddSource(sourcename)
      .Build();
}
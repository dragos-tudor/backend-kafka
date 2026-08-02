
namespace Kafka.Observability;

partial class ObservabilityFuncs
{
  static string[] SplitTraceParent(string traceParent) =>
    traceParent.Split('-', StringSplitOptions.RemoveEmptyEntries);
}
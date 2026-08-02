
namespace Kafka.Observability;

partial class ObservabilityFuncs
{
  static bool HasMinTraceParentParts(string[] parts, int minParts = 4) => parts.Length >= minParts;
}
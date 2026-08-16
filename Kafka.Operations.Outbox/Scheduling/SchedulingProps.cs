
namespace Kafka.Operations.Outbox;

partial class OutboxFuncs
{
  static string RequireProduceError(
    string? produceError) =>
    produceError ?? throw new InvalidOperationException("Produce error is required.");
}
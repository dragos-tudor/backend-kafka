
namespace Kafka.Operations.DeadLetter;

partial class DeadLetterFuncs
{
  static string RequireProduceError(
    string? produceError) =>
    produceError ?? throw new InvalidOperationException("Produce error is required.");
}
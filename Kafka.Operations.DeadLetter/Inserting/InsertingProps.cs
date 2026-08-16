
namespace Kafka.Operations.DeadLetter;

partial class DeadLetterFuncs
{
  static DeadLetterMessage<TKey, TPayload> RequireDeadLetterMessage<TKey, TPayload>(
    DeadLetterMessage<TKey, TPayload>? deadLetterMessage) =>
    deadLetterMessage ?? throw new InvalidOperationException("Deadletter message is required.");
}
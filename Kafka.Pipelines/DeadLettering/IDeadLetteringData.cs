namespace Kafka.Pipelines;

internal interface IDeadLetteringData<TKey, TPayload>:
  Operations.DeadLetter.IConvertingData<TKey, TPayload>,
  Operations.DeadLetter.IInsertingData<TKey, TPayload>;

internal sealed class DeadLetteringData<TKey, TPayload>:
  IDeadLetteringData<TKey, TPayload>
{
  public InboxMessage<TKey, TPayload>? InboxMessage { get; set; }
  public string? InboxMessageError { get; set; }
  public DeadLetterMessage<TKey, TPayload>? DeadLetterMessage { get; set; }
}

partial class PipelinesFuncs
{
  internal static IDeadLetteringData<TKey, TPayload> CreateDeadLetteringData<TKey, TPayload>(
    InboxMessage<TKey, TPayload> message) =>
    new DeadLetteringData<TKey, TPayload>
    {
      InboxMessage = message,
      InboxMessageError = message.LastError,
    };
}


namespace Kafka.Pipelines;

internal interface IResumingData<TKey, TPayload>:
  IHandlingData<TKey, TPayload>,
  Operations.Inbox.ISchedulingData<TKey, TPayload>;

internal sealed class ResumingData<TKey, TPayload>:
  IResumingData<TKey, TPayload>
{
  public InboxMessage<TKey, TPayload>? InboxMessage { get; set; }
  public string? InboxMessageError { get; set; }
}

partial class PipelinesFuncs
{
  internal static IResumingData<TKey, TPayload> CreateResumingData<TKey, TPayload>(
    InboxMessage<TKey, TPayload> message) =>
    new ResumingData<TKey, TPayload>
    {
      InboxMessage = message,
    };
}
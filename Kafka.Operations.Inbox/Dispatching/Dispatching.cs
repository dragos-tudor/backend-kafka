using static Kafka.Operations.Inbox.DispatchingStates;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  internal static async ValueTask<(TData, string)> DispatchDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IDispatchingServices<TKey, TValue, TPayload>
  where TData : IDispatchingData<TKey, TValue, TPayload>
  {
    var message = data.InboxMessage!;
    try {
      var handleError = data.HandleError!;
      var topicPartitionOffset = data.TopicPartitionOffset!;

      var deadLetterTopic = services.GetDeadLetterTopic(data.InboxMessage!);
      var deadLetter = ToKafkaDeadLetter(message, topicPartitionOffset, handleError, services.GetUtcDate(), services.ToKafkaValue);
      data.DeadLetter = deadLetter;

      if (message.Status != InboxMessageStatus.Dispatching)
        await services.UpdateIntegrationMessageAsync(message, message =>
          message.SetInboxMessageStatus(InboxMessageStatus.Dispatching), ct);

      InjectTraceParentActivity(Activity.Current, deadLetter.Headers);
      await PublishMessageAsync(services.GetProducer(data.Pipeline), deadLetterTopic, deadLetter, ct);
      InstrumentDispatchedDeadLetter(message.MessageId, deadLetter.Key?.ToString(), deadLetterTopic, handleError, services);

      await services.UpdateIntegrationMessageAsync(message, message =>
        message.SetInboxMessageStatus(InboxMessageStatus.Dispatched), ct);
      return (data, DispatchedDeadLetterState);
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex) {
      data.DispatchError = ex.Message;
      InstrumentDispatchDeadLetterError(message.MessageId, ex, services);
      return ex is KafkaException
        ? (data, DispatchDeadLetterCriticalErrorState)
        : (data, DispatchDeadLetterErrorState);
    }
  }
}
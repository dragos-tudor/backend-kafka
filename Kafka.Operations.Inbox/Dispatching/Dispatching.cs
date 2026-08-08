
namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  internal static async ValueTask<(TData, string)> DispatchDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IDispatchDeadLetterServices<TKey, TValue, TPayload>
  where TData : IDispatchDeadLetterData<TKey, TValue, TPayload>
  {
    var message = data.InboxMessage!;
    try {
      var handleError = data.HandleError!;
      var topicPartitionOffset = data.TopicPartitionOffset!;

      var deadLetterTopic = services.GetDeadLetterTopic(data.InboxMessage!);
      var deadLetter = ToKafkaDeadLetter(message, topicPartitionOffset, handleError, services.GetUtcDate(), services.ToKafkaValue);
      data.DeadLetter = deadLetter;

      if (message.Status != InboxMessageStatus.DeadLettering)
        await services.UpdateIntegrationMessageAsync(message, InboxMessageStatus.DeadLettering, ct);

      InjectTraceParentActivity(Activity.Current, deadLetter.Headers);
      await PublishMessageAsync(services.GetProducer(), deadLetterTopic, deadLetter, ct);
      InstrumentDispatchedDeadLetter(message.MessageId, deadLetter.Key?.ToString(), deadLetterTopic, handleError, services);

      await services.UpdateIntegrationMessageAsync(message, InboxMessageStatus.DeadLettered, ct);
      return (data, DispatchedDeadLetterState);
    }
    catch (Exception ex) {
      InstrumentDispatchDeadLetterError(message.MessageId, ex, services);
      return (data, DispatchDeadLetterErrorState);
    }
  }
}
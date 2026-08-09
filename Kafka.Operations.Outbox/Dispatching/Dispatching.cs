using static Kafka.Operations.Outbox.DispatchingStates;

namespace Kafka.Operations.Outbox;

partial class OutboxFuncs
{
  internal static async ValueTask<(TData, string)> DispatchDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IDispatchDeadLetterServices<TKey, TValue, TPayload>
  where TData : IDispatchDeadLetterData<TKey, TValue, TPayload>
  {
    var message = data.OutboxMessage!;
    try {
      var publishError = data.PublishError!;

      var deadLetterTopic = services.GetDeadLetterTopic(data.OutboxMessage!);
      var deadLetter = ToKafkaDeadLetter(message, default, publishError, services.GetUtcDate(), services.ToKafkaValue);
      data.DeadLetter = deadLetter;

      if (message.Status != OutboxMessageStatus.Dispatching)
        await services.UpdateIntegrationMessageAsync(message, message =>
          message.SetOutboxMessageStatus(OutboxMessageStatus.Dispatching), ct);

      InjectTraceParentActivity(Activity.Current, deadLetter.Headers);
      await PublishMessageAsync(services.GetProducer(), deadLetterTopic, deadLetter, ct);
      InstrumentDispatchedDeadLetter(message.MessageId, deadLetter.Key?.ToString(), deadLetterTopic, publishError, services);

      await services.UpdateIntegrationMessageAsync(message, message =>
        message.SetOutboxMessageStatus(OutboxMessageStatus.Dispatched), ct);
      return (data, DispatchedDeadLetterState);
    }
    catch (OperationCanceledException) { return default; }
    catch (KafkaException ex) {
      data.DispatchError = ex.Message;
      InstrumentDispatchDeadLetterError(message.MessageId, ex, services);
      return (data, DispatchDeadLetterCriticalErrorState);
    }
    catch (Exception ex) {
      data.DispatchError = ex.Message;
      InstrumentDispatchDeadLetterError(message.MessageId, ex, services);
      return (data, DispatchDeadLetterErrorState);
    }
  }
}
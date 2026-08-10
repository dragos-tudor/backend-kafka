using static Kafka.Operations.Outbox.PublishingStates;

namespace Kafka.Operations.Outbox;

partial class OutboxFuncs
{
  internal static async ValueTask<(TData, string)> PublishOutboxMessageAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData state,
    CancellationToken ct = default)
  where TServices : IPublishingServices<TKey, TValue, TPayload>
  where TData : IPublishingData<TKey, TValue, TPayload>
  {
    var message = state.OutboxMessage!;
    try
    {
      var topic = services.GetOutboxTopic(message);
      var kafkaMessage = ToKafkaMessage(message, services.GetUtcDate(), services.ToKafkaValue);
      await PublishMessageAsync(services.GetProducer(state.Pipeline), topic, kafkaMessage, ct);
      InstrumentPublishedOutboxMessage(message.MessageId, kafkaMessage.Key!.ToString(), topic, services);

      await services.UpdateIntegrationMessageAsync(message, message =>
        message.SetOutboxMessageStatus(OutboxMessageStatus.Published), ct);
      return new (state, PublishedOutboxMessageState);
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex)
    {
      InstrumentPublishOutboxMessageError(message.MessageId, ex, services);
      return ex is KafkaException
        ? new (state, PublishOutboxMessageCriticalErrorState)
        : new (state, PublishOutboxMessageErrorState);
    }
  }
}
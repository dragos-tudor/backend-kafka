
namespace Kafka.Operations.Outbox;

partial class OutboxFuncs
{
  internal static async ValueTask<(TState, string)> PublishOutboxMessageAsync<TServices, TState, TKey, TValue, TPayload>(
    TServices services,
    TState state,
    CancellationToken ct = default)
  where TServices : IPublishOutboxMessageServices<TKey, TValue, TPayload>
  where TState : IPublishOutboxMessageData<TKey, TValue, TPayload>
  {
    var message = state.OutboxMessage!;
    try
    {
      var topic = services.GetOutboxTopic(message);
      var kafkaMessage = ToKafkaMessage(message, services.GetUtcDate(), services.ToKafkaValue);
      await PublishMessageAsync(services.GetProducer(), topic, kafkaMessage, ct);
      InstrumentPublishedOutboxMessage(message.MessageId, kafkaMessage.Key!.ToString(), topic, services);

      await services.UpdateIntegrationMessageAsync(message, message =>
        message.SetOutboxMessageStatus(OutboxMessageStatus.Published), ct);
      return new (state, PublishedOutboxMessageState);
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex)
    {
      InstrumentPublishOutboxMessageError(message.MessageId, ex, services);
      return new (state, PublishOutboxMessageErrorState);
    }
  }
}
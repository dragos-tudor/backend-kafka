
namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  internal static ValueTask<(TData, string)> OffsetConsumer<TService, TData, TKey, TValue, TPayload>(
    TService services,
    TData data,
    CancellationToken ct = default)
  where TService : IOffsetConsumerServices<TKey, TValue>
  where TData : IOffsetConsumerData<TKey, TPayload>
  {
    var topicPartitionOffset = data.TopicPartitionOffset!;
    var inboxMessage = data.InboxMessage;
    try {
      var offsetApplied = ClientsFuncs.OffsetConsumer(services.GetConsumer(), topicPartitionOffset, services.GetKafkaOptions());

      if (inboxMessage is null) {
        InstrumentOffsetConsumerMissingMessage(topicPartitionOffset, services);
        return new((data, MissingInboxMessageState));
      }

      InstrumentOffsetConsumer(inboxMessage.MessageId, topicPartitionOffset, services);
      return new ((data, OffsetConsumedState));
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex) {
      InstrumentOffsetConsumerError(ex, inboxMessage?.MessageId, topicPartitionOffset!, services);
      return new((data, OffsetConsumeErrorState));
    }
  }
}
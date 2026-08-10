using static Kafka.Operations.Inbox.OffsettingStates;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  internal static ValueTask<(TData, string)> OffsetConsumer<TService, TData, TKey, TValue, TPayload>(
    TService services,
    TData data,
    CancellationToken ct = default)
  where TService : IOffsettingServices<TKey, TValue>
  where TData : IOffsettingData<TKey, TPayload>
  {
    var offset = data.TopicPartitionOffset!;
    try {
      var offsetApplied = ClientsFuncs.OffsetConsumer(services.GetConsumer(), offset, services.GetKafkaOptions());

      var hasMessage = data.InboxMessage is not null;
      if (!hasMessage) {
        InstrumentOffsetConsumerMissingMessage(offset, services);
        return new((data, MissingInboxMessageState));
      }

      data.TopicPartitionOffsetApplied = true;
      InstrumentOffsetConsumer(offset, services);
      return new ((data, OffsetConsumedState));
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex) {
      InstrumentOffsetConsumerError(ex, offset!, services);
      return ex is KafkaException
        ? new((data, OffsetConsumeCriticalErrorState))
        : new((data, OffsetConsumeErrorState));
    }
  }
}
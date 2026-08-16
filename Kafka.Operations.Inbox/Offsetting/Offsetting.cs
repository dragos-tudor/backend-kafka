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
    try {
      var offset = RequireTopicPartitionOffset(data.TopicPartitionOffset);
      var offsetApplied = ClientsFuncs.OffsetConsumer(services.GetConsumer(), offset, services.GetKafkaOptions());
      data.TopicPartitionOffsetApplied = offsetApplied;

      InstrumentOffsetConsumer(offset, services);
      return new ((data, OffsetConsumedState));
    }
    catch (OperationCanceledException) { return default; }
    catch (KafkaException ex) when (ex.Error.IsFatal) {
      InstrumentOffsetConsumerCriticalError(ex, data.TopicPartitionOffset, services);
      return new ((data, OffsetConsumeCriticalErrorState));
    }
    catch (Exception ex) {
      InstrumentOffsetConsumerError(ex, data.TopicPartitionOffset, services);
      return new ((data, OffsetConsumeErrorState));
    }
  }
}
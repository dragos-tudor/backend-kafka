using static Kafka.Operations.OperationState;

namespace Kafka.Operations;

partial class OperationsFuncs
{
  internal static TopicPartitionOffset? OffsetConsumer<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    TopicPartitionOffset offset,
    KafkaOptions kafkaOptions)
  =>
    (kafkaOptions.EnableAutoOffsetStore, kafkaOptions.EnableAutoCommit) switch {
      (true, true) => default,
      (false, true) => StoreConsumerOffset(consumer, offset),
      (_, false) => CommitConsumerOffset(consumer, offset),
    };

  internal static ValueTask<(TData, OperationState)> OffsetConsumer<TService, TData, TKey, TValue, TPayload>(
    TService services,
    TData data,
    CancellationToken ct)
  where TService : IOffsetConsumerServices<TKey, TValue>
  where TData : IOffsetConsumerData<TKey, TPayload>
  {
    var topicPartitionOffset = data.TopicPartitionOffset!;

    var offsetApplied = OffsetConsumer(services.GetConsumer(), topicPartitionOffset, services.GetKafkaOptions());
    data.OffsetApplied = offsetApplied is not null;

    if (data.InboxMessage is null) return new((data, MissingInboxMessageState));

    InstrumentOffsetConsumer(offsetApplied, services);
    return new ((data, OffsetConsumerState));
  }
}
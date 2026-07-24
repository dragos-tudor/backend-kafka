
namespace Kafka.Client;

partial class KafkaFuncs
{
  public static void StoreConsumedMessageOffset<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    ConsumeResult<TKey, TValue> consumeResult)
    => consumer.StoreOffset(consumeResult);
}

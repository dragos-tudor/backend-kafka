
namespace Kafka;

partial class KafkaFuncs
{
  static Result<(IProducer<TKey, TValue> Producer, IConsumer<TKey, TValue> Consumer), string?> CreateKafkaClients<TKey, TValue>(
    ConsumerConfig consumerConfig,
    ProducerConfig producerConfig,
    ILogger logger)
  {
    try
    {
      var consumer = CreateKafkaConsumer<TKey, TValue>(consumerConfig);
      var producer = CreateKafkaProducer<TKey, TValue>(producerConfig);
      return (producer, consumer);
    }
    catch (Exception exception)
    {
      LogCreateKafkaClientsFailed(logger, exception);
      return exception.Message;
    }
  }
}
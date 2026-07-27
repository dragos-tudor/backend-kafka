namespace Kafka.Clients;

partial class ClientsFuncs
{
  public static Task<DeliveryResult<TKey, TValue>> PublishMessageAsync<TKey, TValue>(
    IProducer<TKey, TValue> producer,
    string topicName,
    Message<TKey, TValue> message,
    CancellationToken cancellationToken = default)
  => producer.ProduceAsync(topicName, message, cancellationToken);

  public static Message<TKey, TValue> PublishMessage<TKey, TValue>(
    IProducer<TKey, TValue> producer,
    string topicName,
    Message<TKey, TValue> message,
    Action<DeliveryReport<TKey, TValue>>? deliveryHandler = default)
  {
    producer.Produce(topicName, message, deliveryHandler);
    return message;
  }

  public static IEnumerable<Message<TKey, TValue>> PublishMessages<TKey, TValue>(
    IProducer<TKey, TValue> producer,
    string topicName,
    IEnumerable<Message<TKey, TValue>> messages,
    Action<DeliveryReport<TKey, TValue>>? deliveryHandler = default)
  {
    foreach (var message in messages)
      PublishMessage(producer, topicName, message, deliveryHandler);
    return messages;
  }
}
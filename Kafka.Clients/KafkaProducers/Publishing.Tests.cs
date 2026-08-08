
namespace Kafka.Clients;

public sealed partial class ClientsTests
{
  record TestMessage(int Id, string Value);

  [TestMethod]
  public async Task producer__publish_message_async__message_persisted()
  {
    using var producer = CreateKafkaProducer<string, byte[]>(options);
    var payload = new TestMessage(1, "test");
    var message = CreateKafkaMessage("key1", SerializeJson(payload), []);
    var result = await PublishMessageAsync(producer, publishTopicName, message, cancellationToken);

    result.Status.ShouldBe(PersistenceStatus.Persisted);
  }

  [TestMethod]
  public async Task producer__publish_message_async__message_with_key_and_value()
  {
    using var producer = CreateKafkaProducer<string, byte[]>(options);
    var payload = new TestMessage(1, "test");
    var message = CreateKafkaMessage("key2", SerializeJson(payload), []);
    var result = await PublishMessageAsync(producer, publishTopicName, message, cancellationToken);

    result.Message.Key.ShouldBe("key2");
    DeserializeJson<TestMessage>(result.Message.Value).ShouldBe(payload);
  }

  [TestMethod]
  public async Task producer__publish_message_sync__message_persisted()
  {
    using var producer = CreateKafkaProducer<string, byte[]>(options);
    var payload = new TestMessage(1, "test");
    var message = CreateKafkaMessage("key3", SerializeJson(payload), []);
    var tcs = new TaskCompletionSource<DeliveryResult<string, byte[]>>();

    PublishMessage(producer, publishTopicName, message, report =>
    {
      try { tcs.SetResult(report); }
      catch (Exception ex) { tcs.SetException(ex); }
    });

    producer.Flush(cancellationToken);
    var result = await tcs.Task;
    result.Status.ShouldBe(PersistenceStatus.Persisted);
  }

  [TestMethod]
  public async Task producer__publish_message_sync__message_with_key_and_value()
  {
    using var producer = CreateKafkaProducer<string, byte[]>(options);
    var tcs = new TaskCompletionSource<DeliveryResult<string, byte[]>>();
    var payload = new TestMessage(1, "test");

    var message = CreateKafkaMessage("key4", SerializeJson(payload), []);
    PublishMessage(producer, publishTopicName, message, report =>
    {
      try { tcs.SetResult(report); }
      catch (Exception ex) { tcs.SetException(ex); }
    });

    producer.Flush(cancellationToken);
    var result = await tcs.Task;
    result.Message.Key.ShouldBe("key4");
    DeserializeJson<TestMessage>(result.Message.Value).ShouldBe(payload);
  }

  [TestMethod]
  public async Task producer__publish_messages_sync__messages_persisted()
  {
    using var producer = CreateKafkaProducer<string, byte[]>(options);
    Message<string, byte[]>[] messages = [
      CreateKafkaMessage("key5", SerializeJson(new TestMessage(1, "test")), []),
      CreateKafkaMessage("key6", SerializeJson(new TestMessage(2, "test")), []),
    ];
    var tcs = new TaskCompletionSource<DeliveryResult<string, byte[]>>();
    var results = new List<DeliveryResult<string, byte[]>>();

    PublishMessages(producer, publishTopicName, messages, report =>
    {
      try { results.Add(report); if (results.Count == messages.Length) tcs.SetResult(report); }
      catch (Exception ex) { tcs.SetException(ex); }
    });

    producer.Flush(cancellationToken);
    await tcs.Task;
    results.All(r => r.Status == PersistenceStatus.Persisted).ShouldBeTrue();
  }
}
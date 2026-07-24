
namespace Kafka.Client;

public sealed partial class KafkaTests
{
  [TestMethod]
  public async Task topic__create_topic__topic_exists()
  {
    using var client = CreateKafkaAdminClient(options);
    var topicName = GetKafkaTopicName("create-topic");

    await CreateTopicAsync(client, topicName, options, cancellationToken);

    var exists = await WaitForTrueAsync(() => ExistsTopic(client, topicName, options), cancellationToken: cancellationToken);
    exists.ShouldBeTrue();

    await DeleteTopicAsync(client, topicName, options, cancellationToken);
  }
}
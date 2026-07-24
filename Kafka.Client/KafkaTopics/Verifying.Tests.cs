
namespace Kafka.Client;

public sealed partial class KafkaTests
{
  [TestMethod]
  public async Task topic__verify_topic_exists__returns_true_for_existing_topic()
  {
    using var client = CreateKafkaAdminClient(options);
    var topicName = GetKafkaTopicName("verify-topic");

    await CreateTopicAsync(client, topicName, options, cancellationToken);

    var exists = await WaitForTrueAsync(() => ExistsTopic(client, topicName, options), cancellationToken: cancellationToken);
    exists.ShouldBeTrue();

    await DeleteTopicAsync(client, topicName, options, cancellationToken);
  }
}
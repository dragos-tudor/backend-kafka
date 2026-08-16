
namespace Kafka.Messages;

public interface IKafkaDeadLetterTopicService<TKey, TValue> { string GetKafkaDeadLetterTopic(Message<TKey, TValue> message); }
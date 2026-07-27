
namespace Kafka.Messages;

public enum OutboxMessageStatus { Pending, Published, Retry, Failed }
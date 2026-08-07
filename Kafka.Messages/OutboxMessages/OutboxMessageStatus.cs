
namespace Kafka.Messages;

public enum OutboxMessageStatus { Pending, Published, DeadLettering, Retrying, Abandoned }
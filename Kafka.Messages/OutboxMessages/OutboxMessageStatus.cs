
namespace Kafka.Messages;

public enum OutboxMessageStatus { Pending, Published, DeadLettering, DeadLettered, Abandoned }
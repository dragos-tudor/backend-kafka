
namespace Kafka.Messages;

public enum InboxMessageStatus { Pending, DeadLettering, Handled, DeadLettered, Abandoned }
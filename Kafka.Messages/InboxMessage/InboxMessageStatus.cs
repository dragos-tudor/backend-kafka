
namespace Kafka.Messages;

public enum InboxMessageStatus { Pending, DeadLettering, Handled, Retry, Failed }

namespace Kafka.Operations;

public enum OperationState
{
  NotStartedState,

  NotCapturedKafkaMessageState,
  CapturedKafkaMessageState,

  InsertedInboxMessageState,
  IdempotentInboxMessageState,

  OffsetConsumerState,
  MissingInboxMessageState,

  HandledInboxMessageState,
  HandlingInboxMessageFailedState,

  PublishedDeadLetterState
}
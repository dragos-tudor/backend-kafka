
namespace Kafka.Engine;

public enum ConsumingState
{
  NotStartedState,

  NotCapturedKafkaMessageState,
  CapturedKafkaMessageState,

  InsertingInboxMessageState,
  InsertedInboxMessageState,

  ApplyingConsumerOffsetState,
  AppliedConsumerOffsetState,
  AlreadySavedInboxMessageState,

  HandledInboxMessageState,
  HandlingInboxMessageFailedState,

  PublishedDeadLetterState
}
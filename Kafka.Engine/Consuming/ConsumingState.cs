
namespace Kafka.Engine;

internal enum ConsumingState
{
  ConsumingMessageState,
  NotConsumedMessageState,
  CapturedKafkaMessageState,
  InsertingInboxMessageState,
  InsertedInboxMessageState,
  ApplyingConsumerOffsetState,
  AppliedConsumerOffsetState,
  AlreadySavedInboxMessageState,
  HandlingInboxMessageState,
  HandledInboxMessageState,
  PublishingDeadLetterState,
  PublishedDeadLetterState,
}
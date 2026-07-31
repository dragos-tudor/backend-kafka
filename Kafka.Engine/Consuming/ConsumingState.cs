
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

partial class EngineFuncs
{
  static ConsumingError ToConsumingError(ConsumingState state) =>
    state switch
    {
      ConsumingState.ConsumingMessageState =>  ConsumingError.CriticalError,
      ConsumingState.CapturedKafkaMessageState =>  ConsumingError.CriticalError,
      ConsumingState.InsertingInboxMessageState =>  ConsumingError.CriticalError,
      ConsumingState.InsertedInboxMessageState =>  ConsumingError.CriticalError,
      ConsumingState.ApplyingConsumerOffsetState =>  ConsumingError.CriticalError,
      _ => ConsumingError.None
    };
}
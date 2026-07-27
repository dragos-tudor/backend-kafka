
namespace Kafka;

internal enum ConsumeKafkaMessageError
{
  InvalidConsumerMessage,
  ConsumeKafkaMessageFailed,
  SaveInboxMessageFailed,
  InboxMessageAlreadySaved,
  ApplyOffsetFailed,
  MessageDeadLettered,
  HandleInboxMessageFailed,
  OperationCanceled
}
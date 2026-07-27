
namespace Kafka;

public enum ProcessKafkaMessagesError
{
  None,
  CriticalFailure,

}

partial class KafkaFuncs
{
  static ProcessKafkaMessagesError ToProcessKafkaMessagesError(ConsumeKafkaMessageError? error) =>
    error is null ? ProcessKafkaMessagesError.None :
    error switch
    {
      ConsumeKafkaMessageError.ApplyOffsetFailed =>  ProcessKafkaMessagesError.CriticalFailure,
      ConsumeKafkaMessageError.SaveInboxMessageFailed =>  ProcessKafkaMessagesError.CriticalFailure,
      ConsumeKafkaMessageError.ConsumeKafkaMessageFailed =>  ProcessKafkaMessagesError.CriticalFailure,
      ConsumeKafkaMessageError.InvalidConsumerMessage =>  ProcessKafkaMessagesError.None,
      ConsumeKafkaMessageError.OperationCanceled =>  ProcessKafkaMessagesError.None,
      ConsumeKafkaMessageError.InboxMessageAlreadySaved =>  ProcessKafkaMessagesError.None,
      ConsumeKafkaMessageError.MessageDeadLettered =>  ProcessKafkaMessagesError.None,
      ConsumeKafkaMessageError.HandleInboxMessageFailed =>  ProcessKafkaMessagesError.None,
      _ => throw new ArgumentOutOfRangeException(nameof(error), error, null)
    };
}
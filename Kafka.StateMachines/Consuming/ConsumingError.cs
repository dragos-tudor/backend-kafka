
namespace Kafka.StateMachines;

internal enum ConsumingError
{
  None,
  CriticalError
}

partial class StateMachinesFuncs
{
  static ConsumingError ToConsumingError(OperationState state) =>
    state switch
    {
      OperationState.NotStartedState =>  ConsumingError.CriticalError,
      OperationState.CapturedKafkaMessageState =>  ConsumingError.CriticalError,
      OperationState.InsertedInboxMessageState =>  ConsumingError.CriticalError,
      OperationState.MissingInboxMessageState =>  ConsumingError.CriticalError,
      _ => ConsumingError.None
    };
}
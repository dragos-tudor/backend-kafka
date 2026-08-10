
namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  static readonly HashSet<string> RelayingCriticalStates = [
    PublishingStates.PublishOutboxMessageCriticalErrorState,
    Operations.Outbox.DispatchingStates.DispatchDeadLetterCriticalErrorState
  ];
}
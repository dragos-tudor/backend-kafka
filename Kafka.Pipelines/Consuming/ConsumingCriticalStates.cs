
namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  static readonly HashSet<string> ConsumingCriticalStates =
  [
    CapturingStates.CaptureKafkaMessageCriticalErrorState,
    OffsettingStates.OffsetConsumeCriticalErrorState,
    Operations.Inbox.DispatchingStates.DispatchDeadLetterCriticalErrorState
  ];
}
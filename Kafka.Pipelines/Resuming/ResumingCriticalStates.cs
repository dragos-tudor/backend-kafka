
namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  static readonly HashSet<string> ResumingCriticalStates = [
    Operations.Inbox.DispatchingStates.DispatchDeadLetterCriticalErrorState
  ];
}
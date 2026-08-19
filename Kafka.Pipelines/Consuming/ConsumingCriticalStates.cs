using static Kafka.Operations.Inbox.CapturingStates;
using static Kafka.Operations.Inbox.RedirectingStates;
using static Kafka.Operations.Inbox.OffsettingStates;
using static Kafka.Operations.Inbox.InsertingStates;

namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  static readonly HashSet<string> ConsumingCriticalStates = [
    CaptureKafkaMessageCriticalErrorState,
    RedirectKafkaMessageCriticalErrorState,
    OffsetConsumeCriticalErrorState,
    RedirectKafkaMessageCircuitOpenState,
    InsertInboxMessageCircuitOpenState
  ];
}
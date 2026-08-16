
namespace Kafka.Operations.Inbox;

static class OffsettingStates
{
  internal const string OffsetConsumedState = "OffsetConsumedState";
  internal const string OffsetConsumeErrorState = "OffsetConsumeErrorState";
  internal const string OffsetConsumeCriticalErrorState = "OffsetConsumeCriticalErrorState";
}
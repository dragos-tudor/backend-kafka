
namespace Kafka.Operations.Inbox;

static class CapturingStates
{
  internal const string NotCapturedKafkaMessageState = "NotCapturedKafkaMessageState";
  internal const string CapturedKafkaMessageState = "CapturedKafkaMessageState";
  internal const string CaptureKafkaMessageErrorState = "CaptureKafkaMessageErrorState";
  internal const string CaptureKafkaMessageCriticalErrorState = "CaptureKafkaMessageCriticalErrorState";
}
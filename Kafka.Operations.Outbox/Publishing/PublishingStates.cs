
namespace Kafka.Operations.Outbox;

static class PublishingStates
{
  internal const string PublishedOutboxMessageState = "PublishedOutboxMessageState";
  internal const string PublishOutboxMessageErrorState = "PublishOutboxMessageErrorState";
  internal const string PublishOutboxMessageCriticalErrorState = "PublishOutboxMessageCriticalErrorState";
}
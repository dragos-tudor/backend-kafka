
namespace Kafka.Operations.Outbox;

static class PublishingStates
{
  internal static string PublishedOutboxMessageState = "PublishedOutboxMessageState";
  internal static string PublishOutboxMessageErrorState = "PublishOutboxMessageErrorState";
}

namespace Kafka.Operations.Outbox;

static class InsertingStates
{
  internal const string InsertedOutboxMessageState = "InsertedOutboxMessageState";
  internal const string InsertOutboxMessageErrorState = "InsertOutboxMessageErrorState";
  internal const string IdempotentOutboxMessageState = "IdempotentOutboxMessageState";
}
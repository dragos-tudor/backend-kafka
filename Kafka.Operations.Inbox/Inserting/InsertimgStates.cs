
namespace Kafka.Operations.Inbox;

static class InsertingStates
{
  internal const string InsertedInboxMessageState = "InsertedInboxMessageState";
  internal const string InsertInboxMessageErrorState = "InsertInboxMessageErrorState";
  internal const string InsertInboxMessageCircuitOpenState = "InsertInboxMessageCircuitOpenState";
  internal const string IdempotentInboxMessageState = "IdempotentInboxMessageState";
}
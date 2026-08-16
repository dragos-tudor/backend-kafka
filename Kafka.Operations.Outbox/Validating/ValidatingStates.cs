
namespace Kafka.Operations.Outbox;

static class ValidatingStates
{
  internal const string ValidatedOutboxMessageState = "ValidatedOutboxMessageState";
  internal const string ValidateOutboxMessageErrorState = "ValidateOutboxMessageErrorState";
  internal const string ValidateOutboxMessageDataErrorState = "ValidateOutboxMessageDataErrorState";
  internal const string ValidateOutboxMessagePayloadErrorState = "ValidateOutboxMessagePayloadErrorState";
}
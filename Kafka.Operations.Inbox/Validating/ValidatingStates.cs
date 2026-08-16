
namespace Kafka.Operations.Inbox;

static class ValidatingStates
{
  internal const string ValidatedInboxMessageState = "ValidatedInboxMessageState";
  internal const string ValidateInboxMessageErrorState = "ValidateInboxMessageErrorState";
  internal const string ValidateInboxMessageDataErrorState = "ValidateInboxMessageDataErrorState";
  internal const string ValidateInboxMessagePayloadErrorState = "ValidateInboxMessagePayloadErrorState";
}
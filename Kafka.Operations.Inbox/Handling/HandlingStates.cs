
namespace Kafka.Operations.Inbox;

static class HandlingStates
{
  internal const string HandledInboxMessageState = "HandledInboxMessageState";
  internal const string HandleMissingInboxMessageState = "HandleMissingInboxMessageState";
  internal const string HandleMissingInboxMessagePayloadState = "HandleMissingInboxMessagePayloadState";
  internal const string HandleInboxMessageDomainErrorState = "HandleInboxMessageDomainErrorState";
  internal const string HandleInboxMessageTechnicalErrorState = "HandleInboxMessageTechnicalErrorState";
}

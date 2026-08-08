
namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  internal const string HandledInboxMessageState = "HandledInboxMessageState";
  internal const string HandleInboxMessageDomainErrorState = "HandleInboxMessageDomainErrorState";
  internal const string HandleInboxMessageTechnicalErrorState = "HandleInboxMessageTechnicalErrorState";

  static string GetHandleInboxMessageState<T, TError>(T? model, TError? error) =>
    model is not null?
      HandledInboxMessageState:
      HandleInboxMessageDomainErrorState;
}

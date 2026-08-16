using static Kafka.Operations.Inbox.HandlingCounters;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  [LoggerMessage(10, LogLevel.Information, "Handled inbox message. MessageId: {messageId}. Status: {status}")]
  static partial void LogHandledInboxMessage(ILogger logger, Guid? messageId, InboxMessageStatus status);

  [LoggerMessage(11, LogLevel.Error, "Handle inbox message domain error. MessageId: {messageId}. Domain error: {domainError}")]
  static partial void LogHandleInboxMessageDomainError(ILogger logger, Guid? messageId, string domainError);

  [LoggerMessage(12, LogLevel.Error, "Handle inbox message technical error. MessageId: {messageId}.")]
  static partial void LogHandleInboxMessageTechnicalError(ILogger logger, Guid? messageId, Exception exception);


  static Activity? InstrumentHandledInboxMessage(
    Guid? messageId,
    InboxMessageStatus status,
    IInstrumentationServices services)
  {
    LogHandledInboxMessage(services.GetLogger(), messageId, status);
    AddMetricCounter(HandledCounter);
    AddActivityEvent(Activity.Current, "message.handled");
    return Activity.Current;
  }

  static Activity? InstrumentHandleInboxMessageDomainError(
    Guid? messageId,
    string domainError,
    IInstrumentationServices services)
  {
    LogHandleInboxMessageDomainError(services.GetLogger(), messageId, domainError);
    AddActivityTag(Activity.Current, "handle.message.domain.error", domainError);
    AddActivityEvent(Activity.Current, "handle.message.error",
      [CreateActivityEventAttribute("domain.error", domainError)]);
    return Activity.Current;
  }

   static Activity? InstrumentHandleInboxMessageTechnicalError(
    Guid? messageId,
    Exception ex,
    IInstrumentationServices services)
  {
    LogHandleInboxMessageTechnicalError(services.GetLogger(), messageId, ex);
    AddMetricCounter(HandleTechnicalErrorCounter);
    AddActivityTag(Activity.Current, "handle.message.technical.error", ex);
    AddActivityEvent(Activity.Current, "handle.message.error",
      [CreateActivityEventAttribute("technical.error", ex)]);
    return Activity.Current;
  }
}
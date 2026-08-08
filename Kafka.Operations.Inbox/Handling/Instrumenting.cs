using static Kafka.Operations.Inbox.HandlingCounterType;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  [LoggerMessage(10, LogLevel.Information, "Handled inbox message. MessageId: {messageId}")]
  static partial void LogHandledInboxMessage(ILogger logger, Guid? messageId);

  [LoggerMessage(11, LogLevel.Error, "Handle inbox message domain error. MessageId: {messageId}. Domain error: {domainError}")]
  static partial void LogHandleInboxMessageDomainError(ILogger logger, Guid? messageId, string domainError);

  [LoggerMessage(12, LogLevel.Error, "Handle inbox message technical error. MessageId: {messageId}. Technical error: {technicalError}")]
  static partial void LogHandleInboxMessageTechnicalError(ILogger logger, Guid? messageId, string technicalError);


  static Activity? InstrumentHandledInboxMessage(
    Guid? messageId,
    IInstrumentationServices services)
  {
    LogHandledInboxMessage(services.GetLogger(), messageId);
    AddMetricCounter(services.GetMetricCounters<HandlingCounterType>(), HandledCounter);
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
    string technicalError,
    IInstrumentationServices services)
  {
    LogHandleInboxMessageTechnicalError(services.GetLogger(), messageId, technicalError);
    AddMetricCounter(services.GetMetricCounters<HandlingCounterType>(), HandleTechnicalErrorCounter);
    AddActivityTag(Activity.Current, "handle.message.technical.error", technicalError);
    AddActivityEvent(Activity.Current, "handle.message.error",
      [CreateActivityEventAttribute("technical.error", technicalError)]);
    return Activity.Current;
  }
}
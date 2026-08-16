using static Kafka.Operations.Inbox.ValidatingStates;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  internal static ValueTask<(TData, string)> ValidateInboxMessage<TServices, TData, TKey, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IValidatingServices<TKey, TPayload>
  where TData : IValidatingData<TKey, TPayload>
  {
    try {
      var message = RequireInboxMessage(data.InboxMessage);
      var (valPayloadError, valPayloadException) = TryRun(message.Payload, services.ValidateInboxPayload);
      if (valPayloadError is not null) {
        data.InboxMessageError = valPayloadError;
        InstrumentValidateInboxMessagePayloadError(message.MessageId, valPayloadError, services);
        return new ((data, ValidateInboxMessagePayloadErrorState));
      }
      if (valPayloadException is not null) {
        data.InboxMessageError = valPayloadException.Message;
        InstrumentValidateInboxMessagePayloadError(message.MessageId, valPayloadException.Message, services);
        return new ((data, ValidateInboxMessagePayloadErrorState));
      }

      var isValidMessage = IsValidInboxMessage(message);
      if (isValidMessage is false) {
        var valErrors = GetInboxMessageValidationErrors(message);
        data.InboxMessageError = valErrors;
        InstrumentValidateInboxMessageDataError(message.MessageId, valErrors, services);
        return new ((data, ValidateInboxMessageDataErrorState));
      }

      InstrumentValidatedInboxMessage(message.MessageId, services);
      return new ((data, ValidatedInboxMessageState));
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception exception) {
      data.InboxMessageError = exception.Message;
      InstrumentValidateInboxMessageError(data.InboxMessage?.MessageId, exception, services);
      return new ((data, ValidateInboxMessageErrorState));
    }
  }
}
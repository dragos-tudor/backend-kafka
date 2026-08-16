using static Kafka.Operations.Outbox.ValidatingStates;

namespace Kafka.Operations.Outbox;

partial class OutboxFuncs
{
  internal static ValueTask<(TData, string)> ValidateOutboxMessage<TServices, TData, TKey, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IValidatingServices<TKey, TPayload>
  where TData : IValidatingData<TKey, TPayload>
  {
    try {
      var message = data.OutboxMessage;
      var (valPayloadError, valPayloadException) = TryRun(message.Payload, services.ValidateOutboxPayload);
      if (valPayloadError is not null) {
        InstrumentValidateOutboxMessagePayloadError(message.MessageId, valPayloadError, services);
        return new ((data, ValidateOutboxMessagePayloadErrorState));
      }
      if (valPayloadException is not null) {
        InstrumentValidateOutboxMessagePayloadError(message.MessageId, valPayloadException.Message, services);
        return new ((data, ValidateOutboxMessagePayloadErrorState));
      }

      var isValidMessage = IsValidOutboxMessage(message);
      if (isValidMessage is false) {
        var valErrors = GetOutboxMessageValidationErrors(message);
        InstrumentValidateOutboxMessageDataError(message.MessageId, valErrors, services);
        return new ((data, ValidateOutboxMessageDataErrorState));
      }

      InstrumentValidatedOutboxMessage(message.MessageId, services);
      return new ((data, ValidatedOutboxMessageState));
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception exception) {
      InstrumentValidateOutboxMessageError(data.OutboxMessage.MessageId, exception, services);
      return new ((data, ValidateOutboxMessageErrorState));
    }
  }
}
using static Kafka.GetConsumerKafkaMessageError;

namespace Kafka;

partial class KafkaFuncs
{
  internal static Result<ConsumeResult<TKey, TValue>?, GetConsumerKafkaMessageError?> GetConsumerKafkaMessage<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    ILogger logger,
    CancellationToken cancellationToken)
  {
    try
    {
      var result = ConsumeMessage(consumer, cancellationToken);
      if (!IsValidConsumerMessage(result)) return InvalidConsumerMessage;

      return result;
    }
    catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) { return OperationCanceled; }
    catch (Exception exception)
    {
      LogConsumeKafkaMessageFailed(logger, exception);
      return ConsumeKafkaMessageFailed;
    }
  }
}
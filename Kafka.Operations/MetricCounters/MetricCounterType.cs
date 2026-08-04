
namespace Kafka.Operations;

public enum MetricCounterType
{
  CapturedCounter,
  ConsumedCounter,
  DeadLetteredCounter,
  HandledCounter,
  InsertedCounter,
  IdempotentCounter,
  ConsumingErrorsCounter
}
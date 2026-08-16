
namespace Kafka.Operations.DeadLetter;

public interface ISchedulingServices<TKey, TPayload>:
  IRetryMessageOptionsService,
  IInstrumentationServices,
  IUpdateDeadLetterMessageService<TKey, TPayload>,
  IUtcDateService;

namespace Kafka.Engine;

partial class EngineFuncs
{
  static StepContext<IConsumeKafkaMessageServices<TKey, TValue, TPayload, TSession>, IConsumingStepData<TKey, TValue, TPayload>>
    CreateConsumingStepContext<TKey, TValue, TPayload, TSession>(
      IConsumeKafkaMessageServices<TKey, TValue, TPayload, TSession> services,
      ConsumingStepData<TKey, TValue, TPayload> data,
      Activity activity)
    where TSession : IDisposable =>
      new(services, data, activity);
}
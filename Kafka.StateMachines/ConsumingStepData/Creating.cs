
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  internal static IConsumingStepData<TKey, TValue, TPayload> CreateConsumingStepData<TKey, TValue, TPayload>() =>
    new ConsumingStepData<TKey, TValue, TPayload>();
}
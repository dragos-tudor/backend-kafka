
namespace Kafka.StateMachines;

static class ConsumingCounters
{
  internal static readonly Counter<long> ConsumedCounter = StateMachinesMeter!.CreateCounter<long>("consumed.kafka.messages");
  internal static readonly Counter<long> ConsumeCriticalErrorsCounter = StateMachinesMeter!.CreateCounter<long>("consume.kafka.messages.critical.errors");
}
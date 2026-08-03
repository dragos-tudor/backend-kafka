
namespace Kafka.Engine;

public readonly record struct StepContext<TServices, TData>(
  TServices Services,
  TData Data,
  Activity Activity
);

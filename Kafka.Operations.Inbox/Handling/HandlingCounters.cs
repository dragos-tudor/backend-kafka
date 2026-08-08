
using static Kafka.Operations.Inbox.HandlingCounterType;

namespace Kafka.Operations.Inbox;

public enum HandlingCounterType
{
  HandledCounter,
  HandleTechnicalErrorCounter
}

partial class InboxFuncs
{
  internal static IImmutableDictionary<HandlingCounterType, Counter<long>> CreateHandlingCounters(Meter meter) =>
    ImmutableDictionary<HandlingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<HandlingCounterType, Counter<long>>() {
        [HandledCounter] = meter.CreateCounter<long>("handled.inbox.messages"),
        [HandleTechnicalErrorCounter] = meter.CreateCounter<long>("handle.inbox.messages.technical.error"),
      });
}
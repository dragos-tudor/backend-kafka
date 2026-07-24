
namespace Kafka.Client;

public enum KafkaMessageOutcome { Succeeded, Retrying, DeadLettered }

public sealed record KafkaMessageProcessingResult(
  KafkaMessageOutcome Outcome,
  string? Reason = default,
  TimeSpan? NextAttemptDelay = default);

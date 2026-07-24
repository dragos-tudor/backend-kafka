
namespace Kafka.Client;

public abstract record Message
{
  public Guid MessageId { get; init; } = Guid.NewGuid();
  public required string Type { get; init; }
  public required byte[] Payload { get; init; }
  public DateTime Date { get; init; } = DateTime.UtcNow;
  public int Version { get; init; } = 1;
  public Guid CorrelationId { get; init; }
  public string? TraceParent { get; init; } // W3C Trace Context: "00-<trace-id>-<parent-id>-<flags>"
}

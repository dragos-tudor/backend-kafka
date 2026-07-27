namespace Kafka.Clients;

public record KafkaOptions : KafkaBaseOptions<string>
{
    public string ClientId { get; init; } = "storing-kafka-client";
    public string GroupId { get; init; } = "storing-kafka-group";
    public string DefaultTopic { get; init; } = string.Empty;
    public SecurityProtocol SecurityProtocol { get; init; } = SecurityProtocol.SaslPlaintext;
    public SaslMechanism SaslMechanism { get; init; } = SaslMechanism.ScramSha512;
    public AutoOffsetReset AutoOffsetReset { get; init; } = AutoOffsetReset.Earliest;
    public bool EnableAutoCommit { get; init; } = true;
    public bool EnableAutoOffsetStore { get; init; }
    public int DefaultNumPartitions { get; init; } = 12;
    public short DefaultReplicationFactor { get; init; } = 3;
    public string DeadLetterTopicSuffix { get; init; } = "-dlq";
    public IsolationLevel IsolationLevel { get; init; } = IsolationLevel.ReadCommitted;
    public int MaxPollRecords { get; init; } = 500;
    public TimeSpan SessionTimeout { get; init; } = TimeSpan.FromMilliseconds(30000);
    public new TimeSpan ConnectTimeout { get; init; } = TimeSpan.FromSeconds(15);
    public TimeSpan OperationTimeout { get; init; } = TimeSpan.FromSeconds(5);
}
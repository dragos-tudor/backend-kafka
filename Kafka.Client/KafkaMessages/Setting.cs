
namespace Kafka.Client;

partial class KafkaFuncs
{
  const string SchemaTypeHeaderName = "x-schema-type";
  const string SchemaVersionHeaderName = "x-schema-version";
  const string TraceIdHeaderName = "x-traceId";
  const string MessageIdHeaderName = "x-message-id";
  const string CorrelationIdHeaderName = "x-correlation-id";

  public static Headers SetKafkaSchemaTypeHeader(Headers headers, string schemaType) =>
    SetKafkaHeaderString(headers, SchemaTypeHeaderName, schemaType);

  public static Headers SetKafkaSchemaVersionHeader(Headers headers, int schemaVersion) =>
    SetKafkaHeaderString(headers, SchemaVersionHeaderName, schemaVersion.ToString(CultureInfo.InvariantCulture));

  public static Headers SetKafkaTraceIdHeader(Headers headers, string traceId) =>
    SetKafkaHeaderString(headers, TraceIdHeaderName, traceId);

  public static Headers SetKafkaMessageIdHeader(Headers headers, Guid messageId) =>
    SetKafkaHeaderString(headers, MessageIdHeaderName, messageId.ToString());

  public static Headers SetKafkaCorrelationIdHeader(Headers headers, Guid correlationId) =>
    SetKafkaHeaderString(headers, CorrelationIdHeaderName, correlationId.ToString());

  public static Headers SetKafkaMessageHeaders(
    Headers headers,
    string schemaType,
    int schemaVersion,
    string? traceId,
    Guid? messageId = default,
    Guid? correlationId = default)
  {
    var resolvedMessageId = messageId ?? Guid.NewGuid();
    var resolvedCorrelationId = correlationId ?? resolvedMessageId;
    return SetKafkaCorrelationIdHeader(
      SetKafkaMessageIdHeader(
        SetKafkaTraceIdHeader(
          SetKafkaSchemaVersionHeader(
            SetKafkaSchemaTypeHeader(headers, schemaType),
            schemaVersion),
          traceId ?? ""),
        resolvedMessageId),
      resolvedCorrelationId);
  }

  public static Headers SetKafkaMessageHeaders<TSchema>(
    Headers headers,
    int schemaVersion,
    string? traceId,
    Guid? messageId = default,
    Guid? correlationId = default) =>
    SetKafkaMessageHeaders(headers, GetKafkaSchemaType<TSchema>(), schemaVersion, traceId, messageId, correlationId);

}
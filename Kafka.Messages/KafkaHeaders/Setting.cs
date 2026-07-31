
namespace Kafka.Messages;

partial class MessagesFuncs
{
  internal static Headers SetKafkaHeaderString(Headers headers, string headerName, string? value) =>
    SetKafkaHeaderValue(headers, headerName, EncodeString(value));

  static Headers SetSchemaTypeKafkaHeader(Headers headers, string? schemaType) =>
    SetKafkaHeaderString(headers, SchemaTypeHeaderName, schemaType);

  static Headers SetSchemaVersionKafkaHeader(Headers headers, int? schemaVersion) =>
    SetKafkaHeaderString(headers, SchemaVersionHeaderName, schemaVersion?.ToString(CultureInfo.InvariantCulture));

  static Headers SetMessageIdKafkaHeader(Headers headers, Guid messageId) =>
    SetKafkaHeaderString(headers, MessageIdHeaderName, messageId.ToString());

  static Headers SetCorrelationIdKafkaHeader(Headers headers, Guid? correlationId) =>
    SetKafkaHeaderString(headers, CorrelationIdHeaderName, correlationId?.ToString());

  static Headers SetKafkaHeaderValue(Headers headers, string headerName, byte[]? value)
  {
    headers.Add(headerName, value);
    return headers;
  }
}
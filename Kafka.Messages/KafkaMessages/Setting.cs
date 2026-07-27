
namespace Kafka.Messages;

partial class MessagesFuncs
{
  internal static Headers SetKafkaMessageHeaders(
    Headers headers,
    Guid messageId,
    string? schemaType,
    int? schemaVersion,
    Guid? correlationId)
  =>
    SetCorrelationIdKafkaHeader(
      SetMessageIdKafkaHeader(
        SetSchemaVersionKafkaHeader(
          SetSchemaTypeKafkaHeader(headers, schemaType),
          schemaVersion),
        messageId),
      correlationId);
}
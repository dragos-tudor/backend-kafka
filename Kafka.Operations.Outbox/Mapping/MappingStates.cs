
namespace Kafka.Operations.Outbox;

static class MappingStates
{
  internal const string MappedOutboxMessageState = "MappedOutboxMessageState";
  internal const string MapOutboxMessageErrorState = "MapOutboxMessageErrorState";
  internal const string MapOutboxMessagePayloadErrorState = "MapOutboxMessagePayloadErrorState";
}
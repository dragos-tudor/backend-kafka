
namespace Kafka.Operations.Inbox;

static class MappingStates
{
  internal const string MappedKafkaMessageState = "MappedKafkaMessageState";
  internal const string MapKafkaMessageWithInboxErrorState = "MapKafkaMessageWithInboxErrorState";
  internal const string MapKafkaMessageWithoutInboxErrorState = "MapKafkaMessageWithoutInboxErrorState";
  internal const string MapKafkaMessageValueErrorState = "MapKafkaMessageValueErrorState";
}
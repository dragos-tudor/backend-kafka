
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  internal static async Task RelayOutboxMessagesAsync<TKey, TValue, TPaylod>(
    IRelayOutboxMessagesServices<TKey, TValue, TPaylod> services,
    CancellationToken token)
  {
    throw new NotImplementedException();
  }
}

namespace Kafka.Engine;

partial class EngineFuncs
{
  internal static async Task RelayOutboxMessagesAsync<TKey, TValue, TPaylod>(
    IRelayOutboxMessagesServices<TKey, TValue, TPaylod> services,
    CancellationToken token)
  {
    throw new NotImplementedException();
  }
}
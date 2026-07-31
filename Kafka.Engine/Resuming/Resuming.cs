
namespace Kafka.Engine;

partial class EngineFuncs
{
  internal static async Task ResumeInboxMessagesAsync<TKey, TValue, TPayload, TSession>(
    IResumeInboxMessagesServices<TKey, TValue, TPayload, TSession> services,
    CancellationToken cancellationToken = default)
  where TSession : IDisposable
  {
    throw new NotImplementedException();
  }
}
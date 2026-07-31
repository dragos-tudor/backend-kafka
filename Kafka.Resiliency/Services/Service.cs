
namespace Kafka.Resiliency;

public interface IDistributedLockService
{
  Task<IAsyncDisposable?> TryAcquireLockAsync(string key, TimeSpan lockDuration, CancellationToken cancellationToken);
}

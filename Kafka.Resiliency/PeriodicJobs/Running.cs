
namespace Kafka.Resiliency;

partial class ResiliencyFuncs
{
  internal static async Task RunPeriodicJobAsync(
    string jobName,
    TimeSpan timerInterval,
    TimeSpan lockInterval,
    Func<CancellationToken, Task> work,
    IRunPeriodicJobServices services,
    CancellationToken cancellationToken = default)
  {
    using var timer = new PeriodicTimer(timerInterval);
    while (await timer.WaitForNextTickAsync(cancellationToken))
    {
      try
      {
        await using var handle = await services.TryAcquireLockAsync(jobName, lockInterval, cancellationToken);
        if (handle is not null)
          await work(cancellationToken);
      }
      catch (Exception exception)
      {
        LogPeriodicJobError(services.GetLogger(), exception, jobName);
      }
    }
  }
}
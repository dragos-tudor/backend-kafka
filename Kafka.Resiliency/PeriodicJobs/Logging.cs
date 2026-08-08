
namespace Kafka.Resiliency;

partial class ResiliencyFuncs
{
  [LoggerMessage(50, LogLevel.Error, "Periodic job error. Job name {JobName}")]
  static partial void LogPeriodicJobError(ILogger logger, Exception exception, string jobName);
}
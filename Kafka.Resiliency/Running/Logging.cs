
namespace Kafka.Resiliency;

partial class ResiliencyFuncs
{
  [LoggerMessage(5, LogLevel.Error, "Creating Kafka clients failed.")]
  static partial void LogCreateKafkaClientsFailed(ILogger logger, Exception exception);

  [LoggerMessage(6, LogLevel.Error, "Periodic job failed. Job name {JobName}")]
  static partial void LogPeriodicJobFailed(ILogger logger, Exception exception, string jobName);
}
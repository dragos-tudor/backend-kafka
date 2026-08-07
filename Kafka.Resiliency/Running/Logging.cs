
namespace Kafka.Resiliency;

partial class ResiliencyFuncs
{
  [LoggerMessage(5, LogLevel.Error, "Creating Kafka clients error.")]
  static partial void LogCreateKafkaClientsError(ILogger logger, Exception exception);

  [LoggerMessage(6, LogLevel.Error, "Periodic job error. Job name {JobName}")]
  static partial void LogPeriodicJobError(ILogger logger, Exception exception, string jobName);
}
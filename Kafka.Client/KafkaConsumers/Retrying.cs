
namespace Kafka.Client;

partial class KafkaFuncs
{
  public static TimeSpan CalculateNextRetryDelay(int retryCount, KafkaOptions options)
  {
    var delay = options.RetryBaseDelay * Math.Pow(options.RetryBackoffFactor, retryCount);
    return delay > options.MaxRetryDelay ? options.MaxRetryDelay : delay;
  }
}

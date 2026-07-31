
namespace Kafka.Messages;

partial class MessagesFuncs
{
  internal static DateTime CalculateNextAttemptAt(int retryCount, DateTime date, PersistedRetryOptions options)
  {
    var retryDelay = CalculateNextRetryDelay(retryCount, options);
    return date.Add(retryDelay);
  }

  internal static TimeSpan CalculateNextRetryDelay(int retryCount, PersistedRetryOptions options)
  {
    var delay = options.RetryBaseDelay * Math.Pow(options.RetryBackoffFactor, retryCount);
    return delay > options.MaxRetryDelay ? options.MaxRetryDelay : delay;
  }
}

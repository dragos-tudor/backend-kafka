
namespace Kafka;

partial class KafkaFuncs
{
  static Task DelayTask(TimeSpan delay, CancellationToken cancellationToken)
    => Task.Delay(delay, cancellationToken);
}
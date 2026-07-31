
namespace Kafka.Utils;

partial class UtilsFuncs
{
  internal static Task DelayTask(TimeSpan delay, CancellationToken cancellationToken)
    => Task.Delay(delay, cancellationToken);
}
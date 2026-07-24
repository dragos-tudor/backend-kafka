
namespace Kafka.Client;

partial class KafkaFuncs
{
  // Variant B: single pass - caller owns scheduling (e.g. a PeriodicTimer in a hosted service).
  public static async Task ProcessDueMessagesAsync<TItem>(
    string ownerId,
    Func<string, CancellationToken, Task<IReadOnlyList<TItem>>> getDueItems,
    Func<TItem, CancellationToken, Task<KafkaMessageProcessingResult>> processItem,
    Func<TItem, KafkaMessageProcessingResult, CancellationToken, Task> recordOutcome,
    CancellationToken cancellationToken = default)
  {
    var items = await getDueItems(ownerId, cancellationToken);
    foreach (var item in items)
    {
      var result = await processItem(item, cancellationToken);
      await recordOutcome(item, result, cancellationToken);
    }
  }

  // Variant A: self-contained polling loop, built on top of the single-pass overload.
  public static async Task ProcessDueMessagesAsync<TItem>(
    string ownerId,
    Func<string, CancellationToken, Task<IReadOnlyList<TItem>>> getDueItems,
    Func<TItem, CancellationToken, Task<KafkaMessageProcessingResult>> processItem,
    Func<TItem, KafkaMessageProcessingResult, CancellationToken, Task> recordOutcome,
    TimeSpan pollInterval,
    CancellationToken cancellationToken = default)
  {
    while (!cancellationToken.IsCancellationRequested)
    {
      await ProcessDueMessagesAsync(ownerId, getDueItems, processItem, recordOutcome, cancellationToken);
      await Task.Delay(pollInterval, cancellationToken);
    }
  }
}


namespace Kafka;

partial class KafkaFuncs
{
  public static async Task ProcessDueMessagesAsync<TKey, TPayload>(
    IReadOnlyList<MessageBase<TKey, TPayload>> messages,
    Func<MessageBase<TKey, TPayload>, CancellationToken, Task<string?>> processMessage,
    CancellationToken cancellationToken = default)
  {
    foreach (var message in messages)
     await processMessage(message, cancellationToken);
  }

  public static async Task ProcessDueMessagesAsync<TKey, TPayload>(
    Func<CancellationToken, Task<IReadOnlyList<MessageBase<TKey, TPayload>>>> getDueMessages,
    Func<MessageBase<TKey, TPayload>, CancellationToken, Task<string?>> processMessage,
    TimeSpan pollInterval,
    CancellationToken cancellationToken = default)
  {
    while (!cancellationToken.IsCancellationRequested)
    {
      var messages = await getDueMessages(cancellationToken);
      await ProcessDueMessagesAsync(messages, processMessage, cancellationToken);
      await DelayTask(pollInterval, cancellationToken);
    }
  }
}

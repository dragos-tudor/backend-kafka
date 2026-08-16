
namespace Kafka.Operations.DeadLetter;

static class InsertingStates
{
  internal const string InsertedDeadLetterMessageState = "InsertedDeadLetterMessageState";
  internal const string InsertDeadLetterMessageErrorState = "InsertDeadLetterMessageErrorState";
  internal const string IdempotentDeadLetterMessageState = "IdempotentDeadLetterMessageState";
}
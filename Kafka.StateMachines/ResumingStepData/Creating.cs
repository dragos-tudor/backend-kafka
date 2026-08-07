
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  internal static IResumingStepData<TKey, TValue, TPayload> CreateResumingStepData<TKey, TValue, TPayload>(InboxMessage<TKey, TPayload> message) =>
    new ResumingStepData<TKey, TValue, TPayload>
    {
      InboxMessage = message
    };
}
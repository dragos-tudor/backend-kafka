using static Kafka.Engine.ConsumingState;

namespace Kafka.Engine;

partial class EngineFuncs
{
  internal static async ValueTask<ConsumingState> PublishDeadLetterStepAsync<TService, TData, TKey, TValue, TPayload>(
    StepContext<TService, TData> ctx,
    CancellationToken ct)
  where TService : IPublishDeadLetterServices<TKey, TValue, TPayload>
  where TData : IPublishDeadLetterData<TKey, TValue, TPayload>
  {
    var services = ctx.Services;
    var data = ctx.Data;

    var deadLetterTopic = services.GetDeadLetterTopic(data.Message!);
    var deadLetter = ToKafkaDeadLetter(data.Message!, data.Offset!, data.DomainError!, services.GetUtcDate(), services.ToKafkaMessageValue);

    InjectTraceParentActivity(ctx.Activity, data.KafkaMessage!.Headers);
    await PublishKafkaDeadLetterAsync(services.GetProducer(), deadLetter, deadLetterTopic, data.Message!, services, ct);

    InstrumentPublishDeadLetterStep(ctx.Activity, data.DomainError!, deadLetterTopic, services);
    return PublishedDeadLetterState;
  }
}
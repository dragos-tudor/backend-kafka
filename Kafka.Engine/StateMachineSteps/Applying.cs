using static Kafka.Engine.ConsumingState;

namespace Kafka.Engine;

partial class EngineFuncs
{
  internal static ValueTask<ConsumingState> ApplyConsumerOffsetStepAsync<TService, TData, TKey, TValue, TPayload>(
    StepContext<TService, TData> ctx,
    CancellationToken ct)
  where TService : IApplyConsumerOffsetServices<TKey, TValue>
  where TData : IApplyConsumerOffsetData
  {
    var services = ctx.Services;
    var data = ctx.Data;

    var appliedOffset = ApplyConsumerOffsetStrategy(services.GetConsumer(), data.Offset!, services.GetKafkaOptions());
    data.AppliedOffset = appliedOffset?.Offset;

    InstrumentApplyConsumerOffsetStep(ctx.Activity, appliedOffset, services);
    return new(AppliedConsumerOffsetState);
  }
}
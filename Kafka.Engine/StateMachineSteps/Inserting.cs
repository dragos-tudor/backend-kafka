using static Kafka.Engine.ConsumingState;

namespace Kafka.Engine;

partial class EngineFuncs
{
  internal static async ValueTask<ConsumingState> InsertInboxMessageStepAsync<TService, TData, TKey, TValue, TPayload>(
    StepContext<TService, TData> ctx,
    CancellationToken ct = default)
  where TService : IInsertInboxMessageServices<TKey, TValue, TPayload>
  where TData : IInsertInboxMessageData<TKey, TValue, TPayload>
  {
    var services = ctx.Services;
    var data = ctx.Data;

    var message = ToInboxMessage(data.KafkaMessage!, data.Offset!, services.ToPersistedMessagePayload, services.GetUtcDate());
    var saved = await ctx.Services.InsertInboxMessageAsync(message, ct);
    if (!saved) return AlreadySavedInboxMessageState;

    data.Message = message;
    InstrumentInsertInboxMessageStep(ctx.Activity, ctx.Services);
    return InsertedInboxMessageState;
  }
}
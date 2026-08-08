
namespace Kafka.Operations.Outbox;

public interface IScheduleOptionsService { RetryMessageOptions GetScheduleRetryOptions(); }

public interface IDelayOptionsService { RetryMessageOptions GetDelayRetryOptions(); }



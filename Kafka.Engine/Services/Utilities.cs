
namespace Kafka.Engine;

public interface IGetActivitySource { ActivitySource GetActivitySource(); }

public interface IGetMetricCounters { IImmutableDictionary<MetricCounterTypes, Counter<long>> GetMetricCounters(); }

public interface IGetLogger { ILogger GetLogger(); }

public interface IGetUtcDateService { DateTime GetUtcDate(); }
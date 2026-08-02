
namespace Kafka.Engine;

public interface IGetActivitySource { ActivitySource GetActivitySource(); }

public interface IGetMetricCounters { MetricCounters GetMetricCounters(); }

public interface IGetLogger { ILogger GetLogger(); }

public interface IGetUtcDateService { DateTime GetUtcDate(); }

namespace Kafka.Operations;

public interface IInstrumentationServices:
  IActivitySourceService,
  ILoggerService,
  IMetricCountersService;

public interface IActivitySourceService { ActivitySource GetActivitySource(); }

public interface IMetricCountersService { MetricCounters GetMetricCounters(); }

public interface ILoggerService { ILogger GetLogger(); }
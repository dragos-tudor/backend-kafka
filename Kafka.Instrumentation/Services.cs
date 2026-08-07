
namespace Kafka.Instrumentation;

public interface IInstrumentationServices:
  IActivitySourceService,
  ILoggerService,
  IMetricCountersService;

public interface IActivitySourceService { ActivitySource GetActivitySource(); }

public interface IMetricCountersService { IImmutableDictionary<TCounterType, Counter<long>> GetMetricCounters<TCounterType>(); }

public interface ILoggerService { ILogger GetLogger(); }

namespace Kafka.Instrumentation;

public interface IInstrumentationServices:
  IActivitySourceService,
  ILoggerService;

public interface IActivitySourceService { ActivitySource GetActivitySource(); }

public interface ILoggerService { ILogger GetLogger(); }
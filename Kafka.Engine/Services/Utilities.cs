
namespace Kafka.Engine;

public interface IGetActivitySourceService { ActivitySource GetActivitySource(); }

public interface IGetConsumerService<TKey, TValue> { IConsumer<TKey, TValue> GetConsumer(); }

public interface IGetMetricCountersService { MetricCounters GetMetricCounters(); }

public interface IGetLoggerService { ILogger GetLogger(); }

public interface IGetKafkaOptionsService { KafkaOptions GetKafkaOptions(); }

public interface IGetProducerService<TKey, TValue> { IProducer<TKey, TValue> GetProducer(); }

public interface IGetUtcDateService { DateTime GetUtcDate(); }
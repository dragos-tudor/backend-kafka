
namespace Kafka.Pipelines;

public interface IRelayBatchSizeService { int GetRelayBatchSize(); }

public interface IResumeBatchSizeService { int GetResumeBatchSize(); }

public interface IDeadLetteringBatchSizeService { int GetDeadLetteringBatchSize(); }

namespace Kafka.Pipelines;

public interface IRelayBatchSizeService { int GetRelayBatchSize(); }

public interface IResumeBatchSizeService { int GetResumeBatchSize(); }
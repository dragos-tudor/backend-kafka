
namespace Kafka.StateMachines;

public interface IRelayBatchSizeService { int GetRelayBatchSize(); }

public interface IResumeBatchSizeService { int GetResumeBatchSize(); }
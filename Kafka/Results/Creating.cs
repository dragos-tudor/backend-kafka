
namespace Kafka;

partial class KafkaFuncs
{
  internal static Result<T, TFailure> CreateResult<T, TFailure>(T? success, TFailure? failure = default) => new(success, failure);
}
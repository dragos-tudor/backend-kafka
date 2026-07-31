
namespace Kafka.Utils;

partial class UtilsFuncs
{
  internal static Result<T, TError> CreateResult<T, TError>(T? success, TError? error = default) => new(success, error);
}
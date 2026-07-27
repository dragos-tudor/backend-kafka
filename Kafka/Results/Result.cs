#pragma warning disable CA2225

namespace Kafka;

public record Result<T, TFailure>(T? Success, TFailure? Failure = default)
{
  public static implicit operator Result<T, TFailure>(T success) => CreateResult<T, TFailure>(success);

  public static implicit operator Result<T, TFailure>(TFailure failure) => CreateResult<T, TFailure>(default, failure);
}
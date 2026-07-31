#pragma warning disable CA2225

namespace Kafka.Utils;

public record Result<T, TError>(T? Success, TError? Error = default)
{
  public static implicit operator Result<T, TError>(T success) => new Result<T, TError>(success);

  public static implicit operator Result<T, TError>(TError error) => new Result<T, TError>(default, error);
}
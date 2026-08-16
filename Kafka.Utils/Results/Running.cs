
namespace Kafka.Utils;

partial class UtilsFuncs
{
  internal static (TOut?, Exception?) TryRun<TIn, TOut>(TIn value, Func<TIn, TOut> func)
  {
    try { return (func(value), null); }
    catch (Exception ex) { return (default, ex); }
  }
}
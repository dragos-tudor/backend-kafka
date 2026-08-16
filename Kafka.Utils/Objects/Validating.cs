
namespace Kafka.Utils;

partial class UtilsFuncs
{
  static readonly ValidationContext ValidationContext = new ValidationContext(new object());

  internal static string? ValidateObject<T>(T obj) where T: class
  {
    try {
      Validator.ValidateObject(obj, ValidationContext, validateAllProperties: true);
      return null;
    }
    catch (ValidationException ex) {
      return ex.ValidationResult?.ErrorMessage;
    }
  }
}
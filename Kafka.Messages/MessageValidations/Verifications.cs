
using static Kafka.Messages.MessageFieldConstraints;

namespace Kafka.Messages;

partial class MessagesFuncs
{
  static bool IsValidMessageId (Guid messageId) => messageId != Guid.Empty;

  static bool IsValidMessageKey<TKey> (TKey messageKey) => messageKey switch
  {
    null => false,
    Guid gid => gid != Guid.Empty,
    int iid => iid != 0,
    long lid => lid != 0,
    string sid => !string.IsNullOrWhiteSpace(sid),
    _ => true
  };

  static bool IsValidMessageType (string? messageType) => (messageType?.Length ?? 0) <= TypeMaxLength;

  static bool IsValidMessageMetadata (string? messageMetadata) => (messageMetadata?.Length ?? 0) <= MetadataMaxLength;

  static bool IsValidMessageLastError (string? messageLastError) => (messageLastError?.Length ?? 0) <= LastErrorMaxLength;
}

// namespace Kafka.Clients;

// partial class KafkaFuncs
// {
//   public static async Task ResumeInboxMessagesAsync<TKey, TValue, TPayload>(
//     IProducer<TKey, TValue> producer,
//     IResumingInboxServices<TKey, TValue, TPayload> services,
//     CancellationToken cancellationToken = default)
//   {
//     var logger = services.GetLogger(nameof(IResumingInboxServices<,,>));

//     // ASSUMPTION: a method exists that returns inbox messages in Pending or
//     // DeadLettering status (i.e. anything not Handled and not Failed).
//     // If this doesn't exist yet on IConsumingServices, it needs adding.
//     var resumableMessages = await services.GetDueInboxMessages(cancellationToken);

//     foreach (var inboxMessage in resumableMessages)
//     {
//       if (cancellationToken.IsCancellationRequested) break;

//       try
//       {
//         switch (inboxMessage.Status)
//         {
//           case InboxMessageStatus.Pending:
//             await ResumePendingInboxMessageAsync(producer, services, inboxMessage, cancellationToken);
//             break;

//           case InboxMessageStatus.DeadLettering:
//             await ResumeDeadLetteringInboxMessageAsync(producer, services, inboxMessage, cancellationToken);
//             break;

//           default:
//             // defensive: the query above shouldn't return anything else,
//             // but if it does, don't silently reprocess a Handled/Failed message
//             LogUnexpectedResumableStatus(logger, inboxMessage.MessageId, inboxMessage.Status);
//             break;
//         }
//       }
//       catch (Exception exception)
//       {
//         // leave the status exactly where it is; next resumer run will pick it up again
//         LogResumeInboxMessageFailed(logger, exception.Message, inboxMessage.MessageId, inboxMessage.Status);
//       }
//     }
//   }

//   static async Task ResumePendingInboxMessageAsync<TKey, TValue, TPayload>(
//       IProducer<TKey, TValue> producer,
//       IResumingInboxServices<TKey, TValue, TPayload> services,
//       InboxMessage<TKey, TPayload> inboxMessage,
//       CancellationToken cancellationToken)
//   {
//     // Pending = never finished HandleInboxMessage (crashed mid-way, or the
//     // consumer saved it and died before HandleInboxMessage ran at all).
//     // Full reprocessing from scratch is correct here.
//     var failure = await services.HandleInboxMessage(inboxMessage, cancellationToken);

//     if (failure is not null)
//     {
//       var deadLetterMessage = ToKafkaDeadLetter(inboxMessage, failure, services.GetUtcDate(), services.ToMessageValue);
//       var deadLetterTopic = services.GetDeadLetterTopic(inboxMessage.Topic);

//       await services.UpdateInboxMessageStatus(inboxMessage, InboxMessageStatus.DeadLettering, cancellationToken);
//       await PublishMessageAsync(producer, deadLetterTopic, deadLetterMessage, cancellationToken);
//       await services.UpdateInboxMessageStatus(inboxMessage, InboxMessageStatus.Failed, cancellationToken);
//       return;
//     }

//     await services.UpdateInboxMessageStatus(inboxMessage, InboxMessageStatus.Handled, cancellationToken);
//   }

//   private static async Task ResumeDeadLetteringInboxMessageAsync<TKey, TValue, TPayload>(
//       IProducer<TKey, TValue> producer,
//       IResumingInboxServices<TKey, TValue, TPayload> services,
//       InboxMessage<TKey, TPayload> inboxMessage,
//       CancellationToken cancellationToken)
//   {
//     // DeadLettering = the failure decision was ALREADY made by HandleInboxMessage.
//     // Do not call HandleInboxMessage again here — only resume the publish step.
//     // ASSUMPTION: the original failure reason was persisted on the inbox message
//     // (or is otherwise retrievable) so it can be included on the dead letter again.
//     var failure = inboxMessage.LastFailureReason
//         ?? throw new InvalidOperationException(
//             $"Inbox message {inboxMessage.MessageId} is in DeadLettering status but has no stored failure reason.");

//     var deadLetterMessage = ToKafkaDeadLetter(inboxMessage, failure, services.GetUtcDate(), services.ToMessageValue);
//     var deadLetterTopic = services.GetDeadLetterTopic(inboxMessage.Topic);

//     await PublishMessageAsync(producer, deadLetterTopic, deadLetterMessage, cancellationToken);
//     await services.UpdateInboxMessageStatus(inboxMessage, InboxMessageStatus.Failed, cancellationToken);
//   }
// }

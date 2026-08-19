# Operations — States reference

## Inbox
- **Capturing**
  States: `NotCapturedKafkaMessageState`, `CapturedKafkaMessageState`, `CaptureKafkaMessageErrorState`, `CaptureKafkaMessageCriticalErrorState`

- **Mapping**
  States: `MappedKafkaMessageState`, `MapKafkaMessageErrorState`, `MapKafkaMessageValueErrorState`

- **Inserting**
  States: `InsertedInboxMessageState`, `InsertInboxMessageErrorState`, `IdempotentInboxMessageState`

- **Offsetting**
  States: `OffsetConsumedState`, `OffsetConsumeErrorState`,
  `OffsetMissingInboxMessagePayloadState`, `OffsetMissingInboxMessageState`,
  `OffsetConsumeCriticalErrorState`

- **Validating**
  States: `ValidatedInboxMessageState`, `ValidateInboxMessageErrorState`, `ValidateInboxMessageDataErrorState`, `ValidateInboxMessagePayloadErrorState`

- **Handling**
  States: `HandledInboxMessageState`, `HandleMissingInboxMessageState`,`HandleInboxMessageTechnicalErrorState`

- **Redirecting**
  States: `RedirectedKafkaMessageState`, `RedirectKafkaMessageAmbiguousState`, `RedirectKafkaMessageDeliveryErrorState`, `RedirectKafkaMessageErrorState`, `RedirectKafkaMessageCriticalErrorState`

- **Scheduling**
  States: `ScheduleInboxMessageExhaustedState`, `ScheduleInboxMessageRetryState`, `ScheduleInboxMessageErrorState`

## Outbox
- **Mapping**
  States: `MappedOutboxMessageState`, `MapOutboxMessageErrorState`, `MapOutboxMessagePayloadErrorState`

- **Inserting**
  States: `InsertedOutboxMessageState`, `InsertOutboxMessageErrorState`, `IdempotentOutboxMessageState`

- **Producing**
  States: `ProducedCallbackState`, `ProduceCallbackDeliveryErrorState`, `ProduceCallbackErrorState`, `ProducingKafkaMessageState`, `ProduceKafkaMessageErrorState`, `ProduceKafkaMessageCriticalErrorState`

- **Validating**
  States: `ValidatedOutboxMessageState`, `ValidateOutboxMessageErrorState`, `ValidateOutboxMessageDataErrorState`, `ValidateOutboxMessagePayloadErrorState`

- **Scheduling**
  States: `ScheduleOutboxMessageExhaustedState`, `ScheduleOutboxMessageRetryState`, `ScheduleOutboxMessageErrorState`

## DeadLetter
- **Mapping**
  States: `MappedDeadLetterMessageState`, `MapDeadLetterMessageErrorState`, `MapDeadLetterMessagePayloadErrorState`

- **Inserting**
  States: `InsertedDeadLetterMessageState`, `InsertDeadLetterMessageErrorState`, `IdempotentDeadLetterMessageState`

- **Producing**
  States: `ProducedDeadLetterCallbackState`, `ProduceDeadLetterCallbackDeliveryErrorState`, `ProduceDeadLetterCallbackErrorState`, `ProducingKafkaDeadLetterState`, `ProduceKafkaDeadLetterErrorState`, `ProduceKafkaDeadLetterCriticalErrorState`

- **Converting**
  States: `ConvertedDeadLetterMessageState`, `ConvertDeadLetterMessageErrorState`

- **Scheduling**
  States: `ScheduleDeadLetterMessageExhaustedState`, `ScheduleDeadLetterMessageRetryState`, `ScheduleDeadLetterMessageErrorState`
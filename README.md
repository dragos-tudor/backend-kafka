## Backend kafka library.
- convenient functions for Kafka distributed event streaming platform.
- functional-style library [OOP-free].
- podman-inside-of-podman.

### Consumming Kafka Messages
---

Consumes a single Kafka message and drives it through the transactional inbox pattern: durably
capture the message, commit the Kafka offset, then attempt business processing.

```
consume → save to inbox → commit offset → handle message
```

**Why this order matters**

The function deliberately separates two concerns:

- **Did we durably capture the message?** — Kafka's job, resolved by `SaveInboxMessageAsync`.
- **Did we finish business processing?** — the database's job, tracked via `InboxMessageStatus`
  (`Pending` → `DeadLettering`/`Handled` → `Failed`).

The Kafka offset is committed only *after* the inbox row is saved, and it commits
**unconditionally** — regardless of whether this particular call was the one that inserted the
row, or whether business processing later succeeds, fails, or gets dead-lettered. Offset
durability is not contingent on anything downstream of the save.

**Step-by-step behavior**

1. **`GetConsumerKafkaMessage`** — pulls the next record off the consumer. Invalid or EOF
   results are filtered out; genuine consume-time failures (e.g. broker/coordinator issues)
   are reported distinctly from a caller-requested cancellation.
2. **`SaveInboxMessageAsync`** — persists the message to the inbox table. Returns one of:
   newly saved, already saved (idempotent — e.g. after a restart or resumer race), or a save
   failure.
3. **`ApplyConsumerOffset`** — commits the offset. This step always runs and is always checked
   first among the two subsequent branches, because a failed offset commit is restart-worthy
   no matter what happened at the save step.
4. **`HandleInboxMessageAsync`** — runs business processing, *only* if the message was newly
   saved. If the message already existed (already-saved case), processing is skipped entirely
   to avoid double-handling on replay. On business failure, the message is dead-lettered
   (`DeadLettering` status is written *before* the publish, so a mid-crash leaves an
   unambiguous trail) and marked `Failed`; on success it's marked `Handled`.

**Error handling**

Each step returns its own narrow, function-specific error enum (`GetConsumerKafkaMessageError`,
`SaveInboxMessageError`, `ApplyConsumerOffsetError`, `HandleInboxMessageError`) rather than a
single shared type — so the compiler prevents a function from returning an error case that
can't actually happen there (e.g. `HandleInboxMessageAsync` can't accidentally report a save
failure). Each narrow enum is mapped to the shared `ConsumeKafkaMessageError` via an exhaustive
switch with a throwing default, so adding a new case anywhere forces an explicit mapping
decision instead of silently falling through.

Cancellation (`OperationCanceledException` from a cancelled token) is handled as its own
distinct outcome, separate from genuine failures, in every step that supports it.

**Outcome for the caller**

`ConsumeKafkaMessageAsync` returns `ConsumeKafkaMessageError?` — `null` on success (handled or
correctly skipped as already-saved), or a specific error otherwise. The caller
(`ProcessKafkaMessagesAsync`) maps this to a simple `CriticalFailure`/`None` decision: get consumer message, applying offset
or save inbox message failures are treated as critical and trigger a full client teardown, backoff delay,
and restart (which also triggers a Kafka consumer group rebalance); everything else
(cancellation, dead-lettering, already-saved, ordinary business failure) is non-critical and
the consume loop simply continues.

**Consuming messages avoided race conditions**
 - durable-save-before-offset-commit (to avoid move offset for unsaved messages).
 - restart/resumer overlap (via the saved check).
 - fresh-message/resumer-message overlap (via the threshold).
 - resumer-vs-resumer overlap (via the sliding-lease lock).

### Remarks
- all integration tests use podman containers [aspire testing NA].
- dev container network is user-created. ensure isolation from host [kafka-netwok].
- podman containers are isolated using dedicated network [dev-netwok].
- podman containers:
  - when dev container is created podman containers are created.
  - when dev container is started podman containers are started (avoiding ghosts ports hanging).
  - when any, podman pull images from host registry images container.
  - coredns is using to resolve the kafka containers names inside containers network and from dev container.
- kafka setup:
  - create Kafka admin config options => create Kafka admin => create Kafka topic.
  - create Kafka publisher config options => create Kafka publisher => use Kafka publisher.
  - create Kafka consumer config options => create Kafka consumer => use Kafka consumer.
## Kafka client library.
A production-grade implementation of the transactional inbox/outbox pattern built specifically for Confluent.Kafka, with a purpose-built failure taxonomy covering delivery ambiguity, retry exhaustion, and dead-lettering rather than a bolted-on afterthought.

The library is Kafka-first by design, not broker-agnostic — but its internals are deliberately partitioned behind narrow, composable interfaces, keeping Kafka-specific concerns concentrated on specialized operations so a future multi-broker abstraction remains a realistic extension rather than a rewrite.

Still pre-production and not yet battle-tested against a live cluster, but every operation in the pipeline has been individually designed, reviewed, and hardened against real edge cases before a single integration test has run.

### Kafka pipelines (WIP)
---
* consuming (kafka messages - incoming): capturing -> redirecting -> mapping -> validating -> inserting -> offsetting -> handling* -> converting -> inserting -> mapping** -> producing -> scheduling.
* resuming (inbox messages - incoming): handling* -> converting -> inserting -> mapping** -> producing -> scheduling.
* redelivering (dead letter messages - incoming): mapping** -> producing -> scheduling.
* publishing (outbox messages - outgoing): validating ->  inserting -> mapping*** -> producing -> scheduling.
* relaying (dead letter messages - outgoing): mapping*** -> producing -> scheduling.

#### Consuming messages avoided race conditions
 - durable-save-before-offset-commit (via commit offset for saved messages).
 - restart/resumer overlap (via the inserted check).
 - fresh-message/resumer-message overlap (via the relay/resume intervals).
 - resumer-vs-resumer overlap (via the sliding-lease lock). avoid compete consumer pattern.


### Remarks
---
- all integration tests use podman containers [aspire testing NA].
- dev container network is user-created. ensure isolation from host [kafka-netwok].
- podman containers are isolated using dedicated network [dev-netwok].
- podman containers:
  - when dev container is created podman containers are created.
  - when dev container is started podman containers are started (avoiding ghosts ports hanging).
  - when any, podman pull images from host registry images container.
  - coredns is using to resolve the kafka containers names inside containers network and from dev container.
- functional-style library [OOP-free].
- podman-inside-of-podman.
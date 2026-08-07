## Backend kafka library.
- convenient functions for Kafka distributed event streaming platform.
- functional-style library [OOP-free].
- podman-inside-of-podman.

### Consumming Kafka Messages
---
Consumes a single Kafka message and drives it through the transactional inbox pattern:
- capture kafka message.
- insert inbox message.
- offset kafka consumer.
- handle business processing (transactional inbox pattern).
- on handling domain error dispatch dead letter.
- dispatch dead letter.

**Consuming messages avoided race conditions**
 - durable-save-before-offset-commit (via commit offset for saved messages).
 - restart/resumer overlap (via the inserted check).
 - fresh-message/resumer-message overlap (via the relay/resume intervals).
 - resumer-vs-resumer overlap (via the sliding-lease lock). avoid compete consumer pattern.

### Resuming Kafka Messages
---
Resume a single pending/deadletting inbox message and drives it through the transactional inbox pattern:
- handle business processing for inbox messages (transactional inbox pattern) - pending.
- on handling technical error schedule inbox message next retry - pending.
- on handling domain error dispatch dead letter - pending.
- on scheduling exhausted inbox message retries dispatch dead letter - pending.
- dispatch dead letter - deadlettering.
- on dispatching error delay dead letter next retry - deadlettering.
- on delaying exhausted dead letter retires abandon dead letter (notify dead letter abandon) - deadlettering.

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
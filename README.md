## Backend kafka library.
- convenient functions for Kafka distributed event streaming platform.
- functional-style library [OOP-free].
- podman-inside-of-podman.

### Consumming Kafka Messages
---

Consumes a single Kafka message and drives it through the transactional inbox pattern:
- capture kafka message.
- insert inbox message.
- commit the Kafka offset.
- handle business processing (transactional inbox pattern).
- on domain failure publish dead letter.

**Consuming messages avoided race conditions**
 - durable-save-before-offset-commit (to avoid move offset for unsaved messages).
 - restart/resumer overlap (via the inserted check).
 - fresh-message/resumer-message overlap (via the retry threshold).
 - resumer-vs-resumer overlap (via the sliding-lease lock). avoid compete consumer pattern.

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
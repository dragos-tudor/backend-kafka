## Backend kafka library.
- convenient functions for Kafka distributed event streaming platform.
- functional-style library [OOP-free].
- podman-inside-of-podman.

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
- this package was design specifically for Kafka. Having specific operations, pipelines, jobs interfaces based architecture moving to one general-purpose messaging client (to support RabbitMQ, NATS so) it wouldn't need for a totally rewrite.
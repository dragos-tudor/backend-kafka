## Backend kafka library.
- convenient functions for Kafka databases.
- functional-style library [OOP-free].
- podman-inside-of-podman.

### Usage [kafka]
tbi

### Remarks
- all integration tests use podman containers [aspire testing NA].
- dev container network is user-created. ensure isolation from host.
- podman containers are isolated from host [using same dev container network].
- podman containers:
  - when dev container is created podman containers are created.
  - when dev container is started podman containers are started (avoiding ghosts ports hanging).
  - when any, podman pull images from host registry images container.
- kafka setup:
  - create Kafka admin config options => create Kafka admin => create Kafka topic.
  - create Kafka publisher config options => create Kafka publisher => use Kafka publisher.
  - create Kafka consumer config options => create Kafka consumer => use Kafka consumer.
#!/bin/bash
set -euo pipefail

KAFKA_SERVER=${1:?Usage: $0 <kafka_server> <username> <password>}
KAFKA_USERNAME=${2:?Missing username}
KAFKA_PASSWORD=${3:?Missing password}

COUNT=0
MAX_RETRIES=5
echo "waiting for ${KAFKA_SERVER} to be ready..."
until podman exec "${KAFKA_SERVER}" /opt/kafka/bin/kafka-broker-api-versions.sh \
  --bootstrap-server kafka-1:9094,kafka-2:9094,kafka-3:9094 >/dev/null 2>&1; do
  COUNT=$((COUNT + 1))
  if [ "${COUNT}" -ge "${MAX_RETRIES}" ]; then
    echo "Timed out waiting for ${KAFKA_SERVER} after $((MAX_RETRIES * 2))s"
    exit 1
  fi
  echo " still starting (${COUNT}/${MAX_RETRIES})..."
  sleep 2
done

podman exec "${KAFKA_SERVER}" /opt/kafka/bin/kafka-configs.sh \
  --bootstrap-server kafka-1:9094,kafka-2:9094,kafka-3:9094 \
  --alter \
  --add-config "SCRAM-SHA-512=[password=${KAFKA_PASSWORD}]" \
  --entity-type users \
  --entity-name "${KAFKA_USERNAME}"
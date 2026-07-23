set -eo pipefail
set +H

# dnf install -y bash-completion
# podman completion -f /etc/bash_completion.d/podman bash

# podman run --rm docker.io/apache/kafka:4.3.1 /opt/kafka/bin/kafka-storage.sh random-uuid"
CLUSTER_ID=Y7fX2qL9RmKp0O4kS-VjnQ
SERVICES_ROOT=$WORKSPACE_ROOT/.services

echo "pulling kafka and coredns images"
podman pull docker.io/apache/kafka:4.3.1;
podman pull docker.io/coredns/coredns:1.14.4

echo "removing podman containers and dev network"
podman stop -a; podman rm -a; podman network rm "${DEV_NETWORK}" 2>/dev/null || true;

"${SERVICES_ROOT}"/networks/creating.sh "$DEV_NETWORK" "$DEV_NETWORK_SUBNET"
"${SERVICES_ROOT}"/coredns/starting.sh "$DEV_NETWORK" "$DEV_NETWORK_DNS"
nohup "${SERVICES_ROOT}"/coredns/watching.sh "$DEV_NETWORK" > "${SERVICES_ROOT}"/coredns/watch.log 2>&1 &
"${SERVICES_ROOT}"/kafka/starting.sh kafka-1 1 "$DEV_NETWORK" "$DEV_NETWORK_DNS" "$CLUSTER_ID"
"${SERVICES_ROOT}"/kafka/starting.sh kafka-2 2 "$DEV_NETWORK" "$DEV_NETWORK_DNS" "$CLUSTER_ID"
"${SERVICES_ROOT}"/kafka/starting.sh kafka-3 3 "$DEV_NETWORK" "$DEV_NETWORK_DNS" "$CLUSTER_ID"
"${SERVICES_ROOT}"/kafka/configuring.sh kafka-1 "$KAFKA_USERNAME" "$KAFKA_PASSWORD"
"${SERVICES_ROOT}"/kafka/configuring.sh kafka-2 "$KAFKA_USERNAME" "$KAFKA_PASSWORD"
"${SERVICES_ROOT}"/kafka/configuring.sh kafka-3 "$KAFKA_USERNAME" "$KAFKA_PASSWORD"

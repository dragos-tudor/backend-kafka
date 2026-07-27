set -eo pipefail

echo "waiting for podman to be ready..."
until podman info >/dev/null 2>&1; do
  sleep 2
done

echo "stop podman containers"
podman stop -a >/dev/null 2>&1 || true

echo "start podman containers"
podman start coredns kafka-1 kafka-2 kafka-3

$WORKSPACE_ROOT/.services/coredns/hosting.sh
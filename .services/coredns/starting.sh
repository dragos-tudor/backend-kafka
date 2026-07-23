set -euo pipefail

COREDNS_ROOT=$WORKSPACE_ROOT/.services/coredns
DEV_NETWORK=${1:?dev network missing}
DEV_NETWORK_DNS=${2:?dev network dns missing}

echo "starting CoreDNS container"
podman run -p 53:53/tcp -p 53:53/udp \
  -v "$COREDNS_ROOT/Corefile:/etc/coredns/Corefile:Z" \
  -v "$COREDNS_ROOT/dynamic-hosts:/etc/coredns/dynamic-hosts:Z" \
  --network "$DEV_NETWORK" --hosts-file=none --ip $DEV_NETWORK_DNS -d --name coredns \
  docker.io/coredns/coredns:1.14.4 \
  -conf /etc/coredns/Corefile
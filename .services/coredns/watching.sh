#!/bin/bash
set -euo pipefail

DEV_NETWORK=${1:?missing dev network}
COREDNS_ROOT="$WORKSPACE_ROOT/.services/coredns"
HOSTS_FILE="${COREDNS_ROOT}/dynamic-hosts"

regenerate() {
  echo "# auto-generated $(date)" > "${HOSTS_FILE}"
  podman ps -a --format '{{.Names}}' | while read -r name; do
    [ "${name}" = "coredns" ] && continue

    ip=$(podman exec "${name}" cat /etc/hosts | awk '/^[0-9]/ {line=$1} END {print line}')
    echo "add dynamic ip: ${name} -> ${ip}"
    [ -n "${ip}" ] && echo "${ip} ${name}" >> "${HOSTS_FILE}"
  done
}

regenerate  # initial population

podman events --format '{{.Status}}' | while read -r status; do
  case "$status" in
    (start|stop|died|remove)
      regenerate
      ;;
  esac
done
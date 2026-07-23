set -euo pipefail

DEV_NETWORK=${1:?missing dev network}
DEV_NETWORK_SUBNET=${2:?missing dev network}

if ! (podman network ls | grep "$DEV_NETWORK" > /dev/null); then
	echo "creating kafka dev network"
	podman network create --driver=bridge --subnet $DEV_NETWORK_SUBNET --disable-dns "$DEV_NETWORK" > /dev/null
fi
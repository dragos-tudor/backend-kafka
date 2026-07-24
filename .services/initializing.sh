set -e

KAFKA_NETWORK="kafka-network"
if ! (podman network ls | grep $KAFKA_NETWORK > /dev/null); then
	echo "create ${KAFKA_NETWORK}"
	podman network create --driver=bridge --disable-dns $KAFKA_NETWORK
fi

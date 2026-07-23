
KAFKA_NETWORK="kafka-network"
if ! (podman network ls | grep $KAFKA_NETWORK > /dev/null); then
	echo "create kafka network"
	podman network create --driver=bridge --disable-dns $KAFKA_NETWORK
fi

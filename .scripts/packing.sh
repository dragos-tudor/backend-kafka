set -eu

PROJECT=${WORKSPACE_ROOT}/Kafka/Kafka.csproj
VERSION=${1:?missing version}

dotnet pack \
  --configuration Release \
  --output "${WORKSPACE_ROOT}/.packages" \
  -p:PackOnly=true \
  -p:Version="${VERSION}" \
  -p:PackageVersion="${VERSION}" \
  $PROJECT

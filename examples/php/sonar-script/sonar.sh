#!/usr/bin/env bash
#for ARGUMENT in "$@"
#do
#   KEY=$(echo $ARGUMENT | cut -f1 -d=)
#
#   KEY_LENGTH=${#KEY}
#   VALUE="${ARGUMENT:$KEY_LENGTH+1}"
#
#   export "$KEY"="$VALUE"
#done

sonar-scanner -X  \
  -Dsonar.organization="rohith-prakash" \
  -Dsonar.projectKey="rohith-prakash_openapi-generator-php" \
  -Dsonar.projectName="rohith-prakash_openapi-generator-php" \
  -Dsonar.sources="src/Twilio/Rest" \
  -Dsonar.tests="tests" \
  -Dsonar.host.url="https://sonarcloud.io" \
  -Dsonar.login=$SONAR_TOKEN \
  -Dsonar.language="php" \
  -Dsonar.php.tests.reportPath="execution-result.xml" \
  -Dsonar.php.coverage.reportPaths="php_coverage.xml"


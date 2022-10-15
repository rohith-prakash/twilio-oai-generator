FROM php:7.4
RUN apt-get update -y && apt-get install -y git zip unzip wget nodejs
COPY --from=composer:latest /usr/bin/composer /usr/local/bin/composer
#COPY --from=sonarsource/sonar-scanner-cli:latest . /opt/sonar-scanner
RUN wget https://binaries.sonarsource.com/Distribution/sonar-scanner-cli/sonar-scanner-cli-4.2.0.1873-linux.zip \
    && unzip  sonar-scanner-cli-4.2.0.1873-linux.zip \
    && mv sonar-scanner-4.2.0.1873-linux /opt/sonar-scanner
ENV PATH="$PATH:/opt/sonar-scanner/bin"
ENV XDEBUG_MODE=coverage
RUN mkdir /sonar
COPY sonar-script/ /sonar
RUN chmod +x /sonar/sonar.sh
RUN mkdir /app
WORKDIR /app
RUN git clone https://github.com/twilio/twilio-php.git
ENV PATH="vendor/bin:$PATH"
WORKDIR /app/twilio-php

RUN rm -rf src/Twilio/Rest/*
COPY src/Twilio/Rest src/Twilio/Rest

RUN rm -rf tests/Twilio/*/
COPY unit-tests integration-tests tests/Twilio/

RUN composer install
RUN pecl install xdebug
RUN pecl install pcov && docker-php-ext-enable pcov

#CMD  ["/bin/bash", "-c", "sonar-scanner -v"]
#CMD  ["/bin/bash", "-c", "ls /opt/sonar-scanner"]

CMD ["/bin/bash", "-c", "set -o pipefail &&  phpunit -d memory_limit=512M --strict-coverage --disallow-test-output --colors --configuration tests/phpunit.xml --coverage-clover=php_coverage.xml  --log-junit=execution-result.xml 2>&1 | tee /local/test-report.out && cp php_coverage.xml  execution-result.xml /local && /sonar/sonar.sh"]

#RUN if [ "$SONAR_TOKEN" = "" ] ; then echo Skipping sonar analysis ;  else make cover PROJECT_NAME=rohith-prakash_openapi-generator-php SONAR_SOURCES="/d:sonar.sources=src/Twilio/Rest/**/*.* /d:sonar.tests=tests/Twilio/**/*.*" ; fi
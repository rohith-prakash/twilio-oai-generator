FROM php:7.4
RUN apt-get update -y && apt-get install -y git zip
COPY --from=composer:latest /usr/bin/composer /usr/local/bin/composer

ENV XDEBUG_MODE=coverage
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

CMD ["/bin/bash", "-c", "set -o pipefail &&  phpunit -d memory_limit=512M --strict-coverage --disallow-test-output --colors --configuration tests/phpunit.xml --coverage-clover=php_coverage.xml  --log-junit=execution-result.xml 2>&1 | tee /local/test-report.out && cp php_coverage.xml  execution-result.xml /local"]
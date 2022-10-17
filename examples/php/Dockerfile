FROM php:7.4 AS build
ARG SONAR_TOKEN=""
RUN mkdir /sonar
ADD sonar-script/sonar.sh /usr/local/bin/sonar.sh
RUN chmod +x /usr/local/bin/sonar.sh
ENV SONAR_TOKEN=$SONAR_TOKEN

RUN apt-get update -y && apt-get install -y zip unzip wget nodejs
RUN apt-get install -y git
RUN wget https://binaries.sonarsource.com/Distribution/sonar-scanner-cli/sonar-scanner-cli-4.2.0.1873-linux.zip \
    && unzip  sonar-scanner-cli-4.2.0.1873-linux.zip \
    && mv sonar-scanner-4.2.0.1873-linux /opt/sonar-scanner \

RUN git clone https://github.com/twilio/twilio-php.git twilio
RUN mkdir /twilio
WORKDIR /twilio

ENV PATH="vendor/bin:$PATH"

RUN rm -rf src/Twilio/Rest/*/
COPY src/Twilio/Rest src/Twilio/Rest

RUN rm -rf tests/Twilio/Integration/
COPY tests/Twilio/Integration tests/Twilio/Integration

RUN rm -rf tests/Twilio/Unit/Rest
COPY tests/Twilio/Unit/Rest tests/Twilio/Unit/Rest

COPY --from=composer:latest /usr/bin/composer /usr/local/bin/composer
RUN composer install --prefer-dist

RUN pecl install xdebug
RUN pecl install pcov && docker-php-ext-enable pcov

CMD ["/bin/bash", "-c", "phpunit -d memory_limit=512M --disallow-test-output --colors --configuration tests/phpunit.xml --coverage-clover=php_coverage.xml  --log-junit=execution-result.xml 2>&1 | tee /local/test-report.out && cp php_coverage.xml  execution-result.xml /local ; /bin/bash /usr/local/bin/sonar.sh "]

package com.twilio.oai;

import java.io.File;
import java.util.Arrays;
import java.util.Collection;
import java.util.stream.Stream;

import io.swagger.v3.oas.models.OpenAPI;
import io.swagger.v3.oas.models.PathItem;
import io.swagger.v3.oas.models.servers.Server;
import lombok.RequiredArgsConstructor;
import org.openapitools.codegen.DefaultCodegen;

@RequiredArgsConstructor
public class TwilioCodegenAdapter {

    private static final String INPUT_SPEC_PATTERN = "[^_]+_(?<domain>.+?)(_(?<version>[^_]+))?\\..+";
    // regex example : https://flex-api.twilio.com
    private static final String SERVER_PATTERN = "https://(?<domain>[^.]+)\\.twilio\\.com";

    private final DefaultCodegen codegen;
    private final String name;

    private String originalOutputDir;

    public void processOpts() {
        // Find the templates in the local resources dir.
        codegen.setTemplateDir(name);
        // Remove the "API" suffix from the API filenames.
        codegen.setApiNameSuffix("");
        codegen.setApiPackage("");

        originalOutputDir = codegen.getOutputDir();
        setDomain(getInputSpecDomain());

        final String version = getInputSpecVersion();
        codegen.additionalProperties().put("apiVersion", StringHelper.camelize(version, true));
        codegen.additionalProperties().put("apiVersionClass", StringHelper.camelize(version));

        codegen.supportingFiles().clear();

        Arrays.asList("Configuration", "Parameter", "Version").forEach(word -> {
            codegen.reservedWords().remove(word);
            codegen.reservedWords().remove(word.toLowerCase());
        });
    }

    public void setDomain(final String domain) {
        final String domainPackage = domain.replace("-", "");
        setOutputDir(domainPackage, getInputSpecVersion());

        codegen.additionalProperties().put("domainName", StringHelper.camelize(domain));
        codegen.additionalProperties().put("domainPackage", domainPackage);
    }

    public void setOutputDir(final String domain, final String version) {
        codegen.setOutputDir(originalOutputDir + File.separator + domain + File.separator + version);
    }

    public String toParamName(final String name) {
        return name.replace("<", "Before").replace(">", "After");
    }

    public String getDomainFromOpenAPI(final OpenAPI openAPI) {
        return openAPI
            .getPaths()
            .values()
            .stream()
            .findFirst()
            .map(PathItem::getServers)
            .map(Collection::stream)
            .flatMap(Stream::findFirst)
            .map(Server::getUrl)
            .map(url -> url.replaceAll(SERVER_PATTERN, "${domain}"))
            .orElseThrow();
    }

    private String getInputSpecDomain() {
        return codegen.getInputSpec().replaceAll(INPUT_SPEC_PATTERN, "${domain}");
    }

    private String getInputSpecVersion() {
        return codegen.getInputSpec().replaceAll(INPUT_SPEC_PATTERN, "${version}");
    }
}

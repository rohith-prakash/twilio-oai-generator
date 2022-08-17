package com.twilio.oai;

import org.junit.Test;
import org.openapitools.codegen.ClientOptInput;
import org.openapitools.codegen.DefaultGenerator;
import org.openapitools.codegen.config.CodegenConfigurator;

import java.io.File;
import java.util.List;

import static org.junit.Assert.assertFalse;

/**
 * This test allows you to easily launch your code generation software under a debugger. Then run this test under debug
 * mode.  You will be able to step through your java code and then see the results in the out directory.
 */
public class TwilioCsharpGeneratorTest {

    @Test
    public void launchCodeGenerator() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("twilio-csharp")
            .setInputSpec("/Users/rprakash/code/codegen-twilio/twilio-oai/spec/yaml/twilio_accounts_v1.yaml")
            .setOutputDir("codegen/twilio-csharp");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        final List<File> output = generator.opts(clientOptInput).generate();

        assertFalse(output.isEmpty());
    }
}

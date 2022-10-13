<?php

/**
 * This code was generated by
 * \ / _    _  _|   _  _
 * | (_)\/(_)(_|\/| |(/_  v1.0.0
 * /       /
 */

namespace Twilio\Rest\Conversations\V1\Service\Configuration;

use Twilio\Exceptions\TwilioException;
use Twilio\InstanceContext;
use Twilio\Options;
use Twilio\Serialize;
use Twilio\Values;
use Twilio\Version;

class WebhookContext extends InstanceContext {
    /**
     * Initialize the WebhookContext
     *
     * @param Version $version Version that contains the resource
     * @param string $chatServiceSid The unique ID of the [Conversation
     *                               Service](https://www.twilio.com/docs/conversations/api/service-resource) this conversation belongs to.
     */
    public function __construct(Version $version, $chatServiceSid) {
        parent::__construct($version);

        // Path Solution
        $this->solution = ['chatServiceSid' => $chatServiceSid, ];

        $this->uri = '/Services/' . \rawurlencode($chatServiceSid) . '/Configuration/Webhooks';
    }

    /**
     * Update the WebhookInstance
     *
     * @param array|Options $options Optional Arguments
     * @return WebhookInstance Updated WebhookInstance
     * @throws TwilioException When an HTTP error occurs.
     */
    public function update(array $options = []): WebhookInstance {
        $options = new Values($options);

        $data = Values::of([
            'PreWebhookUrl' => $options['preWebhookUrl'],
            'PostWebhookUrl' => $options['postWebhookUrl'],
            'Filters' => Serialize::map($options['filters'], function($e) { return $e; }),
            'Method' => $options['method'],
        ]);

        $payload = $this->version->update('POST', $this->uri, [], $data);

        return new WebhookInstance($this->version, $payload, $this->solution['chatServiceSid']);
    }

    /**
     * Fetch the WebhookInstance
     *
     * @return WebhookInstance Fetched WebhookInstance
     * @throws TwilioException When an HTTP error occurs.
     */
    public function fetch(): WebhookInstance {
        $payload = $this->version->fetch('GET', $this->uri);

        return new WebhookInstance($this->version, $payload, $this->solution['chatServiceSid']);
    }

    /**
     * Provide a friendly representation
     *
     * @return string Machine friendly representation
     */
    public function __toString(): string {
        $context = [];
        foreach ($this->solution as $key => $value) {
            $context[] = "$key=$value";
        }
        return '[Twilio.Conversations.V1.WebhookContext ' . \implode(' ', $context) . ']';
    }
}
/*
 * Twilio - Accounts
 *
 * This is the public Twilio REST API.
 *
 * API version: 1.11.0
 * Contact: support@twilio.com
 */

// Code generated by OpenAPI Generator (https://openapi-generator.tech); DO NOT EDIT.

package openapi

import (
	"encoding/json"

	"github.com/twilio/twilio-go/client"
)

// TestResponseObjectTestArrayOfObjects struct for TestResponseObjectTestArrayOfObjects
type TestResponseObjectTestArrayOfObjects struct {
	Count       float32 `json:"count,omitempty"`
	Description string  `json:"description,omitempty"`
}

func (response *TestResponseObjectTestArrayOfObjects) UnmarshalJSON(bytes []byte) (err error) {
	raw := struct {
		Count       interface{} `json:"count"`
		Description string      `json:"description"`
	}{}

	if err = json.Unmarshal(bytes, &raw); err != nil {
		return err
	}

	*response = TestResponseObjectTestArrayOfObjects{
		Description: raw.Description,
	}

	responseCount, err := client.UnmarshalFloat32(&raw.Count)
	if err != nil {
		return err
	}
	response.Count = *responseCount

	return
}
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Twilio.Clients;
using Twilio.Converters;
using Twilio.Exceptions;
using Twilio.Http;
using System.Collections;

using Twilio.Rest.Api.V2010;
using Twilio.Rest.Api.V2010.Credential;
using Twilio.Base;
using Twilio.Rest.Api.V2010.Account;

namespace Twilio.Test.Rest
{
    [TestFixture]
    public class TwilioRestTest
    {
        private const string TEST_INTEGER = "TestInteger";
        private const string ACCOUNT_SID = "AC222222222222222222222222222222";

        [Test]
        public void TestShouldMakeValidAPICallAWSFetcher()
        {
            var twilioRestClient = Substitute.For<ITwilioRestClient>();
                    Request request = new Request(
                            HttpMethod.Get,
                            Twilio.Rest.Domain.Api,
                            "/v1/Credentials/AWS"
                    );
                    string url = "https://api.twilio.com/v1/Credentials/AWS";
                    String testResponse = "{\"credentials\":[{\"sid\":\"CR12345678123456781234567812345678\", \"test_string\":\"Ahoy\", \"test_object\":{\"mms\": true, \"sms\":false, \"voice\": false, \"fax\":true}}, {\"sid\":\"CR12345678123456781234567812345678\", \"test_string\":\"Hello\", \"test_object\":{\"mms\": true, \"sms\":false, \"voice\": false, \"fax\":true}}], \"meta\": {\"url\":\"" + url + "\", \"next_page_url\":\"" + url + "?PageSize=5" + "\", \"previous_page_url\":\"" + url + "?PageSize=3" + "\", \"first_page_url\":\"" + url + "?PageSize=1" + "\", \"page_size\":4}}";
                    twilioRestClient.Request(request).Returns(new Response(System.Net.HttpStatusCode.OK, testResponse));
                    var response = AwsResource.Read(client: twilioRestClient);
                    Assert.IsNotNull(response);
                    var awsEnumerator = response.GetEnumerator();
                    Assert.IsNotNull(awsEnumerator);
                    awsEnumerator.MoveNext();
                    var firstResult = awsEnumerator.Current;
                    Assert.IsNotNull(firstResult);
                    Assert.AreEqual("CR12345678123456781234567812345678", firstResult.Sid);
                    awsEnumerator.MoveNext();
                    var secondResult = awsEnumerator.Current;
                    Assert.IsNotNull(secondResult);
                    Assert.AreEqual("CR12345678123456781234567812345678", secondResult.Sid);
        }


        [Test]
        public void testShouldGetInValidAPICallResponseAWSFetcher()
        {
            var twilioRestClient = Substitute.For<ITwilioRestClient>();
            var request = new Request(
                HttpMethod.Get,
                Twilio.Rest.Domain.Api,
                "/v1/Credentials/AWS/CRXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                ""
            );
            twilioRestClient.Request(request).Throws(new ApiException("Server Error, no content"));

            try
            {
                AwsResource.Fetch("CRXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", client: twilioRestClient);
                Assert.Fail("Expected TwilioException to be thrown for 500");
            }
            catch (ApiException) {}
            twilioRestClient.Received().Request(request);

        }

        [Test]
        public void TestAwsResourceDeleteRequest()
        {
            var twilioRestClient = Substitute.For<ITwilioRestClient>();
            var request = new Request(
                HttpMethod.Delete,
                Twilio.Rest.Domain.Api,
                "/v1/Credentials/AWS/CRXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                ""
            );
            twilioRestClient.Request(request).Throws(new ApiException("Server Error, no content"));

            try
            {
                AwsResource.Delete("CRXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", client: twilioRestClient);
                Assert.Fail("Expected TwilioException to be thrown for 500");
            }
            catch (ApiException) {}
            twilioRestClient.Received().Request(request);
        }

        [Test]
        public void TestAwsResourceDeleteResponse()
        {
            var twilioRestClient = Substitute.For<ITwilioRestClient>();
            twilioRestClient.AccountSid.Returns("ACXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            twilioRestClient.Request(Arg.Any<Request>())
                            .Returns(new Response(
                                         System.Net.HttpStatusCode.NoContent,
                                         "null"
                                     ));

            var response = AwsResource.Delete("CRXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", client: twilioRestClient);
            Assert.NotNull(response);
        }

        [Test]
        public void TestAwsResourceUpdateResponse()
        {
            var twilioRestClient = Substitute.For<ITwilioRestClient>();
            twilioRestClient.AccountSid.Returns("ACXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            twilioRestClient.Request(Arg.Any<Request>())
                            .Returns(new Response(
                                         System.Net.HttpStatusCode.OK,
                                         "{\"account_sid\": \"ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\",\"date_created\": \"2015-07-31T04:00:00Z\",\"date_updated\": \"2015-07-31T04:00:00Z\",\"friendly_name\": \"friendly_name\",\"sid\": \"CRaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\",\"url\": \"https://accounts.twilio.com/v1/Credentials/AWS/CRaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\"}"
                                     ));

            var response = AwsResource.Update("CRXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", client: twilioRestClient);
            Assert.NotNull(response);
        }

        [Test]
       public void TestUpdateAwsResourceRequest()
       {
           var twilioRestClient = Substitute.For<ITwilioRestClient>();
           var request = new Request(
               HttpMethod.Post,
               Twilio.Rest.Domain.Api,
               "/v1/Credentials/AWS/CRXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
               ""
           );
           twilioRestClient.Request(request).Throws(new ApiException("Server Error, no content"));
           try
           {
               AwsResource.Update("CRXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", client: twilioRestClient);
               Assert.Fail("Expected TwilioException to be thrown for 500");
           }
           catch (ApiException) {}
           twilioRestClient.Received().Request(request);
       }

       [Test]
       public void TestAwsResourceReadRequest()
       {
           var twilioRestClient = Substitute.For<ITwilioRestClient>();
           var request = new Request(
               HttpMethod.Get,
               Twilio.Rest.Domain.Api,
               "/v1/Credentials/AWS",
               ""
           );
           twilioRestClient.Request(request).Throws(new ApiException("Server Error, no content"));
           try
           {
               AwsResource.Read(client: twilioRestClient);
               Assert.Fail("Expected TwilioException to be thrown for 500");
           }
           catch (ApiException) {}
           twilioRestClient.Received().Request(request);
       }

       [Test]
       public void TestAwsResourceReadEmptyResponse()
       {
           var twilioRestClient = Substitute.For<ITwilioRestClient>();
           twilioRestClient.AccountSid.Returns("ACXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
           twilioRestClient.Request(Arg.Any<Request>())
                           .Returns(new Response(
                                        System.Net.HttpStatusCode.OK,
                                        "{\"credentials\": [],\"meta\": {\"first_page_url\": \"https://accounts.twilio.com/v1/Credentials/AWS?PageSize=50&Page=0\",\"key\": \"credentials\",\"next_page_url\": null,\"page\": 0,\"page_size\": 50,\"previous_page_url\": null,\"url\": \"https://accounts.twilio.com/v1/Credentials/AWS?PageSize=50&Page=0\"}}"
                                    ));
           var response = AwsResource.Read(client: twilioRestClient);
           Assert.NotNull(response);
       }

        [Test]
        public void TestAwsResourceObjectCreation()
        {
            string json = "{\"sid\": \"ACXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX\",\"test_string\":\"AwsResourceTestString\",\"test_integer\":123}";
            var awsResource = AwsResource.FromJson(json);
            Assert.IsNotNull(awsResource);
            Assert.AreEqual("ACXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",awsResource.Sid);
            Assert.AreEqual("AwsResourceTestString",awsResource.TestString);
            Assert.AreEqual(123,awsResource.TestInteger);
        }

        [Test]
        public void TestAwsResourcePagination()
        {
            var twilioRestClient = Substitute.For<ITwilioRestClient>();
            string firstPageURI = "/v1/Credentials/AWS";
            string secondPageURI = "/v1/Credentials/AWSN";

            string responseContentFirstPage = "{\"credentials\":[" +
                "{\"sid\":\"CR12345678123456781234567812345678\", \"test_string\":\"Ahoy\", \"test_object\":{\"mms\": true, \"sms\":false, \"voice\": false, \"fax\":true}}" +
                "],\"meta\": {\"url\":\"" + firstPageURI + "\", \"next_page_url\":\"" + secondPageURI + "?PageSize=2" + "\", \"previous_page_url\":\"" + firstPageURI + "?PageSize=2" + "\", \"first_page_url\":\"" + firstPageURI + "?PageSize=2" + "\", \"page_size\":2}}";

            string responseContentSecondPage = "{\"credentials\":[" +
                "{\"sid\":\"CR12345678123456781234567812345678\", \"test_string\":\"Matey\", \"test_object\":{\"mms\": true, \"sms\":false, \"voice\": false, \"fax\":true}}" +
                "],\"meta\": {\"url\":\"" + firstPageURI + "\", \"next_page_url\":\"" + "" + "?PageSize=2" + "\", \"previous_page_url\":\"" + firstPageURI + "?PageSize=2" + "\", \"first_page_url\":\"" + firstPageURI + "?PageSize=2" + "\", \"page_size\":2}}";

            Page<AwsResource> secondPage = Page<AwsResource>.FromJson("credentials", responseContentSecondPage);
            Assert.IsNotNull(secondPage);

            twilioRestClient.Request(Arg.Any<Request>()).Returns(new Response(System.Net.HttpStatusCode.OK, responseContentFirstPage));
            Page<AwsResource> previousPage = AwsResource.PreviousPage(secondPage, client: twilioRestClient); //Get's First Page
            Assert.IsNotNull(previousPage);

            twilioRestClient.Request(Arg.Any<Request>()).Returns(new Response(System.Net.HttpStatusCode.OK, responseContentSecondPage));
            Page<AwsResource> page = AwsResource.GetPage(secondPageURI,twilioRestClient);//Get's second page
            Assert.IsNotNull(page);
            Page<AwsResource> nextPage = AwsResource.NextPage(previousPage, twilioRestClient); // Get's second page
            Assert.IsNotNull(nextPage);

            Assert.AreEqual("Matey", page.Records[0].TestString);
            Assert.AreEqual("Matey", secondPage.Records[0].TestString);
            Assert.AreEqual("Ahoy", previousPage.Records[0].TestString);
            Assert.AreEqual("Matey", nextPage.Records[0].TestString);
        }

        [Test]
        public void TestShouldMakeValidAPICallAwsResourceDelete()
        {
            var twilioRestClient = Substitute.For<ITwilioRestClient>();
            string testResponse = "{\"accountSid\": \"sid\"}";
            twilioRestClient.Request(Arg.Any<Request>()).Returns(new Response(System.Net.HttpStatusCode.OK, testResponse));
            bool resource = AwsResource.Delete(ACCOUNT_SID,client:twilioRestClient);
            Assert.False(resource);
        }

        [Test]
        public void TestAwsResourceObjectCreationExceptionForBrokenJson()
        {
            string json = "{broken_json_key:value";
            Assert.Throws<ApiException>(() => AwsResource.FromJson(json));
        }

        [Test]
        public void testAwsResourceGet()
        {
            String json = "{\"account_sid\": \"a123\", \"sid\": \"123\", \"test_integer\": 123, \"test_number\": 123.1, \"test_number_float\": 123.2, " +
                "\"test_enum\": \"paused\"}";
            String jsonDuplicate = "{\"account_sid\": \"a123\", \"sid\": \"123\", \"test_integer\": 123, \"test_number\": 123.1, \"test_number_float\": 123.2, " +
                 "\"test_enum\": \"paused\"}";

            AwsResource aws = AwsResource.FromJson(json);
            AwsResource awsDuplicate = AwsResource.FromJson(jsonDuplicate);

            Assert.IsNotNull(aws);
            Assert.IsNotNull(awsDuplicate);
            Assert.IsNotNull(aws.AccountSid);
            Assert.IsNotNull(aws.Sid);
            Assert.IsNotNull(aws.TestEnum);
            Assert.AreEqual(aws.AccountSid,awsDuplicate.AccountSid);
            Assert.AreEqual(aws.Sid,awsDuplicate.Sid);
            Assert.AreEqual(aws.TestInteger,awsDuplicate.TestInteger);
            Assert.AreEqual(aws.TestNumber,awsDuplicate.TestNumber);
            Assert.AreEqual(aws.TestNumberFloat,awsDuplicate.TestNumberFloat);
            Assert.AreEqual(aws.TestEnum,awsDuplicate.TestEnum);

            Assert.AreEqual("a123",aws.AccountSid);
            Assert.AreEqual("123",aws.Sid);
            Assert.AreEqual(123,aws.TestInteger);
        }

        [Test]
        public void TestAwsResourceUpdate()
        {
            var twilioRestClient = Substitute.For<ITwilioRestClient>();
            string testResponse = "{\"account_sid\":\"" + ACCOUNT_SID + "\",\"test_string\":\"hello\" }";
            twilioRestClient.Request(Arg.Any<Request>()).Returns(new Response(System.Net.HttpStatusCode.OK, testResponse));
            AwsResource aws = AwsResource.Update(ACCOUNT_SID, client: twilioRestClient);
            Assert.IsNotNull(aws);
            Assert.IsNotNull(aws.TestString);
            Assert.AreEqual("hello", aws.TestString);
        }

        [Test]
        public void TestShouldAddAccountSidIfNotPresent()
        {
            var twilioRestClient = Substitute.For<ITwilioRestClient>();
            twilioRestClient.AccountSid.Returns(ACCOUNT_SID);
            Request request = new Request(
                    HttpMethod.Get,
                    Twilio.Rest.Domain.Api,
                    "/2010-04-01/Accounts/AC222222222222222222222222222222/Calls/123.json"
            );

            twilioRestClient.Request(request).Returns(new Response(System.Net.HttpStatusCode.OK, "{\"account_sid\":\"AC222222222222222222222222222222\",\"call_sid\":\"PNXXXXY\", \"sid\":123, \"test_object\":{\"mms\": true, \"sms\":false, \"voice\": false, \"fax\":true}}"));
            CallResource call = CallResource.Fetch(123,client:twilioRestClient);
            Assert.IsNotNull(call);
            Assert.AreEqual("123", call.Sid);
            Assert.AreEqual(ACCOUNT_SID, call.AccountSid);
        }

        [Test]
        public void TestCallObjectCreation()
        {
            String json = "{\"test_integer\": 123}";
            CallResource call = CallResource.FromJson(json);
            Assert.IsNotNull(call);
            Assert.IsNotNull(call.TestInteger);
            Assert.AreEqual(123, call.TestInteger);
        }

        [Test]
        public void testCallResourceGet()
        {
            String json = "{\"account_sid\": \"a123\", \"sid\": \"123\", \"test_integer\": 123, \"test_number\": 123.1, \"test_number_float\": 123.2, " +
                "\"test_enum\": \"paused\"}";
            String jsonDuplicate = "{\"account_sid\": \"a123\", \"sid\": \"123\", \"test_integer\": 123, \"test_number\": 123.1, \"test_number_float\": 123.2, " +
                 "\"test_enum\": \"paused\"}";

            CallResource call = CallResource.FromJson(json);
            CallResource callDuplicate = CallResource.FromJson(jsonDuplicate);

            Assert.IsNotNull(call);
            Assert.IsNotNull(callDuplicate);
            Assert.IsNotNull(call.AccountSid);
            Assert.IsNotNull(call.Sid);
            Assert.IsNotNull(call.TestEnum);
            Assert.AreEqual(call.AccountSid,callDuplicate.AccountSid);
            Assert.AreEqual(call.Sid,callDuplicate.Sid);
            Assert.AreEqual(call.TestInteger,callDuplicate.TestInteger);
            Assert.AreEqual(call.TestNumber,callDuplicate.TestNumber);
            Assert.AreEqual(call.TestNumberFloat,callDuplicate.TestNumberFloat);
            Assert.AreEqual(call.TestEnum,callDuplicate.TestEnum);

            Assert.AreEqual("a123",call.AccountSid);
            Assert.AreEqual("123",call.Sid);
            Assert.AreEqual(123,call.TestInteger);
        }



    }
}
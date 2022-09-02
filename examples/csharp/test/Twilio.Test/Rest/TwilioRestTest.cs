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

namespace Twilio.Test.Rest
{
    [TestFixture]
    public class TwilioRestTest
    {
        private const string TEST_INTEGER = "TestInteger";
        private const string ACCOUNT_SID = "AC222222222222222222222222222222";
//        [Test]
//        public void TestShouldMakeValidAPICall()
//        {
//            var twilioRestClient = Substitute.For<ITwilioRestClient>();
//            var request = new Request(
//                HttpMethod.Get,
//                Twilio.Rest.Domain.Api,
//                "/2010-04-01/Accounts/AC222222222222222222222222222222.json"
//            );
//
//            twilioRestClient.Request(request).Returns(new Response(System.Net.HttpStatusCode.OK,"{\"account_sid\":\"AC222222222222222222222222222222\", \"call_sid\":\"PNXXXXY\"}"));
//            //getobjectmapper
//            twilioRestClient.AccountSid.Returns("AC222222222222222222222222222222");
//            var account = AccountResource.Fetch(ACCOUNT_SID,client: twilioRestClient);
//            Assert.IsNotNull(account);
//            Assert.AreEqual("AC222222222222222222222222222222",account.Sid);
//
//        }

//        [Test]
//        public void TestShouldMakeInValidAPICallWithExceptionForAccountFetcher()
//        {
//            var twilioRestClient = Substitute.For<ITwilioRestClient>();
//            var request = new Request(
//                HttpMethod.Get,
//                Twilio.Rest.Domain.Api,
//                "/2010-04-01/Accounts/AC222222222222222222222222222222.json"
//            );
//            twilioRestClient.AccountSid.Returns("AC222222222222222222222222222222");
//            twilioRestClient.Request(request).Returns(new Response(System.Net.HttpStatusCode.NotFound,"{\"account_sid\":\"AC222222222222222222222222222222\", \"call_sid\":\"PNXXXXY\"}"));
//            AccountResource.Fetch("AC222222222222222222222222222222",client: twilioRestClient);
//        }

//        [Test]
//        public void TestShouldMakeValidAPICallAccountDeleter()
//        {
//           var twilioRestClient = Substitute.For<ITwilioRestClient>();
//           var request = new Request(
//               HttpMethod.Delete,
//               Twilio.Rest.Domain.Api,
//               "/2010-04-01/Accounts/AC222222222222222222222222222222.json"
//           );
//           //twilioRestClient.AccountSid.Returns("AC222222222222222222222222222222");
//           twilioRestClient.Request(request).Returns(new Response(System.Net.HttpStatusCode.OK,"{\"account_sid\":\"AC222222222222222222222222222222\", \"call_sid\":\"PNXXXXY\"}"));
//           bool account = AccountResource.Delete("AC222222222222222222222222222222",client:twilioRestClient);
//           Assert.False(account);
//
//        }


//        [Test]
//                public void TestShouldMakeInValidAPICallWithExceptionForAccountFetcher()
//                {
//                    Assert.AreEqual(1,1);
//                }

//        [Test]
//        public void TestShouldReadListOfMessages()
//        {
//            var twilioRestClient = Substitute.For<ITwilioRestClient>();
//            var request = new Request(
//                HttpMethod.Get,
//                Twilio.Rest.Domain.Api,
//                "/v1/Credentials/AWS"
//            );
//            string url = "https://api.twilio.com/v1/Credentials/AWS";
//            string testResponse =  "{\"credentials\":[{\"sid\":\"CR12345678123456781234567812345678\", \"test_string\":\"Ahoy\", \"test_object\":{\"mms\": true, \"sms\":false, \"voice\": false, \"fax\":true}}, {\"sid\":\"CR12345678123456781234567812345678\", \"test_string\":\"Hello\", \"test_object\":{\"mms\": true, \"sms\":false, \"voice\": false, \"fax\":true}}], \"meta\": {\"url\":\"" + url + "\", \"next_page_url\":\"" + url + "?PageSize=5" + "\", \"previous_page_url\":\"" + url + "?PageSize=3" + "\", \"first_page_url\":\"" + url + "?PageSize=1" + "\", \"page_size\":4}}";
//            twilioRestClient.Request(request).Returns(new Response(System.Net.HttpStatusCode.OK,testResponse));
//            ResourceSet<AwsResource> awsPages = AwsResource.Read(client:twilioRestClient);
//            Assert.IsNotNull(awsPages);
//            IEnumerator awsPagesEnumerator = awsPages.GetEnumerator();
//            awsPagesEnumerator.Reset();
//            var awsPage1 = (Page<AwsResource>)awsPagesEnumerator.Current;
//
//            Console.WriteLine("######################################");
//            Console.WriteLine(awsPage1);
//            //Assert.AreEqual("Ahoy",awsPage);
//            //Assert.AreEqual("Hello")
//
//        }
        [Test]
            public void TestReadRequest()
            {
                var twilioRestClient = Substitute.For<ITwilioRestClient>();
                var request = new Request(
                    HttpMethod.Get,
                    Twilio.Rest.Domain.Account,
                    "/v1/Credentials/AWS",
                    ""
                );
                twilioRestClient.Request(request).Throws(new ApiException("Server Error, no content"));

                try
                {
                    AwsResource.Read(client: twilioRestClient);
                    Assert.Fail("Expected TwilioException to be thrown for 500");
                }
                catch (ApiException) { }
                twilioRestClient.Received().Request(request);
            }

            [Test]
            public void TestReadEmptyResponse()
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
            public void TestReadFullResponse()
            {
                var twilioRestClient = Substitute.For<ITwilioRestClient>();
                twilioRestClient.AccountSid.Returns("ACXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                twilioRestClient.Request(Arg.Any<Request>())
                                .Returns(new Response(
                                             System.Net.HttpStatusCode.OK,
                                             "{\"credentials\": [{\"account_sid\": \"ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\",\"date_created\": \"2015-07-31T04:00:00Z\",\"date_updated\": \"2015-07-31T04:00:00Z\",\"friendly_name\": \"friendly_name\",\"sid\": \"CRaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\",\"url\": \"https://accounts.twilio.com/v1/Credentials/AWS/CRaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\"}],\"meta\": {\"first_page_url\": \"https://accounts.twilio.com/v1/Credentials/AWS?PageSize=50&Page=0\",\"key\": \"credentials\",\"next_page_url\": null,\"page\": 0,\"page_size\": 50,\"previous_page_url\": null,\"url\": \"https://accounts.twilio.com/v1/Credentials/AWS?PageSize=50&Page=0\"}}"
                                         ));

                var response = AwsResource.Read(client: twilioRestClient);
                Assert.NotNull(response);
            }

            [Test]
            public void TestCreateRequest()
            {
                var twilioRestClient = Substitute.For<ITwilioRestClient>();
                var request = new Request(
                    HttpMethod.Post,
                    Twilio.Rest.Domain.Account,
                    "/v1/Credentials/AWS",
                    ""
                );
                request.AddPostParam("Credentials", Serialize("AKIAIOSFODNN7EXAMPLE:wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY"));
                twilioRestClient.Request(request).Throws(new ApiException("Server Error, no content"));

                try
                {
                    AwsResource.Create("AKIAIOSFODNN7EXAMPLE:wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY", client: twilioRestClient);
                    Assert.Fail("Expected TwilioException to be thrown for 500");
                }
                catch (ApiException) { }
                twilioRestClient.Received().Request(request);
            }

            [Test]
            public void TestCreateResponse()
            {
                var twilioRestClient = Substitute.For<ITwilioRestClient>();
                twilioRestClient.AccountSid.Returns("ACXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                twilioRestClient.Request(Arg.Any<Request>())
                                .Returns(new Response(
                                             System.Net.HttpStatusCode.Created,
                                             "{\"account_sid\": \"ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\",\"date_created\": \"2015-07-31T04:00:00Z\",\"date_updated\": \"2015-07-31T04:00:00Z\",\"friendly_name\": \"friendly_name\",\"sid\": \"CRaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\",\"url\": \"https://accounts.twilio.com/v1/Credentials/AWS/CRaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\"}"
                                         ));

                var response = AwsResource.Create("AKIAIOSFODNN7EXAMPLE:wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY", client: twilioRestClient);
                Assert.NotNull(response);
            }

            [Test]
            public void TestFetchRequest()
            {
                var twilioRestClient = Substitute.For<ITwilioRestClient>();
                var request = new Request(
                    HttpMethod.Get,
                    Twilio.Rest.Domain.Account,
                    "/v1/Credentials/AWS/CRXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                    ""
                );
                twilioRestClient.Request(request).Throws(new ApiException("Server Error, no content"));

                try
                {
                    AwsResource.Fetch("CRXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", client: twilioRestClient);
                    Assert.Fail("Expected TwilioException to be thrown for 500");
                }
                catch (ApiException) { }
                twilioRestClient.Received().Request(request);
            }

            [Test]
            public void TestFetchResponse()
            {
                var twilioRestClient = Substitute.For<ITwilioRestClient>();
                twilioRestClient.AccountSid.Returns("ACXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                twilioRestClient.Request(Arg.Any<Request>())
                                .Returns(new Response(
                                             System.Net.HttpStatusCode.OK,
                                             "{\"account_sid\": \"ACaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\",\"date_created\": \"2015-07-31T04:00:00Z\",\"date_updated\": \"2015-07-31T04:00:00Z\",\"friendly_name\": \"friendly_name\",\"sid\": \"CRaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\",\"url\": \"https://accounts.twilio.com/v1/Credentials/AWS/CRaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\"}"
                                         ));

                var response = AwsResource.Fetch("CRXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", client: twilioRestClient);
                Assert.NotNull(response);
            }

            [Test]
            public void TestUpdateRequest()
            {
                var twilioRestClient = Substitute.For<ITwilioRestClient>();
                var request = new Request(
                    HttpMethod.Post,
                    Twilio.Rest.Domain.Account,
                    "/v1/Credentials/AWS/CRXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                    ""
                );
                twilioRestClient.Request(request).Throws(new ApiException("Server Error, no content"));

                try
                {
                    AwsResource.Update("CRXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", client: twilioRestClient);
                    Assert.Fail("Expected TwilioException to be thrown for 500");
                }
                catch (ApiException) { }
                twilioRestClient.Received().Request(request);
            }

            [Test]
            public void TestUpdateResponse()
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
            public void TestDeleteRequest()
            {
                var twilioRestClient = Substitute.For<ITwilioRestClient>();
                var request = new Request(
                    HttpMethod.Delete,
                    Twilio.Rest.Domain.Account,
                    "/v1/Credentials/AWS/CRXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                    ""
                );
                twilioRestClient.Request(request).Throws(new ApiException("Server Error, no content"));

                try
                {
                    AwsResource.Delete("CRXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", client: twilioRestClient);
                    Assert.Fail("Expected TwilioException to be thrown for 500");
                }
                catch (ApiException) { }
                twilioRestClient.Received().Request(request);
            }

            [Test]
            public void TestDeleteResponse()
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
    }
}
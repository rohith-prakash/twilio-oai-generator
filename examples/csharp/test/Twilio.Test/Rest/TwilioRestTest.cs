using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Twilio.Clients;
using Twilio.Converters;
using Twilio.Exceptions;
using Twilio.Http;

using Twilio.Rest.Api.V2010;

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


        [Test]
                public void TestShouldMakeInValidAPICallWithExceptionForAccountFetcher()
                {
                    Assert.AreEqual(1,1);
                }
    }
}
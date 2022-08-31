using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Twilio.Clients;
using Twilio.Converters;
using Twilio.Exceptions;
using Twilio.Http;

using Twilio.Rest.Api.V2010.Account;

namespace Twilio.Test.Rest.TwilioRestTest
{
    [TestFixture]
    public class TwilioRestTest:TwilioTest
    {
        [Test]
        public void TestShouldMakeValidAPICall()
        {
            var twilioRestClient = Substitute.For<ITwilioRestClient>();
            var request = new Request(
                HttpMethod.Get,
                "api",
                "/2010-04-01/Accounts/AC222222222222222222222222222222.json"
            );


        }
    }
}
/*
 * This code was generated by
 * ___ _ _ _ _ _    _ ____    ____ ____ _    ____ ____ _  _ ____ ____ ____ ___ __   __
 *  |  | | | | |    | |  | __ |  | |__| | __ | __ |___ |\ | |___ |__/ |__|  | |  | |__/
 *  |  |_|_| | |___ | |__|    |__| |  | |    |__] |___ | \| |___ |  \ |  |  | |__| |  \
 *
 * Twilio - Accounts
 * This is the public Twilio REST API.
 *
 * NOTE: This class is auto generated by OpenAPI Generator.
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


using System;
using System.Collections.Generic;
using Twilio.Base;
using Twilio.Converters;

using Twilio.Types;


namespace Twilio.Rest.Api.V2010.Credential
{
    public class DeleteAwsOptions : IOptions<AwsResource>
    {
        
        public string PathSid { get; set; }


        public DeleteAwsOptions(string pathSid)
        {
            PathSid = pathSid;
        }

        
    public  List<KeyValuePair<string, string>> GetParams()
    {
        var p = new List<KeyValuePair<string, string>>();

        return p;
    }
        



    }


    public class FetchAwsOptions : IOptions<AwsResource>
    {
    
        public string PathSid { get; set; }


        public FetchAwsOptions(string pathSid)
        {
            PathSid = pathSid;
        }

        
    public  List<KeyValuePair<string, string>> GetParams()
    {
        var p = new List<KeyValuePair<string, string>>();

        return p;
    }
        



    }


    public class ReadAwsOptions : ReadOptions<AwsResource>
    {
    
        public int? PageSize { get; set; }



        
    public  override List<KeyValuePair<string, string>> GetParams()
    {
        var p = new List<KeyValuePair<string, string>>();

        if (PageSize != null)
        {
            p.Add(new KeyValuePair<string, string>("PageSize", PageSize.ToString()));
        }
        return p;
    }
        



    }

    public class UpdateAwsOptions : IOptions<AwsResource>
    {
    
        public string PathSid { get; set; }
        public string TestString { get; set; }
        public bool? TestBoolean { get; set; }


        public UpdateAwsOptions(string pathSid)
        {
            PathSid = pathSid;
        }

        
    public  List<KeyValuePair<string, string>> GetParams()
    {
        var p = new List<KeyValuePair<string, string>>();

        if (TestString != null)
        {
            p.Add(new KeyValuePair<string, string>("TestString", TestString));
        }
        if (TestBoolean != null)
        {
            p.Add(new KeyValuePair<string, string>("TestBoolean", TestBoolean.Value.ToString().ToLower()));
        }
        return p;
    }
        



    }


}


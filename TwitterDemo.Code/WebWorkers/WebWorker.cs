using System;
using System.Threading.Tasks;
using Hammock;
using Hammock.Authentication.OAuth;
using Hammock.Web;

namespace TwitterDemo.Code.WebWorkers
{
    public class WebWorker
    {
        public virtual RestClient PrepareClient(string domain, string apiVersion = null)
        {
            return new RestClient()
                {Authority = domain, VersionPath = apiVersion};
        }

        public virtual RestRequest PrepareRequest(OAuthCredentials creds, string path, WebMethod method)
        {
            var request = new RestRequest()
            {

                Credentials = new OAuthCredentials()
                {
                    Type = OAuthType.ProtectedResource,
                    SignatureMethod = OAuthSignatureMethod.HmacSha1,
                    ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader,
                    ConsumerKey = creds.ConsumerKey,
                    ConsumerSecret = creds.ConsumerSecret,
                    Token = creds.Token,
                    TokenSecret = creds.TokenSecret,
                },
                Path = path,
                Method = method
            };
            return request;
        }

        /// <summary>
        /// Makes a request to the server and returns json that was sent in response
        /// </summary>
        public virtual async Task<RestResponse> MakeRequest(RestClient client, RestRequest request)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            return await Task<RestResponse>.Factory.StartNew(() => client.Request(request));
        }
    }
}

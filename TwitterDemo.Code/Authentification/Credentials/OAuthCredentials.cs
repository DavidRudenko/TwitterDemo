using System;

namespace TwitterDemo.Code.Authentification.Credentials
{
    [Serializable]
    sealed class OAuthCredentials
    {
        public OAuthCredentials(string tokenSecret, string consumerKey, string consumerSecret, string token)
        {
            TokenSecret = tokenSecret;
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            Token = token;
        }
        public string ConsumerKey { get; }
        public string ConsumerSecret { get; }
        public string Token { get; }
        public string TokenSecret { get; }
        
    }
}

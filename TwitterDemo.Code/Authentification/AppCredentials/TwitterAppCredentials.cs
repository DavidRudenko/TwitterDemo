namespace TwitterDemo.Code.Authentification.AppCredentials
{
    /// <summary>
    /// Application credentials
    /// </summary>
    public class TwitterAppCredentials
    {
        public string ConsumerKey { get; }

        public string ConsumerKeySecret { get; }

        public TwitterAppCredentials(string consumerKey, string consumerKeySecret)
        {
            ConsumerKey = consumerKey;
            ConsumerKeySecret = consumerKeySecret;
        }
    }
}

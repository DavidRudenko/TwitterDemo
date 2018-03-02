using TwitterDemo.Code.Authentification.AppCredentials;
using TwitterDemo.Code.Authentification.AppCredentialsObtainers;

namespace DelayedPoster.Code.Authentification.AppCredentialsObtainers
{
    
    public class TwitterAppCredentialsObtainer:ITwitterAppCredentialsObtainer
    {
        //TODO:Get credentials from file 
        public TwitterAppCredentials GetAppCredentials()
        {
            return new TwitterAppCredentials("consumer_key",
                "consumer_secret");
        }
    }
}

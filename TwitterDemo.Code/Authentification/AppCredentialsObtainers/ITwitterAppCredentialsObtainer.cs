using TwitterDemo.Code.Authentification.AppCredentials;

namespace TwitterDemo.Code.Authentification.AppCredentialsObtainers
{
    public interface ITwitterAppCredentialsObtainer
    {
        TwitterAppCredentials GetAppCredentials();
    }
}

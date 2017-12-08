using System;
using DelayedPoster.Code.Authentification.AppCredentials;
using DelayedPoster.Code.Authentification.PinObtainers;
using TweetSharp;
using DelayedPoster.Code.Authentification.AppCredentialsObtainers;
using DelayedPoster.Code.Authentification.EventArgs;

namespace DelayedPoster.Code.Authentification
{
    /// <summary>
    /// Authentification credentials.
    /// </summary>
    public sealed class TwitterAuthCredentials:IAuthCredentials
    {
        public event EventHandler<OperationResultEventArgs> LoginCompleted;

        private readonly ITwitterAppCredentialsObtainer _credentialsObtainer;
        private readonly IPinObtainer _pinObtainer;
        private readonly string _consumerKey;
        private readonly string _consumerSecret;
        public TwitterService Service { get; }
        public TwitterAuthCredentials(IPinObtainer pinObtainer,ITwitterAppCredentialsObtainer credentialsObtainer)
        {
            var appcreds = credentialsObtainer.GetAppCredentials();
            Service = new TwitterService(appcreds.ConsumerKey, appcreds.ConsumerKeySecret);
            _pinObtainer = pinObtainer;
            _credentialsObtainer = credentialsObtainer;
            TwitterAppCredentials appCredentials = _credentialsObtainer.GetAppCredentials();
            _consumerKey = appCredentials.ConsumerKey;
            _consumerSecret = appCredentials.ConsumerKeySecret;
        }

        public TwitterAuthCredentials(IPinObtainer pinObtainer, string consumerKey, string consumerSecret)
        {
            _consumerSecret = consumerSecret;
            _consumerKey = consumerKey;
            _pinObtainer = pinObtainer;
            Service=new TwitterService(consumerKey,consumerSecret);
        }

        public void TryLogin()
        {
            OAuthRequestToken token = Service.GetRequestToken();
            Uri  uri = Service.GetAuthorizationUri(token);
            _pinObtainer.GetPin(uri.AbsoluteUri);
            _pinObtainer.PinCodeObtained += (sender, e) =>
            {
                var loginResult=TryLoginImpl(e.PinCode, token, Service);
                RaiseLoginCompleted(new OperationResultEventArgs(loginResult));
            };

        }

        private bool TryLoginImpl(string pin, OAuthRequestToken token, TwitterService Service)
        {
            OAuthAccessToken access = Service.GetAccessToken(token, pin);
            if (access.UserId == 0 && access.Token == "?" && access.TokenSecret == "?")
                return false;
            Service.AuthenticateWith(access.Token, access.TokenSecret);
            
            return true;
        }
        void RaiseLoginCompleted(OperationResultEventArgs e)
        {
            LoginCompleted?.Invoke(this, e);
        }
    }
}

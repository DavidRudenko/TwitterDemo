using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TweetSharp;
using TwitterDemo.Code;
using TwitterDemo.Code.Authentification;
using TwitterDemo.Code.Authentification.AppCredentials;
using TwitterDemo.Code.Authentification.AppCredentialsObtainers;
using TwitterDemo.Code.Authentification.Credentials;
using TwitterDemo.Code.Authentification.EventArgs;
using TwitterDemo.Code.Authentification.PinObtainers;

//TODO:Fix Auth 
namespace DelayedPoster.Code.Authentification
{
    /// <summary>
    /// Authentification credentials.
    /// </summary>
    [Serializable]
    public sealed class TwitterAuthCredentials:IAuthCredentials
    {
        public event EventHandler<OperationResultEventArgs> LoginCompleted;

        public bool LoggedIn { get; private set; }

       
        [NonSerialized]
        private ITwitterAppCredentialsObtainer _credentialsObtainer;
        [NonSerialized]
        private IPinObtainer _pinObtainer;
        private string _serializationFilePath = @"../../data.bin";
        [NonSerialized]
        private TwitterService _service;

        public string ConsumerKey { get; private set; }
        public string ConsumerSecret { get; private set; }
        public string Token { get; private set; }
        public string TokenSecret { get; private set; }

        public TwitterService Service
        {
            get { return _service; }
            private set { _service = value; }
        }

        #region Ctors & Destructor

        public TwitterAuthCredentials(IPinObtainer pinObtainer,ITwitterAppCredentialsObtainer credentialsObtainer)
        {
            _pinObtainer = pinObtainer;
            _credentialsObtainer = credentialsObtainer;

            if (TryDeserialize(_serializationFilePath))
                return;

            var appcreds = credentialsObtainer.GetAppCredentials();
            _service = new TwitterService(appcreds.ConsumerKey, appcreds.ConsumerKeySecret);

            TwitterAppCredentials appCredentials = _credentialsObtainer.GetAppCredentials();
            ConsumerKey = appCredentials.ConsumerKey;
            ConsumerSecret = appCredentials.ConsumerKeySecret;
        }
        ~TwitterAuthCredentials()
        {
            TrySerialize(_serializationFilePath);
        }
        public TwitterAuthCredentials(IPinObtainer pinObtainer, string consumerKey, string consumerSecret)
        {
            if (TryDeserialize(_serializationFilePath))
                return;
            ConsumerSecret = consumerSecret;
            ConsumerKey = consumerKey;
            _pinObtainer = pinObtainer;
            _service=new TwitterService(consumerKey,consumerSecret);
        }

        #endregion
        
        public IAsyncResult BeginGetUser()
        {
            if(LoggedIn)
                return Service.BeginGetUserProfile(new GetUserProfileOptions() {IncludeEntities = true});
            return null;
        }
        public TwitterUser EndGetUser(IAsyncResult asyncResult)
        {
            if(asyncResult!=null)
                return Service.EndGetUserProfile(asyncResult);
            return null;
        }
        public void TryLogin()
        {
            if (LoggedIn)
            {
                RaiseLoginCompleted(true);
                return;
            }
            OAuthRequestToken token = Service.GetRequestToken();
            Uri  uri = Service.GetAuthorizationUri(token);
           
            _pinObtainer.GetPin(uri.AbsoluteUri);
            _pinObtainer.PinCodeObtained +=  (sender, e) =>
            {
                var loginResult=TryLoginImpl(e.PinCode, token);
                if (loginResult)
                {
                    LoggedIn = true;
                    TrySerialize(_serializationFilePath);
                    RaiseLoginCompleted(new OperationResultEventArgs(true,null));
                }
                else
                {
                    RaiseLoginCompleted(new OperationResultEventArgs(false,new Exception(message:"Login Failed")));
                }
            };

        }
        
        public void Logout()
        {
            LoggedIn = false;
            Token = null;
            TokenSecret = null;
            Service = new TwitterService(consumerKey:ConsumerKey,consumerSecret:ConsumerSecret);
            
        }
        #region Serialization
        private bool TrySerialize(string filePath)
        {
            BinaryFormatter bf = new BinaryFormatter();
            if (!File.Exists(filePath))
                File.Create(filePath);
            using (var stream = new FileStream(filePath, FileMode.Truncate))
            {
                try
                {
                    var creds = new OAuthCredentials(token: Token, tokenSecret: TokenSecret, consumerKey: ConsumerKey,
                        consumerSecret: ConsumerSecret);

                    bf.Serialize(stream, creds);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        private bool TryDeserialize(string filePath)
        {
            BinaryFormatter bf=new BinaryFormatter();
            if (!File.Exists(filePath) || File.ReadAllBytes(filePath).Length==0)
                return false;
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                OAuthCredentials obj;
                try
                {
                    obj = bf.Deserialize(stream) as OAuthCredentials;
                    if (obj == null)
                        throw new SerializationException(
                            $"file at {filePath} can`t be deserialized properly");
                }
                catch (Exception)
                {
                    return false;
                }
                object nullReference = null;
                if (nullReference.IsOneOf(obj.ConsumerKey, obj.ConsumerSecret, obj.TokenSecret, obj.Token))
                {
                    this.LoggedIn = false;
                    return false;
                }
                this.ConsumerKey = obj.ConsumerKey;
                this.ConsumerSecret = obj.ConsumerSecret;
                this.Token = obj.Token;
                this.TokenSecret = obj.TokenSecret;

                if(this.Service==null)
                    this.Service=new TwitterService();

                this.Service?.AuthenticateWith(ConsumerKey,ConsumerSecret,Token,TokenSecret);
                this.LoggedIn = true;
                return true;
                
            }
        }
        #endregion
        private bool TryLoginImpl(string pin, OAuthRequestToken token)
        {
            OAuthAccessToken access = Service.GetAccessToken(token, pin);
           
            if (access.UserId == 0 && access.Token == "?" && access.TokenSecret == "?")
                return false;
            Service.AuthenticateWith(access.Token, access.TokenSecret);
            Token = access.Token;
            TokenSecret = access.TokenSecret;
            return true;
        }
        #region Event Raisers
        void RaiseLoginCompleted(OperationResultEventArgs e)
        {
            LoginCompleted?.Invoke(this, e);
        }

        void RaiseLoginCompleted(bool success, Exception e = null)
        {
            var eArgs=new OperationResultEventArgs(success,e);
            LoginCompleted?.Invoke(this,eArgs);
        }
#endregion
    }
}

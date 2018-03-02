using System;
using TwitterDemo.Code.Authentification;
using TwitterDemo.Code.Authentification.EventArgs;
using TwitterDemo.Code.Posters;
using TwitterDemo.Code.Timers;

namespace TwitterDemo.Code.PostWrappers
{
    public abstract class PostWrapper
    {
        public event EventHandler<OperationResultEventArgs> PostingFinished;
        protected Poster poster;
        protected IAuthCredentials authCredentials;
        protected PostDelayTimer postDelayTimer;
        public Post Post { get; set; }
        public Poster Poster => poster;

        protected IAuthCredentials AuthCredentials => authCredentials;
        public abstract void TryPost();

        protected virtual void RaisePostingFinished(OperationResultEventArgs e)
        {
            PostingFinished?.Invoke(this, e);
        }
        protected virtual void RaisePostingFinished(bool success,Exception e=null)
        {
            var eArgs = new OperationResultEventArgs(success, e);
            PostingFinished?.Invoke(this, eArgs);
        }
    }
}

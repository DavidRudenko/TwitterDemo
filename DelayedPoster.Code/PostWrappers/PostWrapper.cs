using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelayedPoster.Code.Authentification;
using DelayedPoster.Code.Authentification.EventArgs;
using DelayedPoster.Code.Posters;
using DelayedPoster.Code.Timers;

namespace DelayedPoster.Code.PostWrappers
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
    }
}

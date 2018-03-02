using System;
using DelayedPoster.Code.Authentification;
using DelayedPoster.Code.WebWorkers;

namespace TwitterDemo.Code.Posters
{
    public class TwitterPoster : Poster
    {
        private TwitterWebWorker _webWorker;
        private readonly TwitterAuthCredentials _authCredentials;
        
        public TwitterPoster(TwitterAuthCredentials authCredentials)
        {
            if (authCredentials == null)
                throw new ArgumentNullException(nameof(authCredentials));
            _authCredentials = authCredentials;
            _webWorker = new TwitterWebWorker(_authCredentials);
        }

        public override void TryPost(Post post)
        {
            try
            {
                TryPostImpl(post);
                RaisePostingCompleted(true);
            }
            catch (Exception args)
            {
                RaisePostingCompleted(false, args);
            }
        }
        private  void TryPostImpl(Post post)
        {
            _webWorker.Post(post.Text,post.Attachements);
        }
    }
}
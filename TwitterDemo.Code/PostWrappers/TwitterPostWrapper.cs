using System;
using System.Collections.Generic;
using DelayedPoster.Code.Authentification;
using TwitterDemo.Code.Authentification;
using TwitterDemo.Code.Posters;
using TwitterDemo.Code.Timers;

namespace TwitterDemo.Code.PostWrappers
{
    public class TwitterPostWrapper:PostWrapper
    {

        public TwitterPostWrapper( string title,  string text,
             List<Attachement> attachements,  Poster poster,TimeSpan delay,IAuthCredentials authCredentials)
        {
            if (title == null)
                RaisePostingFinished(false, new ArgumentNullException(nameof(title)));
            if (text == null)
                RaisePostingFinished(false, new ArgumentNullException(nameof(text)));
            if (attachements == null)
                RaisePostingFinished(false, new ArgumentNullException(nameof(attachements)));
            if (poster == null)
                RaisePostingFinished(false, new ArgumentNullException(nameof(poster)));
            if (authCredentials == null)
                RaisePostingFinished(false, new ArgumentNullException(nameof(authCredentials)));
            this.poster = poster;
            this.authCredentials = authCredentials;
            this.Post=new Post(title,text,attachements);
            this.postDelayTimer=new PostDelayTimer(delay,TryPost);
        }

        public TwitterPostWrapper(Post post,TimeSpan delay,TwitterAuthCredentials credentials)
        {
            
            if (post == null)
                RaisePostingFinished(false, new ArgumentNullException(nameof(post)));
            if (credentials == null)
                RaisePostingFinished(false, new ArgumentNullException(nameof(credentials)));
            this.Post = post;
            this.authCredentials = credentials;
            this.postDelayTimer = new PostDelayTimer(delay, () => TryPostImpl());
            this.poster=new TwitterPoster(credentials);
        }
        public override void TryPost()
        {
            postDelayTimer.StartTimer();   
        }
        private void TryPostImpl()
        {
            poster.TryPost(Post);
            poster.PostingCompleted += (sender, e) => RaisePostingFinished(e);
        }
    }
}

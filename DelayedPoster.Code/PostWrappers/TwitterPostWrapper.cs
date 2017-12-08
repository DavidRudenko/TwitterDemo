using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelayedPoster.Code.Annotations;
using DelayedPoster.Code.Authentification;
using DelayedPoster.Code.Authentification.EventArgs;
using DelayedPoster.Code.Posters;
using DelayedPoster.Code.Timers;

namespace DelayedPoster.Code.PostWrappers
{
    //TODO:Add sync for events
    public class TwitterPostWrapper:PostWrapper
    {

        public TwitterPostWrapper( string title,  string text,
             List<Attachement> attachements,  Poster poster,TimeSpan delay,IAuthCredentials authCredentials)
        {
            if (title == null)
                throw new ArgumentNullException(nameof(title));
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            if (attachements == null)
                throw new ArgumentNullException(nameof(attachements));
            if (poster == null)
                throw new ArgumentNullException(nameof(poster));
            if (authCredentials == null)
                throw new ArgumentNullException(nameof(authCredentials));
            this.poster = poster;
            this.authCredentials = authCredentials;
            this.Post=new Post(title,text,attachements);
            this.postDelayTimer=new PostDelayTimer(delay,TryPost);
        }

        public TwitterPostWrapper(Post post,Poster poster,TimeSpan delay,IAuthCredentials credentials)
        {
            if (post == null)
                throw new ArgumentNullException(nameof(post));
            if (poster == null)
                throw new ArgumentNullException(nameof(poster));
            if (credentials == null)
                throw new ArgumentNullException(nameof(credentials));
            this.Post = post;
            this.poster = poster;
            this.authCredentials = credentials;
            this.postDelayTimer=new PostDelayTimer(delay,TryPost);
        }

        public override void TryPost()
        {
            authCredentials.TryLogin();
            authCredentials.LoginCompleted += (sender, e) =>
            {
                if (e.Result)
                {
                    poster.TryPost(Post);
                    poster.PostingCompleted += (sndr, arg) =>
                    {
                       RaisePostingFinished(e);
                    };
                }
                else
                {
                    RaisePostingFinished(new OperationResultEventArgs(false));
                }
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelayedPoster.Code.Annotations;
using DelayedPoster.Code.Authentification;
using DelayedPoster.Code.Authentification.EventArgs;
using DelayedPoster.Code.Posters;

namespace DelayedPoster.Code.PostWrappers
{
    public class TestPostWrapper:PostWrapper
    {
        private TimeSpan _delay;
        private IAuthCredentials _authCredentials;
        private Post _post;
        private Poster _poster;

        public Post Post
        {
            get { return _post; }
            set { _post = value; }
        }


        public Poster Poster
        {
            get { return _poster; }
            set { _poster = value; }
        }
        
        public TestPostWrapper( Post post,  Poster poster, TimeSpan delay,  IAuthCredentials credentials)
        {
            if (post == null) throw new ArgumentNullException(nameof(post));
            if (poster == null) throw new ArgumentNullException(nameof(poster));
            if (credentials == null) throw new ArgumentNullException(nameof(credentials));
            this._delay = delay;
            this._post = post;
            this._poster = poster;
            this._authCredentials = credentials;

        }

        public TestPostWrapper(string header, string text, List<Attachement> attachements,
            Poster poster, TimeSpan delay, IAuthCredentials credentials)
        {
            if (header == null) throw new ArgumentNullException(nameof(header));
            if (text == null) throw new ArgumentNullException(nameof(text));
            if (attachements == null) throw new ArgumentNullException(nameof(attachements));
            if (poster == null) throw new ArgumentNullException(nameof(poster));
            if (credentials == null) throw new ArgumentNullException(nameof(credentials));
            this._delay = delay;
            this._post = new Post(header, text, attachements);
            this._poster = poster;
            this._authCredentials = credentials;
        }

        public override void TryPost()
        {
            RaisePostingFinished(new OperationResultEventArgs(false));
        }
    }
}

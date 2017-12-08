using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelayedPoster.Code.Annotations;
using DelayedPoster.Code.Authentification;
using DelayedPoster.Code.Authentification.EventArgs;
using TweetSharp;

namespace DelayedPoster.Code.Posters
{
    //TODO:Implement posting
    public class TwitterPoster : Poster
    {

        private readonly TwitterAuthCredentials _authCredentials;

        public TwitterPoster(TwitterAuthCredentials authCredentials)
        {
            if (authCredentials == null)
                throw new ArgumentNullException(nameof(authCredentials));
            _authCredentials = authCredentials;
        }

        public override void TryPost(Post post)
        {
            _authCredentials.TryLogin();
            _authCredentials.LoginCompleted += (sender, e) =>
            {
                if (e.Result)
                {
                    if (post.Attachements.Count == 0)
                    {

                        var tweetOptions = new SendTweetOptions() {Status = post.Header};
                        _authCredentials.Service.SendTweet(tweetOptions,HandlePostResult);
                    }
                    else
                    {
                        Dictionary<string, Stream> streams = new Dictionary<string, Stream>();
                        foreach (var fInfo in post.Attachements)
                        {
                            streams.Add(fInfo.File.FullName, new FileStream(fInfo.File.FullName, FileMode.Open));
                        }
                        var tweetOptions = new SendTweetWithMediaOptions() {Images = streams};
                        _authCredentials.Service.SendTweetWithMedia(tweetOptions,HandlePostResult);
                    }
                }
            };
        }
        //TODO:Write exception handling
        private void HandlePostResult(TwitterStatus status, TwitterResponse response)
        {
            if(response.Error==null)
                RaisePostingCompleted(new OperationResultEventArgs(true));
            RaisePostingCompleted(new OperationResultEventArgs(false));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelayedPoster.Code.Authentification.AppCredentials
{
    /// <summary>
    /// Application credentials
    /// </summary>
    public class TwitterAppCredentials
    {
        public string ConsumerKey { get; }

        public string ConsumerKeySecret { get; }

        public TwitterAppCredentials(string consumerKey, string consumerKeySecret)
        {
            ConsumerKey = consumerKey;
            ConsumerKeySecret = consumerKeySecret;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelayedPoster.Code.Authentification.AppCredentials;

namespace DelayedPoster.Code.Authentification.AppCredentialsObtainers
{
    public class TwitterAppCredentialsObtainer:ITwitterAppCredentialsObtainer
    {
        //TODO:Get credentials from file 
        public TwitterAppCredentials GetAppCredentials()
        {
            return new TwitterAppCredentials("gexlyiD7nNKTiX6TxdlG4BFIm",
                "iSWxET6kO70tt2p9pvuf2RLwl13mNTUoU0mbWP5ERfpWrclKxA");
        }
    }
}

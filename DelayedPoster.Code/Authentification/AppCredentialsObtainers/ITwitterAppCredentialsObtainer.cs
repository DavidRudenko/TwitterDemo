using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelayedPoster.Code.Authentification.AppCredentials;

namespace DelayedPoster.Code.Authentification.AppCredentialsObtainers
{
    //TODO:cleanup the code(code review) and create a git repo when done with the twitter production code
    //TODO:Write production code for twitter and tests for it
    public interface ITwitterAppCredentialsObtainer
    {
        TwitterAppCredentials GetAppCredentials();
    }
}

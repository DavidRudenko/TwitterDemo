using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DelayedPoster.Code.Authentification.EventArgs;
using DelayedPoster.Code.Timers;

namespace DelayedPoster.Code.Posters
{
    public class TestPoster:Poster
    {
        public override void TryPost(Post post)
        {
            RaisePostingCompleted(new OperationResultEventArgs(true));
        }

    }
}

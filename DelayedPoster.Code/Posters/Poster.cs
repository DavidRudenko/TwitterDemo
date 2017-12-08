using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelayedPoster.Code.Authentification.EventArgs;

namespace DelayedPoster.Code.Posters
{
    public  abstract class Poster
    {
        public event EventHandler<OperationResultEventArgs> PostingCompleted; 

       public abstract void TryPost(Post post);

        protected virtual void RaisePostingCompleted(OperationResultEventArgs e)
        {
            PostingCompleted?.Invoke(this, e);
        }
    }
}

using System;
using TwitterDemo.Code.Authentification.EventArgs;

namespace TwitterDemo.Code.Posters
{
    public  abstract class Poster
    {
        public event EventHandler<OperationResultEventArgs> PostingCompleted; 

       public abstract void TryPost(Post post);

        protected virtual void RaisePostingCompleted(OperationResultEventArgs e)
        {
            PostingCompleted?.Invoke(this, e);
        }

        protected virtual void RaisePostingCompleted(bool successFlag, Exception e=null)
        {
            var args=new OperationResultEventArgs(successFlag,e);
            PostingCompleted?.Invoke(this,args);
        }
    }
}

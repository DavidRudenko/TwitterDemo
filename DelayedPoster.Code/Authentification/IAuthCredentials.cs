using DelayedPoster.Code.Authentification.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelayedPoster.Code.Authentification
{
    

    public interface IAuthCredentials
    {
         event EventHandler<OperationResultEventArgs> LoginCompleted;

        void TryLogin();
    }
}

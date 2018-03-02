using System;
using TwitterDemo.Code.Authentification.EventArgs;

namespace TwitterDemo.Code.Authentification
{
    

    public interface IAuthCredentials
    {
         event EventHandler<OperationResultEventArgs> LoginCompleted;

        void TryLogin();
    }
}

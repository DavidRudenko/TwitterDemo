using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelayedPoster.Code.Annotations;
using DelayedPoster.Code.Authentification.EventArgs;

namespace DelayedPoster.Code.Authentification
{
    /// <summary>
    /// Valid only with ValidLogin login and ValidPassword password
    /// </summary>
    public class TestAuthCredentials:IAuthCredentials
    {
        public event EventHandler<OperationResultEventArgs> LoginCompleted;
        private string _login;
        private string _password;

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public TestAuthCredentials( string login,  String password)
        {
            if (login == null) throw new ArgumentNullException(nameof(login));
            if (password == null) throw new ArgumentNullException(nameof(password));
            this._login = login;
            this._password = password;
        }

        public void TryLogin()
        {
            if (this._login == "ValidLogin" && this._password == "ValidPassword")
                RaiseLoginCompleted(new OperationResultEventArgs(true));
            RaiseLoginCompleted(new OperationResultEventArgs(false));
        }

        protected virtual void RaiseLoginCompleted(OperationResultEventArgs e)
        {
            LoginCompleted?.Invoke(this, e);
        }
    }
}

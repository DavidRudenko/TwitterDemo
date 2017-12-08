using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelayedPoster.Code.Authentification;
using DelayedPoster.Code.Authentification.AppCredentialsObtainers;
using DelayedPoster.Code.Authentification.PinObtainers;
using NUnit.Framework;

namespace DelayedPoster.Tests
{
    [TestFixture]
    class TwitterAuthCredentialsTesting
    {
        [Test]
        public void Twitter_TryingToLogInWithInvalidCredentials_ReturnsFalse()
        {
            TwitterAuthCredentials authCredentials=new TwitterAuthCredentials(new WrongPinObtainer("somePin"),
                new TwitterAppCredentialsObtainer());
            authCredentials.TryLogin();
            authCredentials.LoginCompleted += (sender, e) => { Assert.AreEqual(false, e.Result); };
        }
        [Test]
        public void Twitter_TryingToLogInWithValidCredentials_ReturnsTrue()
        {
            TwitterAuthCredentials authCredentials = new TwitterAuthCredentials(new TwitterPinObtainer(),
                new TwitterAppCredentialsObtainer());
            authCredentials.TryLogin();
            authCredentials.LoginCompleted += (sender, e) => { Assert.AreEqual(true, e.Result); };
        }

    }
}

using System;
using System.Collections.Generic;
using DelayedPoster.Code;
using DelayedPoster.Code.Annotations;
using DelayedPoster.Code.Authentification;
using DelayedPoster.Code.Authentification.AppCredentialsObtainers;
using DelayedPoster.Code.Authentification.PinObtainers;
using DelayedPoster.Code.Posters;
using DelayedPoster.Code.PostWrappers;
using NUnit.Framework;

namespace DelayedPoster.Tests
{
    [TestFixture]
    public class TwitterPostWrapperTesting
    {
        [Test]
        public void CreatingPostWrapperWithNullPost_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TwitterPostWrapper(null,new TestPoster(), TimeSpan.Zero,new TestAuthCredentials("login","password")));
        }
        [Test]
        public void CreatingPostWrapperWithNullPoster_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TwitterPostWrapper(new Post("header", "text"), null, TimeSpan.Zero, new TestAuthCredentials("login","password")));
        }
        [Test]
        public void CreatingPostWrapperWithNullCredentials_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TwitterPostWrapper(new Post("header", "text"), new TestPoster(), TimeSpan.Zero, null));
        }
        [Test]
        public void CreatingPostWrapperWithNullHeader_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TwitterPostWrapper(null, "text", new List<Attachement>(),
                new TestPoster(), TimeSpan.Zero,
                new TestAuthCredentials("login","password")));
        }

        [Test]
        public void CreatingPostWrapperWithNullText_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TwitterPostWrapper("header", null, new List<Attachement>(),
                new TwitterPoster(), TimeSpan.Zero,
                new TwitterAuthCredentials(new TwitterPinObtainer(), new TwitterAppCredentialsObtainer())));
        }
        [Test]
        public void CreatingPostWrapperWithNullAttachements_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TwitterPostWrapper("header", "text", null,
                new TestPoster(), TimeSpan.Zero,
                new TwitterAuthCredentials(new TwitterPinObtainer(),new TwitterAppCredentialsObtainer())));
        }

        [Test]
        public void TryingToPostWithInvalidCredentials_ReturnsFalse()
        {
            var pw = new TwitterPostWrapper("header", "text", new List<Attachement>(),
                new TestPoster(), TimeSpan.Zero,
                new TwitterAuthCredentials(new WrongPinObtainer("wrongPin"),new TwitterAppCredentialsObtainer() ));
            pw.PostingFinished += (sender, e) => Assert.AreEqual(false, e.Result);
        }

        [Test]
        public void TryingToPostWithValidCredentials_ReturnsTrue()
        {
            var pw = new TwitterPostWrapper("header", "text", new List<Attachement>(), 
                new TestPoster(), TimeSpan.Zero,
                new TwitterAuthCredentials(new TwitterPinObtainer(),new TwitterAppCredentialsObtainer()));
            pw.PostingFinished += (sender, e) => Assert.AreEqual(true, e.Result);
        }
    }
}

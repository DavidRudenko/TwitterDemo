using DelayedPoster.Code;
using DelayedPoster.Code.Posters;
using NUnit.Framework;

namespace DelayedPoster.Tests
{
    [TestFixture]
    public class TwitterPosterTesting
    {
        [Test]
        public void TryingToPostNull_ReturnsFalse()
        {
           Assert.AreEqual(false,new TwitterPoster().TryPost(null));
        }

        [Test]
        public void TryingToPostValidPost_ReturnsTrue()
        {
            Assert.AreEqual(true,new TwitterPoster().TryPost(new Post("header","text")));
        }

    }
}
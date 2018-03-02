using System;
using DelayedPoster.Code;
using NUnit.Framework;

namespace DelayedPoster.Tests
{
    [TestFixture]
    public class TwitterPostTesting
    {
        const string STUB_STRING = "somestring";
        [Test]
        public void CreatingPostWithNullHeader_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Post(null, STUB_STRING));
        }
        [Test]
        public void CreatingPostWithNullText_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Post(STUB_STRING, null));
        }

        public void CreatingPostWithNullAttachements_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(()=>new Post(STUB_STRING,STUB_STRING,null));
        }
    }
}
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using NUnit.Framework;
using DelayedPoster.Code.Loggers;
using DelayedPoster.Code;
using NUnit.Framework.Internal;
using DelayedPoster.Code.Timers;

namespace DelayedPoster.Tests
{

    [TestFixture]
    public class AttachementTesting
    {
        [Test]
        public void CreatingAttachementWithNullPath_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Attachement(null));
        }
        [Test]
        public void CreatingAttachementWithInvalidPath_ThrowsFileNotFoundException()
        {
            Assert.Throws<FileNotFoundException>(() => new Attachement("somepath"));
        }

        [Test]
        public void CreatingAttachementWitValidPath_NoExceptions()
        {
            Assert.DoesNotThrow(()=>new Attachement(@"C:\Users\david\Downloads\saint-lazare-gare-normandy-train.jpg"));
        }
    }
}
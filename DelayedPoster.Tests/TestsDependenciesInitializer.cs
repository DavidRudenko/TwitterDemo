using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOCContainer;
using DelayedPoster.Code;
using DelayedPoster.Code.Posters;
using DelayedPoster.Code.PostWrappers;
using DelayedPoster.Code.Timers;

namespace DelayedPoster.Tests
{
    static class TestsDependenciesInitializer
    {
        public static TSpecific Resolve<TSpecific>()
        {
            return (TSpecific) IocContainer.Resolve<TSpecific>();
        }

        static TestsDependenciesInitializer()
        {
            IocContainer.BindToFactory<Poster,TestPoster>();
            IocContainer.BindToFactory<IPostDelayTimer,PostDelayTimer>();
            IocContainer.BindToFactory<PostWrapper,TestPostWrapper>();
        }
    }
}

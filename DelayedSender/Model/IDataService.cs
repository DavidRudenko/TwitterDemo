using System;
using System.Collections.Generic;
using System.Text;

namespace DelayedPoster.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}

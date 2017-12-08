using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelayedPoster.Code.Authentification.EventArgs
{
    public class OperationResultEventArgs:System.EventArgs
    {
        public bool Result { get; }

        public OperationResultEventArgs(bool result)
        {
            Result = result;
        }
    }
}

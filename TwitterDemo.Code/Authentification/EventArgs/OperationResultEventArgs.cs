using System;

namespace TwitterDemo.Code.Authentification.EventArgs
{
    public class OperationResultEventArgs:System.EventArgs
    {
        public Exception Exception { get; }
        public bool Result { get; }

        public OperationResultEventArgs(bool result,Exception e)
        {
            Result = result;
            Exception = e;
        }
    }
}

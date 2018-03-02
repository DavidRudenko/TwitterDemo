using System;
using TwitterDemo.Code.Authentification.EventArgs;

namespace TwitterDemo.Code.Authentification.PinObtainers
{
    public interface IPinObtainer
    {
        event EventHandler<PinCodeEventArgs> PinCodeObtained; 
        void GetPin(string url);
    }
}
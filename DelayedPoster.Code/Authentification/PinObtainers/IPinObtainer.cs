using System;
using DelayedPoster.Code.Authentification.EventArgs;

namespace DelayedPoster.Code.Authentification.PinObtainers
{
    public interface IPinObtainer
    {
        event EventHandler<PinCodeEventArgs> PinCodeObtained; 
        void GetPin(string url);
    }
}
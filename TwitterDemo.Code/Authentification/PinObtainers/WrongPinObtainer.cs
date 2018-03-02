using System;
using TwitterDemo.Code.Authentification.EventArgs;

namespace TwitterDemo.Code.Authentification.PinObtainers
{
    /// <summary>
    /// for testing purposes only
    /// </summary>
    public class WrongPinObtainer:IPinObtainer
    {
        private readonly string _wrongPin;

        /// <param name="wrongPin">Pincode to be returned in the PinCodeObtained event args.</param>
        public WrongPinObtainer(string wrongPin)
        {
            _wrongPin = wrongPin;
        }
        public event EventHandler<PinCodeEventArgs> PinCodeObtained;
        public void GetPin(string url)
        {
            RaisePinCodeObtained(new PinCodeEventArgs(_wrongPin));
        }

        protected virtual void RaisePinCodeObtained(PinCodeEventArgs e)
        {
            PinCodeObtained?.Invoke(this, e);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelayedPoster.Code.Authentification.EventArgs;
using DelayedPoster.Code.Authentification.PinObtainers;

namespace DelayedPoster.Code.Authentification.PinObtainers
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

namespace DelayedPoster.Code.Authentification.EventArgs
{
    public class PinCodeEventArgs
    {
        public string PinCode { get; }

        public PinCodeEventArgs(string pincode)
        {
            PinCode = pincode;
        }
    }
}
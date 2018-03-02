using System;
using System.Diagnostics;
using System.Windows;
using TwitterDemo.Code.Authentification.EventArgs;

namespace TwitterDemo.Code.Authentification.PinObtainers
{
   
    public class TwitterPinObtainer : IPinObtainer
    {
        public event EventHandler<PinCodeEventArgs> PinCodeObtained;

        public void GetPin(string url)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Process.Start(url);
                TwitterLoginWindow window = new TwitterLoginWindow();
                window.Show();
                window.PinCodeEntered += (sender, e) =>
                {
                    RaisePinCodeObtained(e);
                    window.Close();
                };
            });

        }

        protected virtual void RaisePinCodeObtained(PinCodeEventArgs e)
        {
            PinCodeObtained?.Invoke(this, e);
        }
    }
}
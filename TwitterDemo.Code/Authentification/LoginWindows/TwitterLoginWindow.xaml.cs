using System;
using System.Windows;
using TwitterDemo.Code.Authentification.EventArgs;

namespace TwitterDemo.Code
{
    /// <summary>
    /// Description for TwitterLoginWindow.
    /// </summary>
    public partial class TwitterLoginWindow : Window
    {
        public event EventHandler<PinCodeEventArgs> PinCodeEntered;
        /// <summary>
        /// Initializes a new instance of the TwitterLoginWindow class.
        /// </summary>
        public TwitterLoginWindow()
        {
            InitializeComponent();
        }

        private void SubmitPincode_Button_OnClick(object sender, RoutedEventArgs e)
        {
            RaisePinCodeEntered(new PinCodeEventArgs(PinBox.Text));
        }

        protected virtual void RaisePinCodeEntered(PinCodeEventArgs e)
        {
            PinCodeEntered?.Invoke(this, e);
        }
    }
}
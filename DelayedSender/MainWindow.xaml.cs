using System.Windows;
using DelayedPoster.Code.Authentification;
using DelayedPoster.Code.Authentification.AppCredentialsObtainers;
using DelayedPoster.Code.Authentification.PinObtainers;
using DelayedPoster.ViewModel;
namespace DelayedPoster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            var creds = new TwitterAuthCredentials(new TwitterPinObtainer(), new TwitterAppCredentialsObtainer());
            creds.TryLogin();
            creds.LoginCompleted += (sender, e) =>
                {
                    MessageBox.Show($"Login Completed.The result is {e.Result}");
                };
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }
    }
}
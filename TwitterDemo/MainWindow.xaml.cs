using System.Windows;
using TwitterDemo.Code.Authentification;
using TwitterDemo.Code.Authentification.AppCredentialsObtainers;
using TwitterDemo.Code.Authentification.PinObtainers;
using TwitterDemo.ViewModel;

namespace TwitterDemo
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
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }
    }
}
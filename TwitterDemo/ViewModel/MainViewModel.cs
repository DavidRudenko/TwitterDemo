using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using DelayedPoster.Code.Authentification;
using DelayedPoster.Code.Authentification.AppCredentialsObtainers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using TweetSharp;
using TwitterDemo.Code;
using TwitterDemo.Code.Authentification;
using TwitterDemo.Code.Authentification.AppCredentialsObtainers;
using TwitterDemo.Code.Authentification.PinObtainers;
using TwitterDemo.Code.Posters;
using TwitterDemo.Code.PostWrappers;

namespace TwitterDemo.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
      
        private List<TwitterPostWrapper> _wrappers=new List<TwitterPostWrapper>();
        public RelayCommand AddWrapperCommand { get; set; }
        public RelayCommand AddAttachementCommand { get; set; }
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand LogoutCommand { get; set; }
        public RelayCommand<string> RemoveAttachementCommand { get; set; }
        private string _tweetText;

        public TwitterUser LoggedInUser     
        {
            get { return _loggedInUser; }
            set {Set(ref _loggedInUser,value); }
        }

        #region Private Fields

        private TwitterAuthCredentials _authCreds = new TwitterAuthCredentials(new TwitterPinObtainer(),
            new TwitterAppCredentialsObtainer());
        private ObservableCollection<Attachement> _attachements;
        private TimeSpan _delay;
        private TwitterUser _loggedInUser;

        #endregion

        #region Binding Properties

        public string TweetText
        {
            get { return _tweetText; }
            set { Set(ref _tweetText, value); }
        }

        public TimeSpan Delay
        {
            get { return _delay; }
            set { Set(ref _delay, value); }
        }

        public ObservableCollection<Attachement> Attachements
        {
            get { return _attachements; }
            set
            {
                Set(ref _attachements, value);
            }
        }

        #endregion

        public  MainViewModel()
        {
            this.AddAttachementCommand = new RelayCommand(AddAttachement);
            this.AddWrapperCommand=new RelayCommand(AddWrapper);
            this.RemoveAttachementCommand = new RelayCommand<string>(RemoveAttachement);
            this.LogoutCommand=new RelayCommand(Logout);
            this.LoginCommand = new RelayCommand(Login);
            this._attachements=new ObservableCollection<Attachement>();
            var beginGetUserResult = _authCreds.BeginGetUser();
            this.LoggedInUser = _authCreds.EndGetUser(beginGetUserResult);
            Attachements.CollectionChanged += (sender, e) => RaisePropertyChanged(nameof(Attachements));
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}

        #region Commands Callbacks
        private void RemoveAttachement(string param)
        {
            var attachement = new Attachement(param);
            _attachements.Remove(attachement);
        }
        private void AddAttachement()
        {
            var fd = new OpenFileDialog {Title = "Choose The Attachement"};
            if (fd.ShowDialog() == true)
            {
                _attachements.Add(new Attachement(fd.FileName));
            }
        }

        private void AddWrapper()
        {
            Post post=null;
            try
            {
                 post = new Post(String.Empty, TweetText, _attachements.ToList());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            var poster = new TwitterPoster(_authCreds);
            var wrapper = new TwitterPostWrapper(post, TimeSpan.FromMilliseconds(1), _authCreds);
            

            wrapper.TryPost();//starts timer
            wrapper.PostingFinished += (sender, e) =>
            {
                if (e.Result) //posting was successfull
                {
                    MessageBox.Show("Tweet posted");
                }
                else
                {
                    MessageBox.Show(e.Exception.Message);//TODO:implement logging
                }
            };
        }

        private void Login()
        {
            
            _authCreds.TryLogin();
            _authCreds.LoginCompleted += (sender, e) =>
            {
                if (e.Result)
                {
                    var begin = _authCreds.BeginGetUser();
                    this.LoggedInUser = _authCreds.EndGetUser(begin);
                }
                //else
                //Handle the exception(e.Exception)
            };
        }

        private void Logout()
        {
            this.LoggedInUser = null;
            _authCreds.Logout();
        }

        #endregion
    }
}
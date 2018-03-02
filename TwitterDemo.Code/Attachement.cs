using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using TwitterDemo.Code.Annotations;

namespace TwitterDemo.Code
{
    public class Attachement:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private FileInfo _file;

        public FileInfo File
        {
            get { return _file; }
            set { _file = value; OnPropertyChanged(); }
        }

        public Attachement([NotNull] string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            _file = new FileInfo(path);
            if (!_file.Exists)
                throw new FileNotFoundException($"file {path} does not exist");
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override bool Equals(object obj)
        {
            var asAttachement = obj as Attachement;
            if (asAttachement == null)
                return false;
            return asAttachement.File.FullName.Equals(this.File.FullName);
        }

        public static bool operator ==(Attachement left, Attachement right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Attachement left, Attachement right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return _file.FullName;
        }
    }
}

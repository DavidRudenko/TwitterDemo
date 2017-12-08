using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DelayedPoster.Code.Annotations;
using DelayedPoster.Code.Loggers;

namespace DelayedPoster.Code
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
    }
}

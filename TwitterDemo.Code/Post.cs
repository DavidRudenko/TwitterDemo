using System;
using System.Collections.Generic;

namespace TwitterDemo.Code
{
    public class Post
    {
        private string _header;
        private string _text;
        private List<Attachement> _attachements;

        public List<Attachement> Attachements
        {
            get { return _attachements; }
            set { _attachements = value; }
        }

        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public Post( string header,  string text)
        {
            if (header == null) throw new ArgumentNullException(nameof(header));
            if (text == null) throw new ArgumentNullException(nameof(text));
            this._text = text;
            this._header = header;
            this._attachements=new List<Attachement>();
        }

        public Post(string header, string text,  List<Attachement> attachements):this(header,text)
        {
            if (attachements == null) throw new ArgumentNullException(nameof(attachements));
            this._attachements = attachements;
        }
    }
}

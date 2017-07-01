using System;
using System.Collections.Generic;
using System.Text;

namespace MediaBrowser.Library
{
    public class Album
    {
        public Album(string title, string artUri)
        {
            Title = title;
            AlbumArtUri = artUri;
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _albumArtUri;
        public string AlbumArtUri
        {
            get { return _albumArtUri; }
            set { _albumArtUri = value; }
        }
	
    }
}

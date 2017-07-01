//---------------------------------------------------------------------------
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Limited Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/limitedpermissivelicense.mspx
// All other rights reserved.
//
// This file is part of the 3D Tools for Windows Presentation Foundation
// project.  For more information, see:
// 
// http://CodePlex.com/Wiki/View.aspx?ProjectName=3DTools
//
//---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace PhotoBrowser.FlickrApi
{
    /// <summary>
    /// Class to represent a FlickrPhoto
    /// </summary>
    public class FlickrPhoto
    {
        public FlickrPhoto(string id, string server, string secret, string authToken)
        {
            _id = id;
            _server = server;
            _secret = secret;
            _comments = null;
            _authToken = authToken;

            SmallImage = null;
        }

        private BitmapImage _smallImageCache;
        public BitmapImage SmallImage
        {
            get { return _smallImageCache; }
            set { _smallImageCache = value; }
        }

        private string _authToken;
        public string AuthToken
        {
            get { return _authToken; }
        }

        public string URL_Small
        {
            get
            {
                return "http://static.flickr.com/" + Server + "/" + ID + "_" + Secret + "_t.jpg";
            }
        }

        public string URL_Medium
        {
            get
            {
                return "http://static.flickr.com/" + Server + "/" + ID + "_" + Secret + "_m.jpg";
            }
        }

        public string URL
        {
            get
            {
                return "http://static.flickr.com/" + Server + "/" + ID + "_" + Secret + ".jpg";
            }
        }

        private readonly string _id;
        public string ID
        {
            get { return _id; }
        }

        private readonly string _server;
        public string Server
        {
            get { return _server; }
        }

        private readonly string _secret;
        public string Secret
        {
            get { return _secret; }
        }

        private string _comments;
        public string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        private string _info;
        public string Info
        {
            get { return _info; }
            set { _info = value; }
        }
    }
}

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

namespace PhotoBrowser.FlickrApi
{
    /// <summary>
    /// Class representing the data for an authorized Flickr user
    /// </summary>
    public class AuthorizedFlickrUser
    {
        public AuthorizedFlickrUser()
        {
        }

        private string _nsid;
        public string NSID
        {
            get { return _nsid; }
            set { _nsid = value; }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _fullname;
        public string Fullname
        {
            get { return _fullname; }
            set { _fullname = value; }
        }
	
        private string _token;
        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }

        private string _permissions;
        public string Permissions
        {
            get { return _permissions; }
            set { _permissions = value; }
        }
    }
}

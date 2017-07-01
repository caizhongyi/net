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
    /// The URL to use when authorizing with Flickr
    /// </summary>
    internal class FlickrAuthorizationUrl : FlickrUrl
    {
        public FlickrAuthorizationUrl(string apiKey)
            : base(apiKey, "http://flickr.com/services/auth/")
        {
        }
    }
}

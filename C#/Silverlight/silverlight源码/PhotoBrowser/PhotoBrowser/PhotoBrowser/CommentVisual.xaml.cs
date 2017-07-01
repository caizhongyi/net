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
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace PhotoBrowser
{
	public partial class CommentVisual
	{
        public CommentVisual(string userid, 
                             string username, 
                             string commenttext)
		{
			this.InitializeComponent();

            // set the comment text and the username
            textBox.Text = commenttext;
            Label.Content = username;

            // go load the user's personal photo
            LoadUserPhoto(userid);
		}

        /// <summary>
        /// Function to load and then display the photo for the given user.
        /// </summary>
        /// <param name="userid">The user whose photo is to be displayed</param>
        private void LoadUserPhoto(string userid)
        {
            FlickrApi.Flickr.AsynchGetUserPhoto(
                userid,
                Dispatcher,
                delegate(object result)
                {
                    string url = (string)result;
                    BitmapImage b = new BitmapImage(new Uri(url));

                    if (b.IsDownloading == true)
                    {
                        b.DownloadCompleted += delegate(object sender, EventArgs e)
                        {
                            Image.Source = b;
                        };
                    }
                    else
                    {
                        Image.Source = b;
                    }
                });
        }
	}
}

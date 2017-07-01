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
using System.Xml;

namespace PhotoBrowser
{
	public partial class CommentList
	{
        public CommentList()
		{
			this.InitializeComponent();
		}

        /// <summary>
        /// Takes in an XML string that contains the list of comments that are to
        /// be displayed by the CommentList.
        /// </summary>
        /// <param name="comments">The comments</param>
        public void SetComments(string comments)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(new StringReader(comments));
            StackPanel.Children.Clear();

            foreach (XmlNode node in doc.SelectNodes("/comments/comment"))
            {
                string userid = node.Attributes["author"].Value;
                string authorname = node.Attributes["authorname"].Value;
                string comment = node.InnerText;

                StackPanel.Children.Add(new CommentVisual(userid, authorname, comment));
            }
        }
	}
}

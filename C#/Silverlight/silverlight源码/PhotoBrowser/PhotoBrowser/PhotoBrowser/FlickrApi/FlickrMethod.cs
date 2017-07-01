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
using System.Xml;
using System.Net;

namespace PhotoBrowser.FlickrApi
{
    /// <summary>
    /// Class that represents a Flickr method
    /// </summary>
    internal class FlickrMethod : FlickrUrl
    {
        /// <summary>
        /// Constructs an unsigned FlickrMethod
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="methodName"></param>
        public FlickrMethod(string apiKey, string methodName) : base(apiKey, "http://api.flickr.com/services/rest/")
        {
            AddParameter("method", methodName);
            _isSigned = false;
        }

        /// <summary>
        /// Constructs a signed FlickrMethod
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="methodName"></param>
        /// <param name="authToken"></param>
        /// <param name="secret"></param>
        public FlickrMethod(string apiKey, string methodName, string authToken, string secret)
            : this(apiKey, methodName)
        {
            _authToken = authToken;
            AddParameter("auth_token", authToken);
            _isSigned = true;
            _secret = secret;
        }
        
        /// <summary>
        /// Makes the given method call to Flickr
        /// </summary>
        /// <param name="rspNode"></param>
        /// <returns></returns>
        public bool MakeRequest(out XmlNode rspNode)
        {
            if (_isSigned)
            {
                return MakeSignedRequest(_secret, out rspNode);
            }
            else
            {
                return MakeUnsignedRequest(out rspNode);
            }
        }

        /// <summary>
        /// Makes a signed request to Flickr of the method this class represents
        /// </summary>
        /// <param name="sharedSecret"></param>
        /// <param name="rspNode"></param>
        /// <returns></returns>
        public bool MakeSignedRequest(string sharedSecret, out XmlNode rspNode)
        {
            // make the function call and create the xml doc that will parse the result
            WebRequest request = WebRequest.Create(GetSignedUrlString(sharedSecret));

            WebResponse response = request.GetResponse();

            XmlTextReader responseReader = new XmlTextReader(response.GetResponseStream());
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(responseReader);

            rspNode = xmlDoc.SelectSingleNode("rsp");
            return (rspNode.Attributes["stat"].InnerText == "ok");       
        }

        /// <summary>
        /// Makes an unsigned request to Flickr of the method this class represents
        /// </summary>
        /// <param name="rspNode"></param>
        /// <returns></returns>
        private bool MakeUnsignedRequest(out XmlNode rspNode)
        {
            // make the function call and create the xml doc that will parse the result
            WebRequest request = WebRequest.Create(GetUnsignedUrlString());

            WebResponse response = request.GetResponse();

            XmlTextReader responseReader = new XmlTextReader(response.GetResponseStream());
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(responseReader);

            rspNode = xmlDoc.SelectSingleNode("rsp");
            return (rspNode.Attributes["stat"].InnerText == "ok");
        }

        public string Secret
        {
            get { return _secret; }
        }

        public string AuthToken
        {
            get { return _authToken; }
        }

        /////////////////////////////////
        // private data
        /////////////////////////////////
       
        private bool _isSigned;
        private string _secret;
        private string _authToken;        
    }
}

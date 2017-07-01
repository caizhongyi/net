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

using System.Security.Cryptography;

namespace PhotoBrowser.FlickrApi
{
    /// <summary>
    /// Helps create the URLs used to make method calls to Flickr
    /// </summary>
    internal abstract class FlickrUrl
    {
        public FlickrUrl(string apiKey, string baseUrl)
        {
            _baseUrl = baseUrl;

            parameters = new SortedDictionary<string, string>();
            AddParameter("api_key", apiKey);
        }

        /// <summary>
        /// Adds a parameter to the URL
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        public void AddParameter(string paramName, string paramValue)
        {
            parameters[paramName] = paramValue;       
        }

        /// <summary>
        /// Computes a signature of the built up URL string
        /// </summary>
        /// <param name="sharedSecret"></param>
        /// <returns></returns>
        private string Sign(string sharedSecret)
        {
            string stringToSign = sharedSecret;

            foreach (string s in parameters.Keys)
            {
                stringToSign += (s + parameters[s]);
            }

            // create signature - in the end return a string for easier processing            
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(stringToSign));

            string signature = "";
            foreach (byte b in hash)
            {
                signature += b.ToString("X2");
            }

            return signature.ToLower();
        }

        /// <summary>
        /// Builds the actual URL based upon the given parameters and a signtaure if
        /// required.
        /// </summary>
        /// <param name="shouldSign"></param>
        /// <param name="sharedSecret"></param>
        /// <returns></returns>
        private string BuildUrl(bool shouldSign, string sharedSecret)
        {
            string finalString = BaseUrl;

            bool first = true;
            foreach (string paramName in parameters.Keys)
            {
                if (first)
                {
                    finalString += "?";
                    first = false;
                }
                else
                {
                    finalString += "&";
                }

                finalString += paramName + "=" + parameters[paramName];
            }

            if (shouldSign)
            {
                string signature = Sign(sharedSecret);
                finalString += "&api_sig=" + signature;
            }

            return finalString;
        }

        public string BaseUrl
        {
            get { return _baseUrl; }
        }
	
        /// <summary>
        /// Gets an unsigned version of the URL
        /// </summary>
        /// <returns></returns>
        public string GetUnsignedUrlString()
        {
            return BuildUrl(false, ""); 
        }

        /// <summary>
        /// Gets a signed version of the URL
        /// </summary>
        /// <param name="sharedSecret"></param>
        /// <returns></returns>
        public string GetSignedUrlString(string sharedSecret)
        {
            return BuildUrl(true, sharedSecret);
        }

        //---------------------------
        // Private data
        //---------------------------

        private SortedDictionary<string, string> parameters;
        private readonly string _baseUrl;        
    }
}

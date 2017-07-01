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
using System.Threading;
using System.Windows.Threading;

namespace PhotoBrowser.FlickrApi
{
    /// <summary>
    /// Class representing a collection of Flickr photographs
    /// </summary>
    public class FlickrPhotos
    {
        public delegate void PhotoReadyDelegate(FlickrPhoto photo, object o);

        /// <summary>
        /// Constructs a collection of Flickr photographs.  The collection is a result
        /// of the passed in method, and numPerPage represents how many should be 
        /// fetched at a time.
        /// </summary>
        /// <param name="numPerPage"></param>
        internal FlickrPhotos(FlickrMethod photoQuery, int numPerPage)
        {
            _photoQueryMethod = photoQuery;
            _photoListCache = new Dictionary<int, List<FlickrPhoto>>();
            _numPerPage = numPerPage;
            _count = Int32.MaxValue;
            _user = null;
        }
              
        internal FlickrPhotos(FlickrMethod method, int numPerPage, AuthorizedFlickrUser user)
            : this(method, numPerPage)
        {
            _user = user;
        }

        /// <summary>
        /// Actually loads in the range, from start to end, of Flickr photos, based upon the method
        /// used to populate the collection.  The DateTime is used so used so that if there was a 
        /// LoadRange request that occurred later in time than this one, that this request is ignored.
        /// This makes it so when scrolling through a list of photos, photos that are requested but
        /// scrolled past before seen aren't loaded.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="thisRequest"></param>
        /// <returns></returns>
        public bool LoadRange(int start, int end, DateTime thisRequest)
        {
            bool didLoad = false;

            bool earlyOut = false;
            lock (dateTimeHolder)
            {
                if (thisRequest > dateTimeHolder.lastRangeRequest)
                {
                    dateTimeHolder.lastRangeRequest = thisRequest;
                }
                else
                {
                    earlyOut = true;
                }
            }

            if (earlyOut) return didLoad;

            lock (this)
            {
                DateTime lastRequest;
                lock (dateTimeHolder)
                {
                    lastRequest = dateTimeHolder.lastRangeRequest;
                }

                if (thisRequest >= lastRequest)
                {
                    while (start <= end)
                    {
                        int page = start / NumPerPage + 1;

                        if (page > 0 && start >= 0)
                        {
                            if (!Cache.ContainsKey(page))
                            {
                                PhotoQuery.AddParameter("page", page.ToString());
                                PhotoQuery.AddParameter("per_page", NumPerPage.ToString());

                                List<FlickrPhoto> photos = null;
                                photos = Flickr.GetPhotos(PhotoQuery, ref _count);

                                if (photos != null)
                                {
                                    AddToCache(photos, page);
                                }
                            }
                        }

                        start += NumPerPage;
                    }

                    didLoad = true;
                }
                
            }

            return didLoad;
        }

        /// <summary>
        /// Adds the list of photos to the cache of photos.
        /// </summary>
        /// <param name="photos"></param>
        /// <param name="page"></param>
        private void AddToCache(List<FlickrPhoto> photos, int page)
        {
            // probably want a better revocation policy
            // revoke some things from the cache if necessary
            if (Cache.Keys.Count > 20)
            {
                Random r = new Random();
                foreach (int i in Cache.Keys)
                {
                    if (r.NextDouble() > 0.5)
                    {
                        Cache.Remove(i);
                        break;
                    }
                }
            }

            // cache the return value and return the needed photo
            Cache[page] = photos;
        }

        /// <summary>
        /// Gets the photo at the specified index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public FlickrPhoto GetPhoto(int index)
        {
            FlickrPhoto photo = null;
            
            lock (this)
            {
                // get the page index of the photos
                int page = index / NumPerPage + 1;
                List<FlickrPhoto> photos = null;

                if (Cache.ContainsKey(page))
                {
                    photos = Cache[page];
                }
                else
                {
                    PhotoQuery.AddParameter("page", page.ToString());
                    PhotoQuery.AddParameter("per_page", NumPerPage.ToString());                    

                    photos = Flickr.GetPhotos(PhotoQuery, ref _count);
                }

                // now get the actual photo
                if (index >= _count) return null;

                photo = photos[index % NumPerPage];

                AddToCache(photos, page);                
            }

            return photo;
        }

        /// <summary>
        /// The cache from which to store all the photos
        /// </summary>
        private Dictionary<int, List<FlickrPhoto>> Cache
        {
            get { return _photoListCache; }
        }
	
        /// <summary>
        /// The query used to retrieve the photos
        /// </summary>
        private FlickrMethod PhotoQuery
        {
            get { return _photoQueryMethod; }
        }

        public AuthorizedFlickrUser User
        {
            get { return _user; }
        }
	
        /// <summary>
        /// The number of photos to get per page
        /// </summary>
        public int NumPerPage
        {
            get { return _numPerPage; }
        }

        public int Count
        {
            get
            {
                return _count;
            }
        }

        public FlickrPhoto this[int i]
        {
            get
            {
                return GetPhoto(i);
            }
        }

        /// <summary>
        /// Class that simply wraps around a DateTime.  Made a class so that we can
        /// lock it.
        /// </summary>
        private class DateTimeHolder
        {
            public DateTime lastRangeRequest = DateTime.Now;
        }

        /////////////////////////////////////
        // Private Data
        /////////////////////////////////////
        private DateTimeHolder dateTimeHolder = new DateTimeHolder();

        private Dictionary<int, List<FlickrPhoto>> _photoListCache;
        private readonly FlickrMethod _photoQueryMethod;
        private AuthorizedFlickrUser _user;
        private readonly int _numPerPage;
        private int _count;                        
    }
}

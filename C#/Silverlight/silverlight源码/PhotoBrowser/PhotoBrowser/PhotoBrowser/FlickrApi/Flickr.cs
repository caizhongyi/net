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
using System.Net;
using System.Xml;
using System.Windows;
using System.Threading;
using System.Windows.Threading;

namespace PhotoBrowser.FlickrApi
{
    /// <summary>
    /// The class used to handle all requests to Flickr
    /// </summary>
    public class Flickr
    {
        static Flickr()
        {
            //
            // NOTE: To use the app you'll need to go request your own api key and shared secret
            //       from Flickr here:
            //
            //       http://www.flickr.com/services/api/keys/apply/
            //
            _apiKey = "";
            _sharedSecret = "";            
        }

        /// <summary>
        /// initialize the flickr worker threads
        /// </summary>
        static public void Initialize()
        {
            
            workerThreads = new Thread[NUM_WORKER_THREADS];
            workerThreadWorkQueue = new LinkedList<FlickrWorkDelegate>[NUM_WORKER_THREADS];

            for (int i = 0; i < workerThreads.Length; i++)
            {
                workerThreads[i] = new Thread(new ParameterizedThreadStart(FlickrWorker));
                workerThreads[i].Name = "Worker Thread: " + i;
                workerThreadWorkQueue[i] = new LinkedList<FlickrWorkDelegate>();

                workerThreads[i].Start(workerThreadWorkQueue[i]);
            }
        }

        /// <summary>
        /// Shuts down the worker threads
        /// </summary>
        static public void Shutdown()
        {
            for (int i = 0; i < workerThreads.Length; i++)
            {
                workerThreads[i].Abort();
            }
        }
        
        /// <summary>
        /// The threads that handle all of the asynchronous processing
        /// </summary>
        /// <param name="o"></param>
        internal static void FlickrWorker(object o)
        {
            LinkedList<FlickrWorkDelegate> workQueue = (LinkedList<FlickrWorkDelegate>)o;

            while (true)
            {
                try
                {
                    FlickrWorkDelegate delegateToExecute = null;

                    Monitor.Enter(workQueue);
                    {
                        // wait on someone to give us some work
                        if (workQueue.Count == 0)
                        {
                            Monitor.Wait(workQueue);
                        }

                        // the only way we get here is if there is work to be done - pull it from the queue and continue
                        delegateToExecute = workQueue.First.Value;
                        workQueue.RemoveFirst();
                    }
                    Monitor.Exit(workQueue);

                    // call the work function
                    delegateToExecute();
                }
                catch (ThreadAbortException)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Adds the given FlickrWorkDelegate to one of the work queues
        /// </summary>
        /// <param name="work"></param>
        private static void AddToWorkQueue(FlickrWorkDelegate work)
        {
            int min = int.MaxValue;
            LinkedList<FlickrWorkDelegate> minQueue = null;
            
            for (int i = 0; i < workerThreadWorkQueue.Length; i++)
            {
                if (min > workerThreadWorkQueue[i].Count)
                {
                    min = workerThreadWorkQueue[i].Count;
                    minQueue = workerThreadWorkQueue[i];
                }
            }

            if (work != null)
            {
                Monitor.Enter(minQueue);
                {
                    minQueue.AddLast(work);
                    Monitor.Pulse(minQueue);
                }
                Monitor.Exit(minQueue);
            }
        }

        /// <summary>
        /// Asynchronously gets the most recently updated photos for a given user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="notificationDispatcher"></param>
        /// <param name="notificationEvent"></param>
        public static void AsynchGetRecentlyUpdated(AuthorizedFlickrUser user,
                                                    Dispatcher notificationDispatcher,
                                                    FlickrWorkCompleteDelegate notificationEvent)
        {
            {
                AddToWorkQueue(delegate()
                               {
                                   FlickrPhotos photos = GetRecentlyUpdated(user);

                                   if (notificationDispatcher != null && notificationEvent != null)
                                   {
                                       notificationDispatcher.BeginInvoke(DispatcherPriority.SystemIdle,
                                                                          notificationEvent,
                                                                          photos);
                                   }
                               }
                               );
            }
        }

        /// <summary>
        /// Asyncronously searches for photos with the passed in query string
        /// </summary>
        /// <param name="query"></param>
        /// <param name="notificationDispatcher"></param>
        /// <param name="notificationEvent"></param>
        public static void AsynchSearchForPhotos(string query,                                            
                                                 Dispatcher notificationDispatcher,
                                                 FlickrWorkCompleteDelegate notificationEvent)
        {
            AddToWorkQueue(delegate()
                           {
                               FlickrPhotos photos = SearchForPhotos(query);
                               
                               if (notificationDispatcher != null && notificationEvent != null)
                               {
                                   notificationDispatcher.BeginInvoke(DispatcherPriority.SystemIdle,
                                                                      notificationEvent,
                                                                      photos);
                               }
                           }
                           );
        }

        /// <summary>
        /// Asynchronously searches for photo in the given latitude/longitude rectangle
        /// </summary>
        /// <param name="latLong"></param>
        /// <param name="notificationDispatcher"></param>
        /// <param name="notificationEvent"></param>
        public static void AsynchSearchForPhotos(Rect latLong,
                                                 Dispatcher notificationDispatcher,
                                                 FlickrWorkCompleteDelegate notificationEvent)
        {
            AddToWorkQueue(delegate()
                           {
                               FlickrPhotos photos = SearchForPhotos(latLong);

                               if (notificationDispatcher != null && notificationEvent != null)
                               {
                                   notificationDispatcher.BeginInvoke(DispatcherPriority.SystemIdle,
                                                                      notificationEvent,
                                                                      photos);
                               }
                           }
                           );
        }

        /// <summary>
        /// Loads the given range of Flickr photos asynchronously
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="photos"></param>
        /// <param name="notificationDispatcher"></param>
        /// <param name="notificationEvent"></param>
        public static void AsynchLoadPhotoRange(int start, int end, FlickrPhotos photos,
                                                Dispatcher notificationDispatcher,
                                                FlickrWorkCompleteDelegate notificationEvent)
        {
            DateTime requestTime = DateTime.Now;

            AddToWorkQueue(delegate()
                           {
                               if (photos.LoadRange(start, end, requestTime))
                               {
                                   if (notificationDispatcher != null && notificationEvent != null)
                                   {
                                       notificationDispatcher.BeginInvoke(DispatcherPriority.SystemIdle,
                                                                          notificationEvent,
                                                                          photos);
                                   }
                               }
                           }
                           );
        }

        /// <summary>
        /// Asynchronously gets the photo at the given index from the list of photos
        /// </summary>
        /// <param name="photos"></param>
        /// <param name="index"></param>
        /// <param name="notificationDispatcher"></param>
        /// <param name="notificationEvent"></param>
        public static void AsynchGetPhoto(FlickrPhotos photos,
                                          int index,
                                          Dispatcher notificationDispatcher,
                                          FlickrWorkCompleteDelegate notificationEvent)
        {
            AddToWorkQueue(delegate()
                           {
                               FlickrPhoto photo = photos.GetPhoto(index);

                               if (photo != null)
                               {
                                   string info = GetPhotoInfo(photo);
                                   photo.Info = info;

                                   if (notificationDispatcher != null && notificationEvent != null)
                                   {
                                       notificationDispatcher.BeginInvoke(DispatcherPriority.SystemIdle,
                                                                          notificationEvent,
                                                                          photo);
                                   }
                               }
                           }
                           );
        }

        /// <summary>
        /// Asynchronously gets the comments for the given photo.
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="notificationDispatcher"></param>
        /// <param name="notificationEvent"></param>
        public static void AsynchGetPhotoComments(FlickrPhoto photo,
                                                  Dispatcher notificationDispatcher,
                                                  FlickrWorkCompleteDelegate notificationEvent)
        {
            AddToWorkQueue(delegate()
                            {
                                string comments = GetPhotoComments(photo);
                                photo.Comments = comments;

                                if (notificationDispatcher != null && notificationEvent != null)
                                {
                                    notificationDispatcher.BeginInvoke(DispatcherPriority.SystemIdle,
                                                                       notificationEvent,
                                                                       comments);
                                }
                            }
                            );
        }

        /// <summary>
        /// Asynchronously gets the location for the given photo.
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="notificationDispatcher"></param>
        /// <param name="notificationEvent"></param>
        public static void AsynchGetPhotoLocation(FlickrPhoto photo,
                                                  Dispatcher notificationDispatcher,
                                                  FlickrWorkCompleteDelegate notificationEvent)
        {
            AddToWorkQueue(delegate()
                           {
                               string location = GetPhotoLocation(photo);
                               photo.Location = location;

                               if (notificationDispatcher != null && notificationEvent != null)
                               {
                                   notificationDispatcher.BeginInvoke(DispatcherPriority.SystemIdle,
                                                                      notificationEvent,
                                                                      location);
                               }
                           }
                           );
        }

        /// <summary>
        /// Asynchronously gets the information about the given photo
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="notificationDispatcher"></param>
        /// <param name="notificationEvent"></param>
        public static void AsynchGetPhotoInfo(FlickrPhoto photo,
                                              Dispatcher notificationDispatcher,
                                              FlickrWorkCompleteDelegate notificationEvent)
        {
            AddToWorkQueue(delegate()
                            {
                                string info = GetPhotoInfo(photo);
                                photo.Info = info;

                                if (notificationDispatcher != null && notificationEvent != null)
                                {
                                    notificationDispatcher.BeginInvoke(DispatcherPriority.SystemIdle,
                                                                       notificationEvent,
                                                                       info);
                                }
                            }
                            );
        }

        /// <summary>
        /// Asynchronously gets the icon for the given user.
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="notificationDispatcher"></param>
        /// <param name="notificationEvent"></param>
        public static void AsynchGetUserPhoto(string userid,
                                              Dispatcher notificationDispatcher,
                                              FlickrWorkCompleteDelegate notificationEvent)
        {
            AddToWorkQueue(delegate()
                           {
                               XmlNode userInformation = GetUserInfo(userid);
                               int iconServer = Int32.Parse(userInformation.Attributes["iconserver"].InnerText);

                               string iconUrl;
                               if (iconServer > 0)
                               {
                                   iconUrl = "http://static.flickr.com/" + iconServer + "/buddyicons/" + userid + ".jpg";
                               }
                               else
                               {
                                   iconUrl = "http://www.flickr.com/images/buddyicon.jpg";
                               }

                               if (notificationDispatcher != null && notificationEvent != null)
                               {
                                   notificationDispatcher.BeginInvoke(DispatcherPriority.SystemIdle,
                                                                      notificationEvent,
                                                                      iconUrl);
                               }
                           }
                           );
        }

        /// <summary>
        /// Asynchronously gets a frob 
        /// </summary>
        /// <param name="notificationDispatcher"></param>
        /// <param name="notificationEvent"></param>
        public static void AsynchGetFrob(Dispatcher notificationDispatcher,
                                         FlickrWorkCompleteDelegate notificationEvent)
        {
            AddToWorkQueue(delegate()
                {
                    string frob = GetFrob();
                    if (notificationDispatcher != null && notificationEvent != null)
                    {
                        notificationDispatcher.BeginInvoke(DispatcherPriority.SystemIdle,
                                                           notificationEvent,
                                                           frob);
                    }
                }
                );
        }

        /// <summary>
        /// Asynchronously gets an authenticated user
        /// </summary>
        /// <param name="frob"></param>
        /// <param name="notificationDispatcher"></param>
        /// <param name="notificationEvent"></param>
        public static void AsynchGetAuthenticatedUser(string frob,
                                                      Dispatcher notificationDispatcher,
                                                      FlickrWorkCompleteDelegate notificationEvent)
        {
            AddToWorkQueue(delegate()
                {
                    AuthorizedFlickrUser user = GetAuthorizationInfo(frob);
                    if (notificationDispatcher != null && notificationEvent != null)
                    {
                        notificationDispatcher.BeginInvoke(DispatcherPriority.SystemIdle,
                                                           notificationEvent,
                                                           user);
                    }
                }
                );
        }

        /// <summary>
        /// Asynchrnousy posts comments about the given photo.
        /// </summary>
        /// <param name="comments"></param>
        /// <param name="photo"></param>
        /// <param name="user"></param>
        /// <param name="notificationDispatcher"></param>
        /// <param name="notificationEvent"></param>
        public static void AsynchPostComments(string comments,
                                              FlickrPhoto photo,
                                              AuthorizedFlickrUser user,
                                              Dispatcher notificationDispatcher,
                                              FlickrWorkCompleteDelegate notificationEvent)
        {
            AddToWorkQueue(delegate()
                {
                    bool result = PostComments(comments, photo, user);

                    if (notificationDispatcher != null && notificationEvent != null)
                    {
                        notificationDispatcher.BeginInvoke(DispatcherPriority.SystemIdle,
                                                           notificationEvent,
                                                           result);
                    }
                }
                );
        }

        /// <summary>
        /// Searches for photos based upon the given query string
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static FlickrPhotos SearchForPhotos(string query)
        {
            FlickrMethod method = new FlickrMethod(ApiKey, "flickr.photos.search");
            method.AddParameter("text", query);
            method.AddParameter("sort", "relevance");

            FlickrPhotos photos = new FlickrPhotos(method, 50);
            photos.GetPhoto(0); // we call this to force the call to get the total # count

            return photos;
        }

        /// <summary>
        /// Searches for photos in the given latitude/longitude range.
        /// </summary>
        /// <param name="latLong"></param>
        /// <returns></returns>
        public static FlickrPhotos SearchForPhotos(Rect latLong)
        {
            FlickrMethod method = new FlickrMethod(ApiKey, "flickr.photos.search");            
            method.AddParameter("accuracy", "1");
            method.AddParameter("extras", "geo");
            string searchString = latLong.Left + "," + latLong.Top + "," +
                                  latLong.Right + "," + latLong.Bottom;

            method.AddParameter("bbox", searchString);

            FlickrPhotos photos = new FlickrPhotos(method, 50);
            photos.GetPhoto(0); // we call this to force the call to get the total # count

            return photos;
        }

        /// <summary>
        /// Gets the list of a users most recently updated photos
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static FlickrPhotos GetRecentlyUpdated(AuthorizedFlickrUser user)
        {
            FlickrMethod method = new FlickrMethod(ApiKey,
                                                   "flickr.photos.recentlyUpdated", 
                                                   user.Token, 
                                                   SharedSecret);

            int updateTime = 1;
            method.AddParameter("min_date", updateTime.ToString());
            
            FlickrPhotos photos = new FlickrPhotos(method, 50, user);
            photos.GetPhoto(0); // we call this to force the call to get the total # count

            return photos;
        }
       
        /// <summary>
        /// Gets the info about the given user
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static XmlNode GetUserInfo(string userid)
        {
            FlickrMethod method = new FlickrMethod(ApiKey, "flickr.people.getInfo");
            method.AddParameter("user_id", userid);

            XmlNode rspNode = null;
            if (method.MakeRequest(out rspNode))
            {
                return rspNode.ChildNodes[0];
            }

            return null;
        }
       
        /// <summary>
        /// Gets the authorization info a logged in user
        /// </summary>
        /// <param name="frob"></param>
        /// <returns></returns>
        private static AuthorizedFlickrUser GetAuthorizationInfo(string frob)
        {
            AuthorizedFlickrUser flickrUser = null;

            FlickrMethod method = new FlickrMethod(ApiKey, "flickr.auth.getToken");
            method.AddParameter("frob", frob);

            XmlNode rspNode = null;
            if (method.MakeSignedRequest(SharedSecret, out rspNode))
            {
                foreach (XmlNode outputArg in rspNode.ChildNodes)
                {
                    if (outputArg.Name == "auth")
                    {
                        flickrUser = ParseAuthorizationInfo(outputArg.ChildNodes);
                    }
                }
            }

            authorizedUser = flickrUser;

            return flickrUser;
        }
       
        /// <summary>
        /// Parses the given autohrization info in to an AuthorizedFlickrUser
        /// </summary>
        /// <param name="xmlNodeList"></param>
        /// <returns></returns>
        private static AuthorizedFlickrUser ParseAuthorizationInfo(XmlNodeList xmlNodeList)
        {
            AuthorizedFlickrUser afu = new AuthorizedFlickrUser();

            foreach (XmlNode outputArg in xmlNodeList)
            {
                if (outputArg.Name == "token")
                {
                    afu.Token = outputArg.InnerText;
                }
                else if (outputArg.Name == "perms")
                {
                    afu.Permissions = outputArg.InnerText;
                }
                else if (outputArg.Name == "user")
                {
                    for (int i = 0; i < outputArg.Attributes.Count; i++)
                    {
                        if (outputArg.Attributes[i].Name == "nsid")
                        {
                            afu.NSID = outputArg.Attributes[i].InnerText;
                        }
                        else if (outputArg.Attributes[i].Name == "username")
                        {
                            afu.Username = outputArg.Attributes[i].InnerText;
                        }
                        else if (outputArg.Attributes[i].Name == "fullname")
                        {
                            afu.Fullname = outputArg.Attributes[i].InnerText;
                        }
                    }
                }
            }

            if (afu.Token == "") return null;
            else return afu;
        }

        /// <summary>
        /// Gets the logon URL used to authenticate with Flickr
        /// </summary>
        /// <param name="frob"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string GetLoginUrl(string frob, string permission)
        {
            FlickrAuthorizationUrl authUrl = new FlickrAuthorizationUrl(ApiKey);
            authUrl.AddParameter("perms", permission);
            authUrl.AddParameter("frob", frob);

            return authUrl.GetSignedUrlString(SharedSecret);
        }

        /// <summary>
        /// Gets a frob from the Flickr service
        /// </summary>
        /// <returns></returns>
        private static string GetFrob()
        {
            FlickrMethod method = new FlickrMethod(ApiKey, "flickr.auth.getFrob");

            XmlNode rspNode = null;
            if (method.MakeSignedRequest(SharedSecret, out rspNode))
            {
                foreach (XmlNode outputArg in rspNode.ChildNodes)
                {
                    if (outputArg.Name == "frob")
                    {
                        return outputArg.InnerText;
                    }
                }
            }

            throw new Exception("Error getting frob");
        }

        /// <summary>
        /// Gets the photos returned from the given flickr method
        /// </summary>
        /// <param name="method"></param>
        /// <param name="totalPhotos"></param>
        /// <returns></returns>
        internal static List<FlickrPhoto> GetPhotos(FlickrMethod method, ref int totalPhotos)
        {
            XmlNode rspNode = null;
            List<FlickrPhoto> photos = new List<FlickrPhoto>();

            DateTime time = DateTime.Now;
            if (method.MakeRequest(out rspNode))
            {
                foreach (XmlNode outputArg in rspNode.ChildNodes)
                {
                    if (outputArg.Name == "photos")
                    {
                        totalPhotos = Int32.Parse(outputArg.Attributes["total"].InnerText);

                        foreach (XmlNode photoNode in outputArg.ChildNodes)
                        {
                            photos.Add(new FlickrPhoto(photoNode.Attributes["id"].InnerText,
                                                       photoNode.Attributes["server"].InnerText,
                                                       photoNode.Attributes["secret"].InnerText,
                                                       method.AuthToken));

                        }
                    }
                }
            }
            
            return photos;
        }

        /// <summary>
        /// Gets the information about the given photo
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        public static string GetPhotoInfo(FlickrPhoto photo)
        {
            FlickrMethod method;
            if (photo.AuthToken != null)
            {
                method = new FlickrMethod(ApiKey, "flickr.photos.getInfo", photo.AuthToken, SharedSecret);
            }
            else
            {
                method = new FlickrMethod(ApiKey, "flickr.photos.getInfo");
            }

            method.AddParameter("photo_id", photo.ID);
            method.AddParameter("secret", photo.Secret);
            XmlNode rspNode = null;

            if (method.MakeRequest(out rspNode))
            {
                return rspNode.InnerXml;
            }

            return "";
        }

        /// <summary>
        /// Gets the comments about the given photo
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        public static string GetPhotoComments(FlickrPhoto photo)
        {
            FlickrMethod method;            
            if (photo.AuthToken != null)
            {
                method = new FlickrMethod(ApiKey, "flickr.photos.comments.getList", photo.AuthToken, SharedSecret);
            }
            else
            {
                method = new FlickrMethod(ApiKey, "flickr.photos.comments.getList");
            }

            method.AddParameter("photo_id", photo.ID);
            XmlNode rspNode = null;

            if (method.MakeRequest(out rspNode))
            {
                return rspNode.InnerXml;
            }

            return "";
        }

        /// <summary>
        /// Gets the location of the given photo
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        public static string GetPhotoLocation(FlickrPhoto photo)
        {
            FlickrMethod method;
            if (photo.AuthToken != null)
            {
                method = new FlickrMethod(ApiKey, "flickr.photos.geo.getLocation", photo.AuthToken, SharedSecret);
            }
            else
            {
                method = new FlickrMethod(ApiKey, "flickr.photos.geo.getLocation");
            }

            method.AddParameter("photo_id", photo.ID);
            XmlNode rspNode = null;

            if (method.MakeRequest(out rspNode))
            {
                return rspNode.InnerXml;
            }

            return "";
        }

        /// <summary>
        /// Posts the given comments about the given photo.
        /// </summary>
        /// <param name="comments"></param>
        /// <param name="photo"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool PostComments(string comments, FlickrPhoto photo, AuthorizedFlickrUser user)
        {
            FlickrMethod method = new FlickrMethod(ApiKey, "flickr.photos.comments.addComment",
                                                   user.Token, SharedSecret);
            method.AddParameter("comment_text", comments);
            method.AddParameter("photo_id", photo.ID);

            XmlNode rspNode = null;
            return method.MakeRequest(out rspNode);            
        }

        /// <summary>
        /// Flickr API key
        /// </summary>
        private static string ApiKey
        {
            get { return _apiKey; }
        }

        /// <summary>
        /// Flickr shared secret
        /// </summary>
        private static string SharedSecret
        {
            get { return _sharedSecret; }
        }

        /// <summary>
        /// The current authorized uer
        /// </summary>
        public static AuthorizedFlickrUser CurrAuthorizedUser
        {
            get { return authorizedUser; }
        }

        public delegate void FlickrWorkDelegate();
        public delegate void FlickrWorkCompleteDelegate(object o);
       
        //////////////////////////////////////////////////////
        //
        // Private Data
        //
        //////////////////////////////////////////////////////
        
        private static AuthorizedFlickrUser authorizedUser;
        
        private static readonly string _apiKey;
        private static readonly string _sharedSecret;
        
        private const int NUM_WORKER_THREADS = 5;
        private static Thread[] workerThreads;
        private static LinkedList<FlickrWorkDelegate>[] workerThreadWorkQueue;
    }
}

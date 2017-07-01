// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty  Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;

namespace DeepEarth
{

    /// <summary>
    /// <para>
    /// Helper functions around the Silverlight IsolatedStorage
    /// The storage is stored per application or per domaain and can be thought of as cookie on steroids.
    /// Perfect place to store user preferences.
    /// </para>
    /// <example>
    ///
    /// <code title="Save an object of TilePathInfo into the ISO and then get it back out.">  
    /// <![CDATA[ 
    /// //TilePathInfo class is serializable using [DataContract] on the class and [DataMember] on the properties
    /// TilePathInfo tilePathInfo = new TilePathInfo();
    /// tilePathInfo.TilePath = "Simple Test"; //set some test data for the example
    /// 
    /// //Need a unique key for storage and retrieval
    /// string ISOKey = "SimpleTest.txt"
    /// 
    /// //IsolatedStorage is a static class, lets save this data:
    /// IsolatedStorage.SaveData(tilePathInfo, ISOKey);
    /// 
    /// //Then to get it back out:
    /// TilePathInfo newtilePathInfo = IsolatedStorage.LoadData<TilePathInfo>(ISOKey);
    /// ]]>
    /// </code>
    /// </example>
    /// </summary>
    public static class IsolatedStorage
    {
        /// <summary>
        /// Saves a string of data to the given filename (key)
        /// </summary>
        /// <param name="data">The data to save</param>
        /// <param name="fileName">The unique filename for retrieval later</param>
        public static void SaveData(string data, string fileName)
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var isfs = new IsolatedStorageFileStream(fileName, FileMode.Create, isf))
                {
                    using (var sw = new StreamWriter(isfs))
                    {
                        sw.Write(data);
                        sw.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Save a generic item of type T (your choice of type) to the given filename (key)
        /// </summary>
        /// <typeparam name="T">The serializerable Type</typeparam>
        /// <param name="dataToSave">The data to save</param>
        /// <param name="fileName">The unique filename for retrieval later</param>
        public static void SaveData<T>(T dataToSave, string fileName)
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                IsolatedStorageFileStream isfs = isf.CreateFile(fileName);
                var serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(isfs, dataToSave);
                isfs.Close();
            }
        }

        /// <summary>
        /// Retrieves a String of data given the unique key, else returns string.empty
        /// </summary>
        /// <param name="fileName">The unique filename for retrieval</param>
        /// <returns>The value stored if found else string.empty</returns>
        public static string LoadStringData(string fileName)
        {
            string data = String.Empty;
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isf.FileExists(fileName))
                {
                    using (var isfs = new IsolatedStorageFileStream(fileName, FileMode.Open, isf))
                    {
                        using (var sr = new StreamReader(isfs))
                        {
                            string lineOfData;
                            while ((lineOfData = sr.ReadLine()) != null) data += lineOfData;
                        }
                    }
                }
            }
            return data;
        }

        /// <summary>
        /// Retrieves a generic item of type T data (your choice of type) given the unique key, else returns default.
        /// </summary>
        /// <typeparam name="T">The serializerable Type</typeparam>
        /// <param name="fileName">The unique filename for retrieval</param>
        /// <returns>The value stored if found else the default</returns>
        public static T LoadData<T>(string fileName)
        {
            T data = default(T);
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isf.FileExists(fileName))
                {
                    IsolatedStorageFileStream isfs = isf.OpenFile(fileName, FileMode.Open);
                    var serializer = new DataContractSerializer(typeof (T));
                    data = (T) serializer.ReadObject(isfs);
                    isfs.Close();
                }
            }
            return data;
        }

    }
}
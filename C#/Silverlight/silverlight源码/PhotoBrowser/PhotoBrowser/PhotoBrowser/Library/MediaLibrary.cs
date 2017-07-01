using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using WMPLib;
using System.IO;
using System.Security.Policy; // windows media player

namespace MediaBrowser.Library
{
    /// <summary>
    /// Represents the media library on the computer
    /// </summary>
    class MediaLibrary
    {
        // the player control and associated library
        static private WindowsMediaPlayer Player;
        static private IWMPMediaCollection2 Library;

        static MediaLibrary()
        {
            Player = new WindowsMediaPlayer();

            Player.CurrentMediaItemAvailable += MediaLibrary.CurrMediaAvailable;

            Library = (IWMPMediaCollection2)Player.mediaCollection;
        }

        static public void CurrMediaAvailable(string name)
        {
            int x = 0;
        }

        static public List<Album> GetAlbums()
        {
            List<Album> albums = new List<Album>();

            IWMPQuery nullQuery = Library.createQuery();
            IWMPStringCollection2 allTitles = (WMPLib.IWMPStringCollection2)Library.getStringCollectionByQuery("Album", nullQuery, "audio", "", false);

            //
            // go grab the album information for the ones we're interested in
            //
            for (int i = 0; i < allTitles.count; i++)
            {
                string albumTitle = allTitles.Item(i);
                string albumArtUri = null;

                IWMPPlaylist albumPlaylist = Library.getByAlbum(albumTitle);
                
                if (albumPlaylist.count > 0)
                {
                    IWMPMedia3 mediaItem = (IWMPMedia3)albumPlaylist.get_Item(0);

                    // see if we can pull the album art
                    int numOfType = mediaItem.getAttributeCountByType("WM/Picture", "");
                    if (numOfType > 0)
                    {
                        IWMPMetadataPicture picData = (IWMPMetadataPicture)mediaItem.getItemInfoByType("WM/Picture", "", 0);   
                        albumArtUri = null;
                    }                    
                }

                albums.Add(new Album(albumTitle, albumArtUri));
            }

            // return the list of albums
            return albums;
        }
    }
}

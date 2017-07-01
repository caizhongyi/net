using System;
using System.Collections;

/// <summary>
/// Original: SWX Flickr API by Aral Balkan. Uses the phpFlickr library. You can call this API using SWX, Amfphp, JSON and XML-RPC.
/// </summary>
public class Flickr
{
    // Please replace these keys with your own if deploying
    // to your own server and please do not abuse these
    // or they will have to be revoked/changed.

    string apiKey = "e7efb59164979981686e62d8bcc473be";
    string sharedSecret = "2be064bed40b0b78";

    FlickrNet.Flickr _flickr;
    public Flickr()
    {
        _flickr = new FlickrNet.Flickr(apiKey, sharedSecret);
    }

	public object getUserPhotos(string userName, string photoStyle, int numPhotos)
	{
		return swxGetUserPhotos(userName, photoStyle, numPhotos, 1);
	}

    public object getUserPhotos(string userName, string photoStyle, int numPhotos, int page)
    {
        return swxGetUserPhotos(userName, photoStyle, numPhotos, page);
    }

	public object swxGetUserPhotos(string userName, string photoStyle, int numPhotos, int page)
	{
		if(!String.IsNullOrEmpty(userName)) 
		{
		    // Find the NSID of the username
		    FlickrNet.FoundUser person = _flickr.PeopleFindByUsername(userName);
		
		    // Get $numPhotos of the user's public photos, starting at page $page.
            FlickrNet.PhotoSearchOptions options = new FlickrNet.PhotoSearchOptions();
            options.UserId = person.UserId; // Your NSID
            options.PerPage = numPhotos;
            options.Page = page;
            FlickrNet.Photos photos = _flickr.PhotosSearch(options);
		    //FlickrNet.Photos photos = _flickr.PeopleGetPublicPhotos(person.UserId);
			// Build the results
			ArrayList photoList = new ArrayList();
			
		    // Loop through the photos and output the html
            foreach(FlickrNet.Photo photo in photos.PhotoCollection)
            {
                Hashtable newPhoto = new Hashtable();
                newPhoto["id"] = photo.PhotoId;
                newPhoto["link"] = photo.LargeUrl;
                newPhoto["alt"] = photo.Title;
                newPhoto["src"] = photo.SquareThumbnailUrl;
                photoList.Add(newPhoto);
			}

            Hashtable result = new Hashtable();
		    result["photo"] = photoList;
            result["page"] = photos.PageNumber;
            result["pages"] = photos.TotalPages;
            return result;
		}						
		return null;
	}

	public object photosGetSizes(string photoId)
	{
        FlickrNet.Sizes sizes = _flickr.PhotosGetSizes(photoId);
        return sizes.SizeCollection;
	}
}

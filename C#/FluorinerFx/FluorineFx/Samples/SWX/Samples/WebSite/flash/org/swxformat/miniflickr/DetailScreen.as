import org.swxformat.SWX;
import org.swxformat.LoadManager;
import org.swxformat.ExternalAsset;

import org.swxformat.miniflickr.EventfulMovieClip;
import org.swxformat.miniflickr.PublicSwxGateway;

import com.darronschall.DynamicRegistration;

import mx.utils.Delegate;
import mx.transitions.Tween;
import mx.transitions.easing.*;

class org.swxformat.miniflickr.DetailScreen extends EventfulMovieClip
{
	// Constants
	public static var CLOSE_DETAILS:String = "closeDetails";
	
	//
	// Properties
	//
	
	// Reference to the public SWX gateway.
	var swx:SWX;

	// TODO: Refactor: Pull out to base class.	
	var transitions:Array;
	
	// List of photo ids for the current page.
	var photoIds:Array;
	var photoIdIndex:Number;
		
	//
	// On stage
	//
	
	var imageHolder:MovieClip;
	var mainDataLoadIndicator:MovieClip;
	var loadIndicator:MovieClip;
	var alt:TextField;
	
	// Constructor
	function DetailScreen()
	{
		// Get a reference to the public SWX gateway.
		swx = PublicSwxGateway.getInstance().swx;
		
		transitions = new Array();
	}
	
	//
	// Public methods
	//
	
	public function set visible(state:Boolean):Void
	{
		_visible = state;
	
		if (state)
		{
			// Visible
			Key.removeListener(this);
			Key.addListener(this);
		}
		else
		{
			// Invisible.
			
			// Stop listening for user input.
			Key.removeListener(this);
		}
	}
	
	public function showPicture(photoIds:Array, photoIdIndex:Number):Void
	{
		// Save local copies of the photo data. 
		this.photoIds = photoIds;
		this.photoIdIndex = photoIdIndex;

		var id:String = photoIds[photoIdIndex];
		
		// Extend the backlight so user can see what's going on.
		fscommand2("ExtendBacklightDuration", 30);
		
		// Cancel all current calls. This will also
		// cancel all loading external assets that use the LoadManager. 
		swx.cancelAllCalls();

		// Create the SWX call parameters
		var callParameters:Object =
		{
			serviceClass: "Flickr",
			method: "photosGetSizes",
			args: [id],
			result: [this, resultHandler],
			timeout: [this, timeoutHandler],
			fault: [this, faultHandler]
		}

		// Make the SWX call
		swx.call(callParameters);

		// Display the load indicator
		mainDataLoadIndicator.message.text = "Loading photo info...";	
		mainDataLoadIndicator._visible = true;
		
		loadIndicator._visible = false;
		imageHolder._visible = false;
		
		// Make screen visible
		visible = true;
	}
	
	
	//
	// Private methods
	//	
		
	// TODO: Refactor: Pull out to base class.
	private function stopAllTransitions():Void
	{
		for (var i=0; i < transitions.length;i++)
		{
			transitions[i].stop();
		}
	}
		
	//
	// Event handlers
	//
	
	private function onLoad():Void
	{
		DynamicRegistration.initialize(imageHolder);
	}
	
	
	// Key handler; prev/next navigation between pages.
	private function onKeyUp():Void
	{
		var code = Key.getCode();
		var ascii = Key.getAscii();

		if (code == ExtendedKey.SOFT1)
		{
			swx.cancelAllCalls();
			dispatchEvent({type: CLOSE_DETAILS});
			return;
		}

		// Right
		if (ascii == 35) // || ascii == 50) // for desktop testing, enable and use "1"
		{
			// #: Get the next photo detail.
			if (photoIdIndex != (photoIds.length-1))
			{
				photoIdIndex++;
				showPicture(photoIds, photoIdIndex);
			}
			
			return;
		}

		// Left
		if (ascii == 42 || code == ExtendedKey.SOFT1) // || ascii == 49) // for desktop testing, enable and use "2"
		{
			// *: Get the prev photo detail
			if (photoIdIndex != 0)
			{
				photoIdIndex--;
				showPicture(photoIds, photoIdIndex);
			}
			
			return;
		}



	}
	
	// SWX result handler.
	private function resultHandler(event:Object):Void
	{
		// Hide the main data load indicator.
		mainDataLoadIndicator._visible = false;
		
		// Show the load indicator
		loadIndicator._visible = true;

		// Get a reference to the returned results.
		var sizes:Array = event.result;

		var imageSrc = "";
		for (var i = 0; i < sizes.length; i++)
		{
			if (sizes[i].Label == "Small")
			{
				// OK, this is the size we want to display, get the source URL.
				imageSrc = sizes[i].Source; 
			}
		}
		
		// Create a new external asset to load.
		var image:ExternalAsset = new ExternalAsset(imageHolder, imageSrc);
		
		image.addEventListener(ExternalAsset.LOAD, Delegate.create(this, imageLoadHandler))

		// Add it to the load queue using the Load Manager.
		LoadManager.getInstance().load(image);
		
		// Garbage collection, remove the clip that the data is in 
		// since we won't need it again.
		//sizes = null;
		//event.result = null; 
		//event.clip.removeMovieClip();
		
	}

	private function imageLoadHandler():Void
	{
		// Center the image once it has loaded.		
		// TODO: Do not hard code these.
		var centerX:Number = 120 - imageHolder._width/2;
		var centerY:Number = 160 - imageHolder._height/2;

		// Set the image's registration point to its center
		// so that it rotates nicely. It's all about the visual bling! :)
		// (Thanks, Darron!)
		imageHolder.setRegistration(imageHolder._width/2,imageHolder._height/2);

		imageHolder._x2 = 120; 
		imageHolder._y2 = 160;
	
		imageHolder._visible = true;
		
		stopAllTransitions();
		
		var t1:Tween = new Tween(imageHolder, "_xscale2", Strong.easeIn, 0, 100, .75, true);
		var t2:Tween = new Tween(imageHolder, "_yscale2", Strong.easeIn, 0, 100, .75, true);
		var t3:Tween = new Tween(imageHolder, "_rotation2", Strong.easeIn, -360, 0, .75, true);
		
		transitions =  [t1, t2, t3];
	}

	// SWX timeout handler.
	private function timeoutHandler():Void
	{
		trace("Call timed out!");
	}	
	
	// SWX fault handler.
	private function faultHandler(event:Object):Void
	{
		// An error occured. Display the error message.
		trace(event.fault.message);
	}
}
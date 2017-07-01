
import mx.events.EventDispatcher;
	
class org.swxformat.EventfulMovieClip extends MovieClip
{
	
	public function EventfulMovieClip()
	{
		EventDispatcher.initialize(this);
	}

	//
	// Note: 	The following methods are mixed in at runtime 
	//       	by the EventDispatcher.
	
	/**
	* Add a listener for a particular event.
	* 
	* @param event the name of the event ("click", "change", etc)
	* @param handler the function or object that should be called
	*/
	public function addEventListener(event:String, handler) : Void {};

	/**
	* Remove a listener for a particular event.
	* 
	* @param event the name of the event ("click", "change", etc)
	* @param handler the function or object that should be called
	*/

	public function removeEventListener(event:String, handler) : Void {};

	/**
	* Dispatch an event to all listeners
	* 
	* @param eventObj an Event or one of its subclasses describing the event
	*/
	public function dispatchEvent(eventObj:Object) : Void {};
}

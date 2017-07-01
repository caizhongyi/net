//
// MTASC example for SWX Full API.
//
// The SWX classes compile under MTASC but please note the following:
//
// 1. 	In the std folder, you must create an mx/utils/ folder and in it place 
// 		Steve Webster's refined Delegate class (and change its class signature to mx.utils.Delegate)
// 		http://dynamicflash.com/2005/05/delegate-version-101/
// 		(This has been done for you in this example folder).
//
// 2.	You must include the standard Macromedia classes in your classpath
//      eg. On my system, -cp /Users/aral/Library/Application\ Support/Macromedia/Flash\ 8/en/Configuration/Classes/
//
// The command line I used to compile this example on my machine is:
// mtasc -cp ../../ -cp /Users/aral/Library/Application\ Support/Macromedia/Flash\ 8/en/Configuration/Classes/ -cp std/ -swf mtascTest.swf -main -header 500:200:20 MtascTest.as
//
// Thanks to Wouter Verweirder for providing two changes to the codebase to make it compatible with MTASC.
//

import org.swxformat.*;

class MtascTest extends MovieClip // and assimilates _root
{
	static var mtascTest:MtascTest;
	
	var status:TextField;

	function MtascTest(target) 
	{
		// Assimilate the target
		target.__proto__ = this.__proto__;
		target.__constructor__ = MtascTest;
		this = target;		

		// Creates a TextField sized 100x600 at pos 100, 100
		createTextField("status", 0, 100, 100, 400, 150);

		// Create the data holder movie clip
		createEmptyMovieClip("dataHolder", 1000);
		
		var swx:SWX = new SWX();
		swx.gateway = "http://localhost:8888/php/swx.php";
		swx.encoding = "GET";
		swx.debug = true;
		
		var callParameters:Object =
		{
			serviceClass: "Simple",
			method: "echoData",
			args: ['Echo, echo, echo..!'],
			result: [this, resultHandler]
		}
		
		swx.call(callParameters);
	}
	
	function resultHandler(event:Object):Void
	{
		status.text = event.result;
	}
		
	static function main ()
	{
		// Create an Application instance and
		// have it assimilate _root.
		var mtascTest:MtascTest = new MtascTest(_root);
	}
}
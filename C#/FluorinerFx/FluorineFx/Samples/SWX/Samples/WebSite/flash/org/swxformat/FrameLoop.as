//
//  FrameLoop
//
//  Created by Aral Balkan on 2006-10-14.
//  Copyright (c) 2006 Aral Balkan. All rights reserved.
//

import org.swxformat.*;

class FrameLoop extends EventfulMovieClip
{
	// Constants
	public static var PROGRESS:String = "progress";
	public static var COMPLETE:String = "complete";

	// Properties
	private var commands:Array;
	private var totalCommands:Number = null;
	private var completedCommands:Number = 0;

	// Make sure you add functions to the array as closures (use Delegate)
	public function FrameLoop()
	{
		totalCommands = commands.length;
		start();
	}
	
	// Start the frame loop
	public function start():Void
	{
		onEnterFrame = doFrameLoop;
	}
	
	// Stop the frame loop
	public function halt()
	{
		onEnterFrame = null;
	}
	
	// 
	// Private methods
	//
	
	private function doFrameLoop()
	{		
		// Run the next function in the loop. Typed
		// to an object as you should be using a closure
		// not a bare function reference (i.e., use Delegate)
		var currentCommand:Object = commands.shift();
		
		var currentScope:Object = currentCommand.scope;
		var currentFunction = currentCommand.fn;
		var currentArgs = currentCommand.args;

		var startTime:Number = getTimer();
		currentFunction.apply(currentScope, currentArgs);
		var duration:Number = getTimer()-startTime;

		completedCommands++;
		
		trace ("Frameloop: iteration " + completedCommands + " took " + duration + "ms.")
		
		// Dispatch progress event
		dispatchEvent({type: PROGRESS, completed: completedCommands, total: totalCommands});
		
		// Are we done?
		if (!commands.length)
		{
			// Yes: Remove the frame loop and dispatch complete event
			halt();
			dispatchEvent({type: COMPLETE, completed: completedCommands, total: totalCommands});
		}
	}
}
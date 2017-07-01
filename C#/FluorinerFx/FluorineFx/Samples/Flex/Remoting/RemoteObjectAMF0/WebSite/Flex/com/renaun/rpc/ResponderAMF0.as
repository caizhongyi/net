/*

Copyright 2006,2007 Renaun Erickson (http://renaun.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

@version 1.1
@ignore
*/
package com.renaun.rpc {
	
import flash.net.Responder;
import mx.messaging.messages.RemotingMessage;
import mx.rpc.AsyncToken;
import mx.rpc.events.ResultEvent;

/**
 * 	The Responder class provides an object that is used in 
 * 	NetConnection.call() to handle return values from the server.
 */
dynamic public class ResponderAMF0 extends Responder {

	//--------------------------------------------------------------------------
	//
	//  Properties
	//
	//--------------------------------------------------------------------------

	/**
	 * 	@private
	 */		
	private var asyncToken:AsyncToken;
	
	/**
	 * 	@private
	 */		
	private var message:RemotingMessage;

	/**
	 * 	@private
	 */			
	private var resultFunction:Function;

	/**
	 * 	@private
	 */			
	private var statusFunction:Function;

	//--------------------------------------------------------------------------
	//
	//  Constructor
	//
	//--------------------------------------------------------------------------
	
	/**
	 *	Constructs an instance of the responder object with the specified methodName, result function, and status function.
	 */
	public function ResponderAMF0( methodName:String, result:Function, status:Function ) {
		super( onQueryResult, onQueryFault );
		resultFunction = result;
		statusFunction = status;
		message = new RemotingMessage();
		message.body = "success";	
		message.operation = methodName;
		asyncToken = new AsyncToken( message );
	}
	
	/**
	 * 	@private
	 */	
    private function onQueryResult( result : Object ):void {
    	message.body = "success";
		resultFunction( RemotingMessage( asyncToken.message ).operation, result );

    }
	
	/**
	 * 	@private
	 */
    private function onQueryFault( fault : Object ):void {
    	message.body = "fault";
		statusFunction( RemotingMessage( asyncToken.message ).operation, fault );
    }
    
    /**
     * 	Returns the reference to the AsyncToken for this Responder.
     */
    public function getAsyncToken():AsyncToken {
    	return asyncToken;	
    }

}

}
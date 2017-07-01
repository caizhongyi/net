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
	
import flash.events.*;

import mx.core.IMXMLObject;
import mx.managers.CursorManager;
import mx.rpc.AsyncToken;
import mx.rpc.events.FaultEvent;	
import mx.rpc.events.ResultEvent;
import mx.rpc.Fault;
import mx.rpc.mxml.IMXMLSupport;

//--------------------------------------
//  Events
//--------------------------------------


/**
 *  The result event is dispatched when a service call successfully 
 * 	returns and isn't handled by the Operation itself.
 *
 *  <p>The RESULT event type.</p>
 *
 *  @eventType mx.rpc.events.ResultEvent.RESULT
 */
[Event(name="result", type="mx.rpc.events.ResultEvent")]

/**
 *  The fault event is dispatched when a service call 
 *  fails and isn't handled by the Operation itself.
 *
 *  <p>The FAULT event type.</p>
 *
 *  @eventType mx.rpc.events.FaultEvent.FAULT
 */
[Event(name="fault", type="mx.rpc.events.FaultEvent")]	

[Bindable]
/**
 *  The method class for RemoteObjectAMF0 service calls.
 */
public class method implements IMXMLSupport
{

    //--------------------------------------------------------------------------
    //
    //  Constructor
    //
    //--------------------------------------------------------------------------

    /**
     *  Constructor.
     */
	public function method() 
	{
		_arguments = [];
	}

    //--------------------------------------------------------------------------
    //
    //  Properties
    //
    //--------------------------------------------------------------------------

    //----------------------------------
    //  concurrency
    //----------------------------------

    /**
     *  @private
     */		
	private var _concurrency:String = "multiple";

    /**
     *  Value that indicates how to handle multiple calls to the same service. 
     *  The default value is multiple. The following values are permitted:
     * 
     *  <ul>
     * 	<li>multiple Existing requests are not cancelled, 
     * 	and the developer is responsible for ensuring the 
     * 	consistency of returned data by carefully managing 
     * 	the event stream. This is the default.</li>
     * 
     * 	<li>single Only a single request at a time is 
     * 	allowed on the operation; multiple requests 
     * 	generate a fault.</li>
     * 
     * 	<li>last Making a request cancels any 
     * 	existing request.</li>
     * 	</ul>
     * 
     *  @default multiple
     */
    public function get concurrency():String 
    {
        return _concurrency;
    }         
    
    /**
     *  @private
     */
    public function set concurrency( value:String ):void 
    {
        _concurrency = value;
    }

    //----------------------------------
    //  makeObjectsBindable
    //----------------------------------

    /**
     *  @private
     */	    	
	private var _makeObjectsBindable:Boolean = false;	

    /**
     *  When this value is true, anonymous objects 
     * 	returned are forced to bindable objects.
     * 
     *  @default false
     */
    public function get makeObjectsBindable():Boolean 
    {
        return _makeObjectsBindable;
    }
    
    /**
     *  @private
     */
    public function set makeObjectsBindable( value:Boolean ):void 
    {
        _makeObjectsBindable = value;
    }

    //----------------------------------
    //  arguments
    //----------------------------------

    /**
     *  @private
     */
    private var _arguments:Array; // the array of our DataGridColumns

 	public function get arguments():Array
 	{
 		return _arguments.slice(0);
 	}

 	public function set arguments( value:Array ):void
 	{
 		_arguments = value.slice(0);
 	}

    
    //----------------------------------
    //  name
    //----------------------------------

    /**
     *  @private
     */	    	
	private var _name:String = "";

    /**
     *  Defines the method name that you want to call on the service class.
     * 
     *  @default empty string
     */
    public function get name():String 
    {
        return _name;
    }
    
    /**
     *  @private
     */
    public function set name( value:String ):void 
    {
        _name = value;
    }


    //----------------------------------
    //  showBusyCursor
    //----------------------------------

    /**
     *  @private
     */	    	
	private var _showBusyCursor:Boolean = false;	

    /**
     *  If true, a busy cursor is displayed while a 
     * 	service is executing. The default value is false.
     * 
     *  @default false
     */
    public function get showBusyCursor():Boolean 
    {
        return _showBusyCursor;
    }
    
    /**
     *  @private
     */
    public function set showBusyCursor( value:Boolean ):void 
    {
        _showBusyCursor = value;
    } 	
    

    //--------------------------------------------------------------------------
    //
    //  Methods
    //
    //--------------------------------------------------------------------------

								
}
}


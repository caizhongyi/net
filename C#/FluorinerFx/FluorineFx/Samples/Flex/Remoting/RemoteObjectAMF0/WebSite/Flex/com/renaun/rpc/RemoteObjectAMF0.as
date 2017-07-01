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

import com.renaun.rpc.RemotingConnection;
import com.renaun.rpc.ResponderAMF0;
	
import flash.errors.IllegalOperationError;
import flash.events.*;
import flash.net.*;
import flash.utils.flash_proxy;
import flash.utils.Proxy;

import mx.collections.*;
import mx.controls.Alert;
import mx.core.IMXMLObject;
import mx.managers.CursorManager;
import mx.rpc.AbstractService;
import mx.rpc.AsyncToken;
import mx.rpc.events.FaultEvent;	
import mx.rpc.events.ResultEvent;
import mx.rpc.Fault;
import mx.rpc.mxml.IMXMLSupport;
import mx.utils.ArrayUtil;
import mx.messaging.config.ServerConfig;
import mx.messaging.errors.InvalidDestinationError;
import mx.utils.ObjectUtil;
import mx.messaging.ChannelSet;
import mx.messaging.Channel;

use namespace flash_proxy;

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
 *  The RemoteObjectAMF0 class provides a method to connect to AMF0 gateways.
 */
dynamic public class RemoteObjectAMF0 extends AbstractService 
									  implements IMXMLObject, IMXMLSupport
{

    //--------------------------------------------------------------------------
    //
    //  Constructor
    //
    //--------------------------------------------------------------------------

    /**
     *  Constructor.
     */
	public function RemoteObjectAMF0() 
	{
		_methods = [];

		_methodsLookup = new Object();		
		_methodResultLookup = new Object();
		_methodResponderLookup = new Object();		
	}

    //--------------------------------------------------------------------------
    //
    //  Variables
    //
    //--------------------------------------------------------------------------

    /**
     *  @private
     */
	private var _view:Object;

    /**
     *  @private
     */	
	private var _id:String;

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
    //  endpoint
    //----------------------------------

    /**
     *  @private
     */	    	
	private var _endpoint:String = null;//"http://{server.name}:{server.port}/flashservices/gateway.php";

    /**
     *  URL to the AMF0 server's gateway.
     * 
     *  @default http://{server.name}:{server.port}/flashservices/gateway.php
     */
    public function get endpoint():String 
    {
        return _endpoint;
    }
    
    /**
     *  @private
     */
    public function set endpoint( value:String ):void 
    {
        _endpoint = value;
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
    //  methods
    //----------------------------------

    /**
     *  @private
     */
     
    private var _methods:Array; // the array of our DataGridColumns


 	public function get methods():Array
 	{
 		return _methods;
 	}

 	public function set methods( value:Array ):void
 	{
 		_methods = value;
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
    
    //----------------------------------
    //  source
    //----------------------------------

    /**
     *  @private
     */	    	
	private var _source:String = "";

    /**
     *  Defines the Service classes name.  The service class should
     *  be defined with out a trailing file extension.
     * 
     * 	<p>Example:  source="com.renaun.samples.login.services.Login"
     *  The above source value will tell the AMFPHP gateway to look for
     *  a service classes located at
     *  /{services}/com/renaun/samples/login/services/Login.php</p>
     * 
     *  <p>Each specific gateway can implement the source value differently</p>
     * 
     *  @default empty string
     */
    public function get source():String 
    {
        return _source;
    }
    
    /**
     *  @private
     */
    public function set source( value:String ):void 
    {
        _source = value;
    }

    //----------------------------------
    //  gateway_conn
    //----------------------------------

	private var _gatewayConnection:RemotingConnection;

	/**
	 * 	The RemotingConnection that references the current connection
	 *  to the AMF gateway.
	 */
    public function get gateway_conn():RemotingConnection 
    {
		if( _gatewayConnection == null ) 
		{
			if( endpoint != null )
	        	_gatewayConnection = new RemotingConnection( endpoint );
	        else
	        {
				var channelSet:ChannelSet = ServerConfig.getChannelSet(destination);
				var channelId:String = channelSet.channelIds[0];
				var channel:Channel = ServerConfig.getChannel(channelId);
	        	_gatewayConnection = new RemotingConnection( channel.endpoint );
	        }
        	_gatewayConnection.addEventListener( NetStatusEvent.NET_STATUS, netStatusHandler );
  		}
        return _gatewayConnection;
    }	 

	protected var _methodsLookup:Object;
	protected var _methodResultLookup:Object;
	protected var _methodResponderLookup:Object;		

	/**
	 * 	Carray over from Flex 1.5.  It stores the last results value, beware this is the last 
	 *  results value regardless of what method call.
	 */
	public var results:*;

    //--------------------------------------------------------------------------
    //
    //  Methods
    //
    //--------------------------------------------------------------------------

	flash_proxy override function callProperty( methodName:*, ...args ):* 
	{
        var respond:ResponderAMF0 = new ResponderAMF0( methodName, setQueryResult, setQueryFault );
		
		var parameters:Array = args;
		
		// Help from code found here - http://www.code4net.com/archives/000119.html
		// Also found in Flex Framework source files
		if(args.length > 0){
			parameters.unshift( source + "." + methodName.toString(), respond );
    		gateway_conn.call.apply( gateway_conn, parameters );
		} else {
            gateway_conn.call( source + "." + methodName.toString(), respond );
		}
		
		// Set Busy Cursor
		if( _showBusyCursor )
			CursorManager.setBusyCursor();

		// Save a reference to the current ResponderAMF0, keyed by method name
        _methodResponderLookup[ methodName ] = respond;
        return respond.getAsyncToken();
	}
	
	flash_proxy override function getProperty( name:* ):* {
		if( _methodResultLookup[ name ] != null )
			return _methodResultLookup[ name ];
	}		
	
	flash_proxy override function setProperty(name:*, value:*):void {
		if( name == "destination" )
			endpoint = value;
		else
			_methodResultLookup[ name ] = value;
	}

	flash_proxy override function getDescendants( name:* ):* {
	
	}
	
	/**
	 * 	Not sure 
	 */
	public function initialized( view:Object, id:String  ):void {
		_view = view;
		_id = id;
	}

	/**
	 * 	Set the credentials for the destination accessed by the service. 
	 * 	The credentials are applied to all services connected over the same ChannelSet.
	 *  Note that services that use a proxy or a third-party adapter to a remote 
	 *  endpoint will need to setRemoteCredentials instead.
	 */
	override public function setCredentials( username:String, password:String ):void
	{
		gateway_conn.setCredentials( username, password );			
	}

	/**
	 * 	@private
	 */
    private function setQueryResult( methodName:String, result:Object ):void {
		results = bindableFilter( result );
		
		var respond:ResponderAMF0 = _methodResponderLookup[ methodName ];

		var asyncToken:AsyncToken = respond.getAsyncToken();

		var resultEvent:ResultEvent = new ResultEvent( ResultEvent.RESULT, 
										false, 
										true, 
										results, 
										respond.getAsyncToken(), 
										respond.getAsyncToken().message );

		if( asyncToken.hasResponder() ) {
			for( var i:int = 0; i < asyncToken.responders.length; i++ )
				asyncToken.responders[ i ].result( resultEvent );
		}

        dispatchEventHandler( resultEvent, methodName );		
			
		// remove Busy Cursor
		if( _showBusyCursor )
			CursorManager.removeBusyCursor();
					
    }

	/**
	 * 	@private
	 */
    private function setQueryFault( methodName:String, fault:Object ):void {
        var respond:ResponderAMF0 = _methodResponderLookup[ methodName ];
        var code:String = "unknown fault";
        if( fault.code != null )
        	code = String( fault.code );
        var faultString:String = "";
        if( fault.level != null )
        	faultString = String( "level: " + fault.level );
        var description:String = "";
        if( fault.description != null )
        	description = String( fault.description );            	            

		var faultEvent:FaultEvent = new FaultEvent( FaultEvent.FAULT, 
        								false, 
        								false, 
        								new Fault( code, faultString, description ),
        								respond.getAsyncToken(), 
        								respond.getAsyncToken().message );

		var asyncToken:AsyncToken = respond.getAsyncToken();

		if( asyncToken.hasResponder() ) {
			for( var i:int = 0; i < asyncToken.responders.length; i++ )
				asyncToken.responders[ i ].fault( faultEvent );
		}			
        
        dispatchEventHandler( faultEvent, methodName );
        
		// remove Busy Cursor
		if( _showBusyCursor )
			CursorManager.removeBusyCursor();
        								
    }

	/**
	 *  Handle any NetConnection status events.  Check info.level == "error" and send fault event.
	 */
	private function netStatusHandler( event:NetStatusEvent ):void
	{
		if( event.info.level == "error" ) {
			var faultEvent:FaultEvent = new FaultEvent( FaultEvent.FAULT, 
	        								false, 
	        								false, 
	        								new Fault( event.type, event.info.code, event.info.code ) );		
	        dispatchEvent( faultEvent );
  		}
	}
	
    /**
     *  If makeObjectsBindable filter.  If returned object is an Array auto make it an ArrayCollection
     */
    private function bindableFilter( result:Object ):* {
    	if( this.makeObjectsBindable ) {
        	if( result is Array ) {
        		return new ArrayCollection( ArrayUtil.toArray( result ) );
        	} else if( result is Object ) {
        		// Just do one level deep searching for Array types
        		for( var a:String in result ) {
        			if( result[ a ] is Array )
        				result[ a ] = new ArrayCollection( ArrayUtil.toArray( result[ a ] ) );
        		}
        	}
    	}
        return result;
    }	

	/**
	 * 	Make the ResultEvent or FaultEvent call to the main RemoteObjectAMF0
	 *  instance or any specific methods
	 */
	private function dispatchEventHandler( event:Event, methodName:String ):void
	{
		// Main class event call
		dispatchEvent( event );
		
		// Method Lookup and Dispatch Event for specific Method
		var methodReference:method;
		for each( var curMethod:method in methods ) {
			if( curMethod.name == methodName ) {
				methodReference = curMethod;
				break;
			}
		}
		if( methodReference ) {
			methodReference.dispatchEvent( event );
		}		
	}

								
}
}


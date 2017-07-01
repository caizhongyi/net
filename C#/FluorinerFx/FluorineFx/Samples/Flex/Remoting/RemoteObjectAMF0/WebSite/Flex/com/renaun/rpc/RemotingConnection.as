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

import flash.net.NetConnection;
import flash.net.ObjectEncoding;


/**
 *  The <code>RemotingConnection</code> class wraps the basic <code>NetConnection</code>
 * 	class to default the ObjectEncoding value to <code>ObjectEncoding.AMF0</code>.
 */
public class RemotingConnection extends NetConnection
{

    //--------------------------------------------------------------------------
    //
    //  Constructor
    //
    //--------------------------------------------------------------------------

    /**
     *  Constructor.
     */	
	public function RemotingConnection( sURL:String )
	{
		objectEncoding = ObjectEncoding.AMF0;
		if (sURL) connect( sURL );
	}
	
	public function AppendToGatewayUrl( s : String ) : void
	{
		//
	}

    public function AddHeader() : void
    {
    }
    
    public function ReplaceGatewayUrl() : void
    {
    }

	public function setCredentials( username:String, password:String ):void
    {    
    	super.addHeader( "Credentials", false, { userid:username, password:password } );
    }
	
}

}


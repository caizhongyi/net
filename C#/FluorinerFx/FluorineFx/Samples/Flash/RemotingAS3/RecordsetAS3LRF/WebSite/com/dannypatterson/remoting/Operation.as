//*******************************************************************************
//	Copyright (c) 2006 Patterson Consulting, Inc
//	All rights reserved.
//
// 	Redistribution and use in source and binary forms, with or without
// 	modification, are permitted provided that the following conditions are met:
//
// 		* Redistributions of source code must retain all references to Patterson
// 		  Consulting, Danny Patterson, com.dannypatterson and dannypatterson.com.
//		* Redistributions of source code must retain the above copyright
//		  notice, this list of conditions and the following disclaimer.
//		* Redistributions in binary form must reproduce the above copyright
//		  notice, this list of conditions and the following disclaimer in the
//		  documentation and/or other materials provided with the distribution.
//		* Neither the name of the Patterson Consulting nor the names of its
//		  contributors may be used to endorse or promote products derived from
//		  this software without specific prior written permission.
//
// 	THIS SOFTWARE IS PROVIDED BY THE REGENTS AND CONTRIBUTORS ``AS IS'' AND ANY
// 	EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// 	WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// 	DISCLAIMED. IN NO EVENT SHALL THE REGENTS AND CONTRIBUTORS BE LIABLE FOR ANY
// 	DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// 	(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// 	LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// 	ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// 	(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// 	SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//*******************************************************************************


package com.dannypatterson.remoting {

	import flash.net.NetConnection;
	import flash.net.Responder;
	import flash.events.EventDispatcher;
	import flash.utils.setTimeout;
	import flash.events.ErrorEvent;
	import flash.events.Event;
	
	/**
	 * @author Danny Patterson
	 * @version 1.1.0 2006-11-14
	 */	
	public class Operation extends EventDispatcher {
		
		public static const TIMEOUT:Number = 120000; // 2 minutes
		private var connection:NetConnection;
		private var operationPath:String;
		private var responder:Responder;
		private var args:Array;
		
		
		public function Operation(connection:NetConnection, operationPath:String, args:Array) {
			this.connection = connection;
			this.operationPath = operationPath;
			this.args = args;
			this.responder = new Responder(onResult, onFault);;
		}
		
		private function onFault(fault:Object):void {
			dispatchEvent(new FaultEvent(FaultEvent.FAULT, true, true, fault));
		}
		
		private function onResult(result:Object):void {
			dispatchEvent(new ResultEvent(ResultEvent.RESULT, true, true, result));
		}
		
		private function onTimeout():void {
			dispatchEvent(new FaultEvent(FaultEvent.CONNECTION_ERROR, true, true, {text:"Connection Error: Operation timed out!"}));
		}
		
		public function execute():void {
			var callArgs:Array = new Array(operationPath, responder);
			connection.call.apply(null, callArgs.concat(args));
			setTimeout(onTimeout, Operation.TIMEOUT);
		}
		
	}
	
}
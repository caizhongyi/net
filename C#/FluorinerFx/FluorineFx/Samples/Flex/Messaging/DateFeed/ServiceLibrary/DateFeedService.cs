using System;
using System.Collections;
using System.Threading;

using FluorineFx;
using FluorineFx.Messaging;
using FluorineFx.Messaging.Messages;

namespace ServiceLibrary
{
	/// <summary>
	/// Summary description for DateFeedService.
	/// </summary>
    [RemotingService]
	public class DateFeedService
	{
		private static Thread _thread;
		private static bool _isRunning = false;
		static object _objLock = new object();

        public DateFeedService()
		{
		}

		public void toggle()
		{
			lock(_objLock)
			{
				if(!_isRunning)
				{
					_isRunning = true;
					_thread = new Thread(new ThreadStart(Push));
					_thread.Start();
				}
				else
				{
					_isRunning = false;
					_thread.Join();
					_thread = null;        
				}
			}
		}

		private void Push()
		{
			while(_isRunning)
			{
				MessageBroker msgBroker = MessageBroker.GetMessageBroker(null);
				DateTime now = DateTime.Now;
				AsyncMessage msg = new AsyncMessage();
                msg.destination = "datefeed";
				msg.clientId = Guid.NewGuid().ToString("D");
				msg.messageId = Guid.NewGuid().ToString("D");
				msg.timestamp = Environment.TickCount;
				Hashtable body = new Hashtable();
				body.Add("userId", "daemon");
				body.Add("msg", now.ToString());
				msg.body = body;
				msgBroker.RouteMessage(msg);
				Thread.Sleep(1000);
			}
		}
	}
}

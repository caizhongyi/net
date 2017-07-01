using System;
using System.Collections;
using System.Threading;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Globalization;

using FluorineFx.Context;
using FluorineFx.Messaging;
using FluorineFx.Messaging.Services;
using FluorineFx.Messaging.Messages;
using FluorineFx.Messaging.Services.Messaging;

namespace ServiceLibrary
{
    class MarketFeedAdapter : MessagingAdapter
    {
        private Hashtable _watchList;
        private Hashtable _clients;
        private Thread _thread;


        public MarketFeedAdapter()
        {
            _watchList = new Hashtable();
            _clients = new Hashtable();

            _thread = new Thread(new ThreadStart(FeedThread));
            _thread.Start();
        }

        public override bool HandlesSubscriptions
        {
            get
            {
                return true;
            }
        }

        public override object Manage(CommandMessage commandMessage)
        {
            if (commandMessage.operation == CommandMessage.SubscribeOperation)
            {
                if (commandMessage.headers.ContainsKey(CommandMessage.SubtopicHeader))
                {
                    string symbol = commandMessage.headers[CommandMessage.SubtopicHeader] as string;
                    string clientId = commandMessage.clientId as string;
                    lock (_clients)
                    {
                        Hashtable clients = null;
                        if (!_watchList.Contains(symbol))
                        {
                            clients = new Hashtable();
                            _watchList[symbol] = clients;
                        }
                        else
                            clients = _watchList[symbol] as Hashtable;
                        clients[clientId] = null;

                        Hashtable watchlist = null;
                        if (!_clients.Contains(clientId))
                        {
                            watchlist = new Hashtable();
                            _clients[clientId] = watchlist;
                        }
                        else
                            watchlist = _clients[clientId] as Hashtable;
                        watchlist[symbol] = null;
                    }
                }
            }
            if (commandMessage.operation == CommandMessage.UnsubscribeOperation)
            {
                string clientId = commandMessage.clientId as string;
                lock (_clients)
                {
                    if (_clients.Contains(clientId))
                    {
                        Hashtable watchlist = _clients[clientId] as Hashtable;
                        if (watchlist != null)
                        {
                            foreach (string symbol in watchlist.Keys)
                            {
                                Hashtable clients = _watchList[symbol] as Hashtable;
                                if (clients.Contains(clientId))
                                    clients.Remove(clientId);
                                if (clients.Count == 0)
                                    _watchList.Remove(symbol);
                            }
                        }
                        _clients.Remove(clientId);
                    }
                }
            }
            return null;
        }

        private void FeedThread()
        {
            while (true)
            {
                StringBuilder sb = new StringBuilder();
                lock (_clients)
                {
                    if (_watchList.Count > 0)
                    {
                        foreach (string symbol in _watchList.Keys)
                        {
                            if (sb.Length > 0)
                                sb.Append(",");
                            sb.Append(symbol);
                        }

                        string url = "http://quote.yahoo.com/d/quotes.csv?f=snd1t1c1ohgl1v&s=" + sb.ToString();
                        Uri uri = new Uri(url);
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

                        request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705; .NET CLR 1.1.4322)";
                        request.Accept = "*/*";
                        request.KeepAlive = false;

                        try
                        {
                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                            Stream responseStream = response.GetResponseStream();
                            StreamReader streamReader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                            string text = streamReader.ReadToEnd();

                            responseStream.Close();
                            streamReader.Close();

                            string[] lines = text.Split(new char[] { '\n' });
                            //"MSFT","MICROSOFT CP","3/7/2006","4:00pm",+0.15,26.87,27.10,26.81,27.06,51632000
                            //"IBM","INTL BUSINESS MAC","3/7/2006","4:01pm",+0.29,80.01,80.79,79.86,80.29,4330800
                            Stock[] stocks = new Stock[lines.Length - 1];//last line is empty
                            DateTimeFormatInfo dateTimeFormatInfo = DateTimeFormatInfo.InvariantInfo;
                            NumberFormatInfo numberFormatInfo = NumberFormatInfo.InvariantInfo;
                            for (int i = 0; i < lines.Length - 1; i++)
                            {
                                string[] parts = lines[i].Split(new char[] { ',' });
                                string strDateTime = parts[2].Trim(new char[] { '\"' }) + " " + parts[3].Trim(new char[] { '\"' });

                                DateTime date = strDateTime.IndexOf("N/A") > 1 ? DateTime.Now : DateTime.Parse(strDateTime, dateTimeFormatInfo);
                                double change = parts[4] == "N/A" ? 0 : double.Parse(parts[4], numberFormatInfo);
                                double open = parts[5] == "N/A" ? 0 : double.Parse(parts[5], numberFormatInfo);
                                double low = parts[6] == "N/A" ? 0 : double.Parse(parts[6], numberFormatInfo);
                                double high = parts[7] == "N/A" ? 0 : double.Parse(parts[7], numberFormatInfo);
                                double last = parts[8] == "N/A" ? 0 : double.Parse(parts[8], numberFormatInfo);
                                double volume = parts[9] == "N/A" ? 0 : double.Parse(parts[9], numberFormatInfo);
                                Stock stock = new Stock(
                                    parts[0].Trim(new char[] { '\"' }),
                                    parts[1].Trim(new char[] { '\"' }),
                                    date,
                                    change,
                                    open,
                                    low,
                                    high,
                                    last,
                                    volume
                                    );
                                stocks[i] = stock;
                            }

                            foreach (Stock stock in stocks)
                            {
                                MessageBroker msgBroker = MessageBroker.GetMessageBroker(null);
                                AsyncMessage msg = new AsyncMessage();
                                msg.destination = "marketdata";
                                msg.headers.Add(AsyncMessage.SubtopicHeader, stock.symbol);

                                msg.headers.Add("Symbol", stock.symbol);
                                msg.headers.Add("Last", stock.last);
                                msg.headers.Add("High", stock.high);
                                msg.headers.Add("Low", stock.low);
                                msg.headers.Add("Open", stock.open);

                                msg.clientId = Guid.NewGuid().ToString("D");
                                msg.messageId = Guid.NewGuid().ToString("D");
                                msg.timestamp = Environment.TickCount;
                                msg.body = stock;
                                msgBroker.RouteMessage(msg);
                            }
                        }
                        catch (SocketException /*ex*/)
                        {
                        }
                        catch (Exception /*ex*/)
                        {
                        }
                    }
                }
                Thread.Sleep(1000);
            }
        }
    }
}

using System;
using FluorineFx.Context;
using FluorineFx.Messaging.Api;
using FluorineFx.Messaging.Adapter;
using FluorineFx.Messaging.Api.SO;
using FluorineFx.Messaging.Api.Service;
using FluorineFx.Messaging.Api.Stream;
using FluorineFx.Messaging.Rtmp.Stream;
using FluorineFx.Messaging.Api.Stream.Support;

namespace ServiceLibrary
{
    public class WebRadioApplication : ApplicationAdapter, IStreamAwareScopeHandler
    {
        public override bool AppStart(IScope application)
        {
            IServerStream stream = StreamUtils.CreateServerStream(this.Scope, "webradio");
            SimplePlayItem playItem = new SimplePlayItem();
            playItem.Name = "alienlounge.mp3";            
            stream.AddItem(playItem);
            playItem = new SimplePlayItem();
            playItem.Name = "cowboing.mp3";
            stream.AddItem(playItem);
            playItem = new SimplePlayItem();
            playItem.Name = "rhumbatism.mp3";
            stream.AddItem(playItem);
            playItem = new SimplePlayItem();
            playItem.Name = "splinteredlove.mp3";
            stream.AddItem(playItem);
            stream.IsRewind = true;
            stream.Start();
            //stream.SaveAs("test", false);
            return base.AppStart(application);
        }

        public override bool AppConnect(IConnection connection, object[] parameters)
        {
            return base.AppConnect(connection, parameters);
        }

        #region IStreamAwareScopeHandler Members

        public void StreamBroadcastClose(IBroadcastStream stream)
        {
        }

        public void StreamBroadcastStart(IBroadcastStream stream)
        {
        }

        public void StreamPlaylistItemPlay(IPlaylistSubscriberStream stream, IPlayItem item, bool isLive)
        {
        }

        public void StreamPlaylistItemStop(IPlaylistSubscriberStream stream, IPlayItem item)
        {
        }

        public void StreamPlaylistVODItemPause(IPlaylistSubscriberStream stream, IPlayItem item, int position)
        {
        }

        public void StreamPlaylistVODItemResume(IPlaylistSubscriberStream stream, IPlayItem item, int position)
        {
        }

        public void StreamPlaylistVODItemSeek(IPlaylistSubscriberStream stream, IPlayItem item, int position)
        {
        }

        public void StreamPublishStart(IBroadcastStream stream)
        {
        }

        public void StreamRecordStart(IBroadcastStream stream)
        {
        }

        public void StreamSubscriberClose(ISubscriberStream stream)
        {
        }

        public void StreamSubscriberStart(ISubscriberStream stream)
        {
        }

        #endregion
    }
}

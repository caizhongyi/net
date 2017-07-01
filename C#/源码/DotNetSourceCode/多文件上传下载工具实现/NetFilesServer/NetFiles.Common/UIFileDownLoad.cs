using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NetFiles.Common
{
    public partial class UIFileDownLoad : UserControl
    {
        public UIFileDownLoad()
        {
            InitializeComponent();
        }
        HFSoft.Net.Messages.IMessageChannel mChannel;
        System.IO.FileStream mStream;
        string mSaveFileName;
        public void Start(NetFiles.Command.Record record,string savepath)
        {
            try
            {
                mSaveFileName = savepath += "\\" + record.Name;
                txtFileName.Text ="обть[" + record.Name +"]     " + record.Size+"(byte)";

                NetFiles.Command.GET_FILE_REQUEST getfile = new NetFiles.Command.GET_FILE_REQUEST();
                getfile.FullName = record.FullName;
                HFSoft.Net.IChannel channel = NetFiles.Common.Config.ClientChannel(null);

                mChannel = new HFSoft.Net.Messages.MessageChannel(channel, new HFSoft.Net.Messages.EventMessageReceive(
                    readmessage));
                mChannel.Channel.ChannelError += new HFSoft.Net.EventChannelError(onError);
                mChannel.Send(getfile);
            }
            catch (Exception e_)
            {
                HFSoft.ExceptionHandler.Disposal(e_, this.FindForm());
                Parent.Controls.Remove(this);
            }
            
            

        }
        private void readmessage(object source, HFSoft.Net.Messages.EventMessageReceiveArgs e)
        {
            HFSoft.Net.Messages.IMessage message = e.Message;
            if (message is NetFiles.Command.SEND_FILE)
            {
                NetFiles.Command.SEND_FILE sf = (NetFiles.Command.SEND_FILE)message;
                if (sf.State == NetFiles.Command.FileState.Start)
                {

                    this.Invoke(new SetProgressBar(ProgressBarChange), sf.Count);
                    mStream = System.IO.File.Open(mSaveFileName, System.IO.FileMode.Create);
                }
                NetFiles.Command.SEND_FILE.SaveFile(sf, mStream);
                this.Invoke(new SetProgressBar(ProgressBarChange), 0);
                
                if (sf.State == NetFiles.Command.FileState.End || sf.Count == 1)
                {

                    this.Invoke(new HFSoft.Net.EventChannelError(onErrordisposal), source, null);
                }
                
                
            }
        }
        delegate void SetProgressBar(int value);
        void ProgressBarChange(int value)
        {
            if (value == 0)
            {
                progressBar1.Value++;
            }
            else
            {
                progressBar1.Maximum = value;
            }
        }

        private void onError(object source, HFSoft.Net.EventChannelErrorArgs e)
        {
            this.Invoke(new HFSoft.Net.EventChannelError(onErrordisposal), source, e);
            
        }
        private void onErrordisposal(object source, HFSoft.Net.EventChannelErrorArgs e)
        {

                
              
                mChannel.Channel.ChannelError -= new HFSoft.Net.EventChannelError(onError);
                if (e != null)
                {
                    HFSoft.ExceptionHandler.Disposal(e.Error, this.FindForm());
                    Parent.Controls.Remove(this);

                }


                if (mStream != null)
                {
                    mStream.Flush();
                    mStream.Close();
                    mStream = null;
                }
                cmdComplete.Enabled = true;
              mChannel.Channel.Close();
            
            
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            
                mChannel.Channel.Close();
                Parent.Controls.Remove(this);
        }

       

        private void cmdComplete_Click(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
        }

        private void UIFileDownLoad_Load(object sender, EventArgs e)
        {

        }
    }
}

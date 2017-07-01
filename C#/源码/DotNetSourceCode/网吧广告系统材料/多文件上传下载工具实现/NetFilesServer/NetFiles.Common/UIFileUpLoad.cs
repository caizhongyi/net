using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NetFiles.Common
{
    public partial class UIFileUpLoad : UserControl
    {
        public UIFileUpLoad()
        {
            InitializeComponent();
        }
        HFSoft.Net.IChannel mChannel;
        public void Start(NetFiles.Command.Record record, string filename)
        {
            try
            {
                txtFileName.Text = "上传文件[" + filename + "]";
                mChannel = NetFiles.Common.Config.ClientChannel(null);
                int count = NetFiles.Common.ExecuteCommand.PackageCount(filename);
                int index = 0;
                mChannel.Completed += delegate(object source, EventArgs e)
                {
                    
                    index++;
                    this.Invoke(new EventHandler(ChangeState), null, null);
                    if (index == count)
                        mChannel.Close();
                };
                progressBar1.Maximum = count;
                mChannel.ChannelError += new HFSoft.Net.EventChannelError(onError);
                ExecuteCommand.SEND_FILE(mChannel, filename, record,System.IO.Path.GetFileName(filename));

            }
            catch (Exception e_)
            {
                HFSoft.ExceptionHandler.Disposal(e_, this.FindForm());
                Parent.Controls.Remove(this);
            }
        }
        private void ChangeState(object source, EventArgs e)
        {
            progressBar1.Value++;
        }

        private void onError(object source, HFSoft.Net.EventChannelErrorArgs e)
        {
            this.Invoke(new HFSoft.Net.EventChannelError(onErrordisposal), source, e);

        }
        private void onErrordisposal(object source, HFSoft.Net.EventChannelErrorArgs e)
        {
            cmdComplete.Enabled = true;
            mChannel.Close();
        }

        private void cmdComplete_Click(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            mChannel.Close();
            Parent.Controls.Remove(this);
        }
    }
}

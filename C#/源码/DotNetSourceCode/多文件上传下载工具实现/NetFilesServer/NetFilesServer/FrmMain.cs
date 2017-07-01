using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NetFiles.DataAccess;
using NetFiles.DataAccess.Entities;
namespace NetFilesServer
{
    public partial class FrmMain : Form
    {
        NetFiles.Common.IViewData<BootInfo> mViewData;
        
        public FrmMain()
        {
            InitializeComponent();
            //定义数据绑定对象
            mViewData = new NetFiles.Common.DataViews.BootView(listDirectory);
            mViewData.ViewDataBound += delegate(BootInfo item, ListViewItem vitem)
            {
                //定义数据绑定处理匿名函数
            };
            mViewData.SelectChange += delegate(BootInfo item, ListViewItem vitem)
            {
                if (item != null)
                {
                    NetFiles.Common.Config.ControlEnable(true, cmdDisable, cmdDeldirectory);
                }
                else

                {
                    NetFiles.Common.Config.ControlEnable(false, cmdDisable, cmdDeldirectory);
                }
            };
        }
        private void LoadDirectory()
        {
            IBoot boot = AccessFactory.CreateBoot();
            
            //数据绑定输出
            mViewData.DataBind(boot.List());
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadDirectory();
        }
        //添加服务目录
        private void cmdAdddirectory_Click(object sender, EventArgs e)
        {
            FrmDirectory frmd = new FrmDirectory();
            if (frmd.ShowDialog(this) == DialogResult.OK)
            {
                listDirectory.Items.Add(mViewData.CreateItem(frmd.BootInfo));
            }
        }
        //启动服务
        private void cmdStart_Click(object sender, EventArgs e)
        {
            try
            {
                NetFiles.Common.Config.Server.Start();
                NetFiles.Common.Config.Server.ChannelReceive += new HFSoft.Net.EventReceive(
                    Command);
                NetFiles.Common.Config.Server.ChannelError += new HFSoft.Net.EventChannelError(OnError);
                NetFiles.Common.Config.Server.PoolChange += new HFSoft.Net.EventPoolChange(OnConnectionChange);
            }
            catch (Exception e_)
            {
                HFSoft.ExceptionHandler.Disposal(e_, this);
            }
            finally
            {
                
                NetFiles.Common.Config.ControlEnable(false, cmdStart);
                NetFiles.Common.Config.ControlEnable(true, cmdStop);
                
            }
        }

        private void Command(object source, HFSoft.Net.EventReceiveArgs e)
        {
            HFSoft.Net.Messages.IMessage message = HFSoft.Net.Messages.MessageChannel.GetMessage(e.Data);
            if (message == null)
                e.Channel.Close();
            if (message is NetFiles.Command.GET_FILE_REQUEST)
            {
                NetFiles.Common.ExecuteCommand.SEND_FILE(e.Channel, ((NetFiles.Command.GET_FILE_REQUEST)message).FullName);
            }
            if (message is NetFiles.Command.LIST_FILE_REQUEST)
            {
                NetFiles.Common.ExecuteCommand.LIST_FILES(e.Channel, (NetFiles.Command.LIST_FILE_REQUEST)message);
            }
            if (message is NetFiles.Command.SEND_FILE)
            {
                SaveFild((NetFiles.Command.SEND_FILE)message,e.Channel);
            }
        }
        Dictionary<string, System.IO.Stream> mFileStreams = new Dictionary<string, System.IO.Stream>();
        private void SaveFild(NetFiles.Command.SEND_FILE sf,HFSoft.Net.IChannel channel)
        {
            System.IO.Stream stream;
            if (mFileStreams.ContainsKey(channel.ID))
            {
                stream = mFileStreams[channel.ID];
            }
            else
            {
                stream = System.IO.File.Open(sf.Folder.FullName + "\\" + sf.Name, System.IO.FileMode.Create);
                mFileStreams.Add(channel.ID, stream);
            }
            NetFiles.Command.SEND_FILE.SaveFile(sf, stream);

            if (sf.State == NetFiles.Command.FileState.End || sf.Count == 1)
            {

                stream.Flush();
                stream.Close();
                mFileStreams.Remove(channel.ID);
            }
        }

        protected void OnConnectionChange(object source, HFSoft.Net.EventPoolChangeArgs e)
        {
            if (mFileStreams.ContainsKey(e.Channel.ID))
            {
                mFileStreams[e.Channel.ID].Close();
                mFileStreams.Remove(e.Channel.ID);
            }
        }

        protected void OnError(object source, HFSoft.Net.EventChannelErrorArgs e)
        {
            if (e.Error is System.Net.Sockets.SocketException)
            {

            }
            else
            {
                if (e.Channel.State == HFSoft.Net.ChannelState.Connected)
                {
                    NetFiles.Command.REQUEST_ERROR error = new NetFiles.Command.REQUEST_ERROR();
                    error.Exception = e.Error.Message;
                    using (HFSoft.Net.Messages.MessageChannel mc = new HFSoft.Net.Messages.MessageChannel(e.Channel))
                    {
                        mc.Send(error);
                    }
                }
            }
            
        }
        //停止服务
        private void cmdStop_Click(object sender, EventArgs e)
        {
            try
            {
                NetFiles.Common.Config.Server.Stop();
            }
            catch (Exception e_)
            {
                HFSoft.ExceptionHandler.Disposal(e_, this);
            }
            finally
            {
                NetFiles.Common.Config.ControlEnable(true, cmdStart);
                NetFiles.Common.Config.ControlEnable(false, cmdStop);
            }
        }

        //编辑服务目录
        private void cmdDisable_Click(object sender, EventArgs e)
        {
            FrmDirectory frmd = new FrmDirectory();
            frmd.BootInfo = mViewData.SelectItem;
            if (frmd.ShowDialog(this) == DialogResult.OK)
            {
                LoadDirectory();
            }
        }

        private void cmdDeldirectory_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "是否要删除[" + mViewData.SelectItem.BootDirectory + "]服务目录?", "删除数据"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                IBoot boot = AccessFactory.CreateBoot();
                boot.Delete(mViewData.SelectItem.BootDirectory);
                LoadDirectory();
            }
        }
        //退出程序
        private void cmdExit_Click(object sender, EventArgs e)
        {
            try
            {
                NetFiles.Common.Config.Server.Stop();
            }
            catch
            {
            }
            finally
            {
                Application.Exit();
            }
        }

       
    }
}
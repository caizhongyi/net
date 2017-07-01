using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NetFileClient
{
    public partial class Form1 : Form
    {

        private NetFiles.Common.DataViews.RecordView mRecordView;

        private NetFiles.Command.Record mActRecord = null;

        public Form1()
        {
            InitializeComponent();
            mRecordView = new NetFiles.Common.DataViews.RecordView(listView1);
            mRecordView.ViewDataBound += delegate(NetFiles.Command.Record item, ListViewItem vitem)
            {
                if (item.Type == NetFiles.Command.RecoreType.Folder)
                {
                    vitem.ImageIndex = 0;
                }
                else
                {
                    vitem.ImageIndex = 1;
                }
            };
            mRecordView.SelectChange += delegate(NetFiles.Command.Record item, ListViewItem vitem)
            {
                if (item != null)
                    cmdDownLoad.Enabled = true;
                else
                    cmdDownLoad.Enabled = false;
            };
        }

        //获取相关服务目录
        private void cmdLoadService_Click(object sender, EventArgs e)
        {
            HFSoft.Net.IChannel channel = null;
            try
            {
                channel = NetFiles.Common.Config.ClientChannel(new System.Threading.ManualResetEvent(false));
                NetFiles.Command.LIST_FILE_RESPONSE response;
                using (HFSoft.Net.Messages.MessageChannel mc = new HFSoft.Net.Messages.MessageChannel(channel))
                {
                    mc.Send(new NetFiles.Command.LIST_FILE_REQUEST());
                    if (channel.Error != null)
                    {
                        HFSoft.ExceptionHandler.Disposal(channel.Error, this);
                        return;
                    }
                    if (mc.Message is NetFiles.Command.REQUEST_ERROR)
                    {
                        NetFiles.Command.REQUEST_ERROR error = (NetFiles.Command.REQUEST_ERROR)mc.Message;
                        throw (new Exception(error.Exception));

                    }
                    response = (NetFiles.Command.LIST_FILE_RESPONSE)mc.Message;
                }
                mActRecord = null;
                treeView1.Nodes.Clear();
                foreach (NetFiles.Command.Record item in response.Records)
                {
                    TreeNode node = new TreeNode(item.Name);
                    node.Tag = item;
                    treeView1.Nodes.Add(node);
                }
                cmdUpLoad.Enabled = false;
            }
            catch (Exception e_)
            {
                HFSoft.ExceptionHandler.Disposal(e_, this);
            }
            finally
            {
                if (channel != null)
                    channel.Close();
            }
        }


        private void LoadFiles(NetFiles.Command.Record record)
        {
            mActRecord = record;
            NetFiles.Command.LIST_FILE_REQUEST request = new NetFiles.Command.LIST_FILE_REQUEST();
            request.Folder = record;
            HFSoft.Net.IChannel channel = null;
            HFSoft.Net.Messages.IMessage message;
            try
            {
                channel = NetFiles.Common.Config.ClientChannel(new System.Threading.ManualResetEvent(false));
                using (HFSoft.Net.Messages.MessageChannel mc = new HFSoft.Net.Messages.MessageChannel(channel))
                {
                    mc.Send(request);
                    if (channel.Error != null)
                    {
                        HFSoft.ExceptionHandler.Disposal(channel.Error, this);
                        return;
                    }
                    message = mc.Message;
                    if (message is NetFiles.Command.REQUEST_ERROR)
                    {
                        throw (new Exception(((NetFiles.Command.REQUEST_ERROR)message).Exception));

                    }
                    mRecordView.DataBind(((NetFiles.Command.LIST_FILE_RESPONSE)message).Records);

                }
                NetFiles.Common.Config.ControlEnable(true, cmdUpLoad);
            }
            catch (Exception e_)
            {
                HFSoft.ExceptionHandler.Disposal(e_, this);
            }
            finally
            {
                if (channel != null)
                    channel.Close();
            }
        }

        //服务目录选择后
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            NetFiles.Command.Record record = (NetFiles.Command.Record)e.Node.Tag;
            LoadFiles(record);

        }

        private void cmdDownLoad_Click(object sender, EventArgs e)
        {
            
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                NetFiles.Command.Record record;
                NetFiles.Common.UIFileDownLoad download;
                foreach (ListViewItem item in listView1.SelectedItems)
                {

                    record = (NetFiles.Command.Record)item.Tag;
                    if (record.Type == NetFiles.Command.RecoreType.File)
                    {
                        download = new NetFiles.Common.UIFileDownLoad();
                        download.Dock = DockStyle.Top;
                        groupBox2.Controls.Add(download);
                        download.Start(record, folderBrowserDialog1.SelectedPath);
                    }
                }
            }
        }

        //双击项目
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (mRecordView.SelectItem != null)
            {
                if (mRecordView.SelectItem.Type == NetFiles.Command.RecoreType.Folder)
                {
                    LoadFiles(mRecordView.SelectItem);
                }
            }
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {

            if (mActRecord != null)
            {
                LoadFiles(mActRecord);
            }
        }

        private void cmdUpLoad_Click(object sender, EventArgs e)
        {
            if (mActRecord == null)
            {
                MessageBox.Show(this, "不确定当前目录！");
                return;
            }
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                foreach (string item in openFileDialog1.FileNames)
                {
                    NetFiles.Common.UIFileUpLoad upload = new NetFiles.Common.UIFileUpLoad();
                    upload.Dock = DockStyle.Top;
                    groupBox2.Controls.Add(upload);
                    upload.Start(mActRecord, item);
                }
            }
        }
    }
}
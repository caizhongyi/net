using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FtpClientDll_cs;
using System.Runtime.InteropServices;

namespace FtpClient_cs
{
    public partial class FTPClientForm : Form
    {
        private FTPClient _client;
        private string _strLocalDir = "E:\\";
        private string _strRemoteDir = "/";

        public FTPClientForm()
        {
            InitializeComponent();

            textMsg.Text = "提示信息： ";

            listLocal.Columns.Add("文件名", 135, HorizontalAlignment.Left);
            listLocal.Columns.Add("文件大小", 65, HorizontalAlignment.Right);
            RefreshLocalList();

            listRemote.Columns.Add("文件名", 135, HorizontalAlignment.Left);
            listRemote.Columns.Add("文件大小", 65, HorizontalAlignment.Right);

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (_client != null)
                return;
            _client = new FTPClient();
            _client.RemoteHost = textHost.Text.Trim();
            _client.RemoteUser = textUser.Text.Trim();
            _client.RemotePass = textPass.Text.Trim();
            _client.RemotePort = int.Parse(textPort.Text.Trim());
            _client.RemotePath = "/";
            _client.Connect();
            RefreshRemoteList();
            textMsg.Text += "\r\n" + "连接 " + textHost.Text.TrimEnd("\r\n".ToCharArray()) + " 成功";
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (_client == null)
                return;
            _client.DisConnect();
            _client = null;

            listRemote.Items.Clear();
            _strRemoteDir = "/";
            textRemote.Text = "";

            textMsg.Text += "\r\n" + "已经从服务器断开";
        }

        private void RefreshRemoteList()
        {
            string[] list, item;
            ListViewItem lvi;
            string[] uper = { "..", "<DIR>" };
            if (_client == null)
            {
                return;
            }

            _client.ChDir(_strRemoteDir);
            list = _client.List("");

            listRemote.Items.Clear();
            if (_strRemoteDir.Length > 1)
            {
                listRemote.Items.Add(new ListViewItem(uper));
            }
            foreach (string str in list)
            {
                if (str.Contains("<DIR>"))
                {
                    item = str.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    lvi = new ListViewItem();
                    lvi.SubItems[0].Text = item[3];
                    lvi.SubItems.Add(item[2]);
                    listRemote.Items.Add(lvi);
                }
                else
                {
                    item = str.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    lvi = new ListViewItem();
                    lvi.SubItems[0].Text = item[3];
                    lvi.SubItems.Add(item[2]);
                    listRemote.Items.Add(lvi);
                }
            }
            textRemote.Text = _strRemoteDir;
        }

        private void RefreshLocalList()
        {
            listLocal.Items.Clear();
            string[] uper = { "..", "<DIR>" };
            if (_strLocalDir.Length > 3)
            {
                listLocal.Items.Add(new ListViewItem(uper));
            }
            DirectoryInfo dir = new DirectoryInfo(_strLocalDir);
            try
            {
                foreach (DirectoryInfo d in dir.GetDirectories())
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.SubItems[0].Text = d.Name;
                    lvi.SubItems.Add("<DIR>");
                    listLocal.Items.Add(lvi);
                }
                foreach (FileInfo f in dir.GetFiles())
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.SubItems[0].Text = f.Name;
                    lvi.SubItems.Add(f.Length.ToString());
                    listLocal.Items.Add(lvi);
                }
            }
            catch
            {
                MessageBox.Show("无法显示此文件夹的内容");
            }
            textLocal.Text = _strLocalDir;
        }

        private void textLocal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13)
                return;
            if (!Directory.Exists(textLocal.Text))
                return;
            _strLocalDir = textLocal.Text;
            if (_strLocalDir[_strLocalDir.Length - 1] != '\\')
            {
                _strLocalDir += '\\';
            }
            RefreshLocalList();
        }

        private void listLocal_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string name, type;
            ListViewItem lvi;
            lvi = listLocal.SelectedItems[0];
            name = lvi.SubItems[0].Text;
            type = lvi.SubItems[1].Text;
            if (type == "<DIR>")
            {
                if (name == "..")
                {
                    LocalBack();
                }
                else
                {
                    _strLocalDir += name;
                    _strLocalDir += '\\';
                    RefreshLocalList();
                }
            }
            else
            {
                if (_client == null)
                {
                    MessageBox.Show("还未连接服务器", "提示");
                    return;
                }
                try
                {
                    _client.Put(_strLocalDir + name);                   
                    textMsg.Text += "\r\n" + "已经上传文件：" + name;
                    RefreshRemoteList();
                }
                catch 
                {
                    MessageBox.Show("服务器当前目录没有写入权限！", "错误");
                }
            }
        }

        private void listRemote_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string name, type;
            ListViewItem lvi;
            lvi = listRemote.SelectedItems[0];
            name = lvi.SubItems[0].Text;
            type = lvi.SubItems[1].Text;
            if (type == "<DIR>")
            {
                if (name == "..")
                {
                    RemoteBack();
                }
                else
                {
                    _strRemoteDir += name;
                    _strRemoteDir += '/';
                    RefreshRemoteList();
                }
                textMsg.Text += "\r\n" + "进入目录：" + _strRemoteDir;
            }
            else
            {
                _client.Get(name, _strLocalDir, name);
                textMsg.Text += "\r\n" + "已经下载文件："+name;
                RefreshLocalList();
            }
        }

        private void LocalBack()
        {
            _strLocalDir = _strLocalDir.TrimEnd("\\".ToCharArray());
            int flag = 0, pos = 0;
            while (flag != -1)
            {
                pos = flag + 1;
                flag = _strLocalDir.IndexOf('\\', pos);
            }
            _strLocalDir = _strLocalDir.Remove(pos);
            RefreshLocalList();
        }

        private void RemoteBack()
        {
            _strRemoteDir = _strRemoteDir.TrimEnd("/".ToCharArray());
            int flag = 0, pos = 0;
            while (flag != -1)
            {
                pos = flag + 1;
                flag = _strRemoteDir.IndexOf('/', pos);
            }
            _strRemoteDir = _strRemoteDir.Remove(pos);
            RefreshRemoteList();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            string name, type;
            foreach (ListViewItem lvi in listRemote.SelectedItems)
            {
                name = lvi.SubItems[0].Text;
                type = lvi.SubItems[1].Text;
                if (type.Contains("<DIR>"))
                    continue;
                _client.Get(name, _strLocalDir, name);
                textMsg.Text += "\r\n" + "已经下载文件：" + name;
                RefreshLocalList();
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            string name, type;

            if (_client == null)
            {
                MessageBox.Show("还未连接服务器", "提示");
                return;
            }
            foreach(ListViewItem lvi in listLocal.SelectedItems)
            {
                name = lvi.SubItems[0].Text;
                type = lvi.SubItems[1].Text;
                if (type.Contains("<DIR>"))
                    continue;
                try
                {
                    _client.Put(_strLocalDir + name);
                    textMsg.Text += "\r\n" + "已经上传文件：" + name;
                    RefreshRemoteList();
                }
                catch
                {
                    MessageBox.Show("服务器当前目录没有写入权限！", "错误");
                    return;
                }
            }
        }
    }
}
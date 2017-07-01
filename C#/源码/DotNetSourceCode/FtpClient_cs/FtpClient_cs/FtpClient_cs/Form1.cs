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

            textMsg.Text = "��ʾ��Ϣ�� ";

            listLocal.Columns.Add("�ļ���", 135, HorizontalAlignment.Left);
            listLocal.Columns.Add("�ļ���С", 65, HorizontalAlignment.Right);
            RefreshLocalList();

            listRemote.Columns.Add("�ļ���", 135, HorizontalAlignment.Left);
            listRemote.Columns.Add("�ļ���С", 65, HorizontalAlignment.Right);

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
            textMsg.Text += "\r\n" + "���� " + textHost.Text.TrimEnd("\r\n".ToCharArray()) + " �ɹ�";
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

            textMsg.Text += "\r\n" + "�Ѿ��ӷ������Ͽ�";
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
                MessageBox.Show("�޷���ʾ���ļ��е�����");
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
                    MessageBox.Show("��δ���ӷ�����", "��ʾ");
                    return;
                }
                try
                {
                    _client.Put(_strLocalDir + name);                   
                    textMsg.Text += "\r\n" + "�Ѿ��ϴ��ļ���" + name;
                    RefreshRemoteList();
                }
                catch 
                {
                    MessageBox.Show("��������ǰĿ¼û��д��Ȩ�ޣ�", "����");
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
                textMsg.Text += "\r\n" + "����Ŀ¼��" + _strRemoteDir;
            }
            else
            {
                _client.Get(name, _strLocalDir, name);
                textMsg.Text += "\r\n" + "�Ѿ������ļ���"+name;
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
                textMsg.Text += "\r\n" + "�Ѿ������ļ���" + name;
                RefreshLocalList();
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            string name, type;

            if (_client == null)
            {
                MessageBox.Show("��δ���ӷ�����", "��ʾ");
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
                    textMsg.Text += "\r\n" + "�Ѿ��ϴ��ļ���" + name;
                    RefreshRemoteList();
                }
                catch
                {
                    MessageBox.Show("��������ǰĿ¼û��д��Ȩ�ޣ�", "����");
                    return;
                }
            }
        }
    }
}
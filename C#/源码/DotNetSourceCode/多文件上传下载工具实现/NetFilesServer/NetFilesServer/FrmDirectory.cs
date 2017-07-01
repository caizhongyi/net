using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NetFiles.DataAccess.Entities;
using HFSoft.Data;
namespace NetFilesServer
{
    public partial class FrmDirectory : Form
    {
        public FrmDirectory()
        {
            InitializeComponent();
        }
        //ѡ��Ŀ¼�¼�
        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                txtfolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void FrmDirectory_Load(object sender, EventArgs e)
        {
            txtState.SelectedIndex = 0;
        }
        //ȡ���¼�
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private BootInfo mBootInfo = null;
        /// <summary>
        /// ��ȡʵ�����
        /// </summary>
        public BootInfo BootInfo
        {
            get
            {
                return mBootInfo;
            }
            set
            {
                mBootInfo = value;
                BootInfoBinder.Export(value);
            }
        }

        //���ݱ����¼�
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                NetFiles.DataAccess.IBoot boot = NetFiles.DataAccess.AccessFactory.CreateBoot();
                if (BootInfo == null)
                {
                    BootInfo = BootInfoBinder.Import();
                    boot.Add(BootInfo);
                }
                else
                {
                    BootInfoBinder.Import(BootInfo);
                    boot.Edit(BootInfo);
                }
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception e_)
            {
                HFSoft.ExceptionHandler.Disposal(e_, this);
            }
        }
        private ObjectDataBinder<BootInfo> mBootInfoBinder = null;
        protected ObjectDataBinder<BootInfo> BootInfoBinder
        {
            get
            {
                if (mBootInfoBinder == null)
                {
                    mBootInfoBinder = new ObjectDataBinder<BootInfo>();
                    StringChanger directory = new StringChanger();
                    directory.Min = 1;
                    directory.CastException = "��ѡ������Ŀ¼!";
                    mBootInfoBinder.AddMapper(txtfolder,"Text", "BootDirectory",directory);
                    mBootInfoBinder.AddMapper(txtState,"SelectedIndex", "State");
                    mBootInfoBinder.AddMapper(txtRemark, "Text", "Remark");
                }
                return mBootInfoBinder;
            }
        }
        
    }
}
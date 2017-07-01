using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WbSystem
{
    public partial class DelOrUpdateNewsInfo : Form
    {
        NewsInfo ni = new NewsInfo();
        XmlDocument xmlDoc=new XmlDocument();
        public DelOrUpdateNewsInfo()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            RreshNewForm();
        }
        void RreshNewForm()
        {
            NewsInfoForm nif = new NewsInfoForm();
            nif.Show();

            this.Close();
        }
        private void DelOrUpdateNewsInfo_Load(object sender, EventArgs e)
        {
            this.cbNewsInfo.SelectedIndex = 0;

            txtTitle.Text = ni.Newstext;
            txtContent.Text = ni.Wb_Adv_Content;
            txtURL.Text = ni.Wb_Adv_Url;
        }

        private void cbNewsInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cbinfo = cbNewsInfo.SelectedIndex;
            switch (cbinfo)
            {
                case 0:
                    this.txtTitle.ReadOnly = true;
                    this.txtContent.ReadOnly = true;
                    this.txtURL.ReadOnly = true;
                    break;
                case 1:
                    this.txtTitle.ReadOnly = false;
                    this.txtContent.ReadOnly = true;
                    this.txtURL.ReadOnly = true;
                    break;
                case 2:
                    this.txtTitle.ReadOnly = true;
                    this.txtContent.ReadOnly = true;
                    this.txtURL.ReadOnly = false;
                    break;
                case 3:
                    this.txtTitle.ReadOnly = true;
                    this.txtContent.ReadOnly = false;
                    this.txtURL.ReadOnly = true;
                    break;                  
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DelXML();
        }
        //�޸�
        void UpdateXML()
        {
            xmlDoc.Load("NewsInfo.xml");
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("NewDataSet").ChildNodes;//��ȡbookstore�ڵ�������ӽڵ�
            foreach (XmlNode xn in nodeList)//���������ӽڵ�
            {
                XmlElement xe = (XmlElement)xn;//���ӽڵ�����ת��ΪXmlElement����
                XmlNodeList nls = xe.ChildNodes;//������ȡxe�ӽڵ�������ӽڵ�
                foreach (XmlNode xn1 in nls)//����
                {
                    XmlElement xe2 = (XmlElement)xn1;//ת������
                    if (xe2.InnerText == ni.Newstext)//����ҵ�
                    {
                        xe2.InnerText = txtTitle.Text.Trim();
                    }
                }
               
            }
            xmlDoc.Save("NewsInfo.xml");//���档

        }
        //ɾ��
        void DelXML()
        {
            if (MessageBox.Show("ȷʵҪɾ������Ϣ��", "hello", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                xmlDoc.Load("NewsInfo.xml");
                XmlNodeList xnl = xmlDoc.SelectSingleNode("NewDataSet").ChildNodes;
                foreach (XmlNode xn in xnl)
                {
                    XmlElement xe = (XmlElement)xn;
                    XmlNodeList nls = xe.ChildNodes;//������ȡxe�ӽڵ�������ӽڵ�
                    foreach (XmlNode xn1 in nls)//����
                    {
                        XmlElement xe2 = (XmlElement)xn1;//ת������
                        if (xe2.InnerText == ni.Newstext)//����ҵ�
                        {
                            XmlNode xn3 = xn1.ParentNode;
                            XmlElement xea = (XmlElement)xn3;
                            xea.ParentNode.RemoveChild(xea);
                        }
                    }
                }
                xmlDoc.Save("NewsInfo.xml");//���档
                RreshNewForm();
            }
        }

        private void btnMod_Click(object sender, EventArgs e)
        {
            UpdateXML();
        }
    }
}
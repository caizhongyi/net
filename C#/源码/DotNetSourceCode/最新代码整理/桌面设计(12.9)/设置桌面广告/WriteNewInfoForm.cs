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
    public partial class WriteNewInfoForm : Form
    {
        NewsInfo ni = new NewsInfo();
        XmlDocument xmlDoc=new XmlDocument();
        public WriteNewInfoForm()
        {
            InitializeComponent();
        }

        private void btnKeep_Click(object sender, EventArgs e)
        {
            //���浽xml
            InsertXML();
            //���
            this.txtContent.Text = "";
            this.txtTitle.Text = "";
            this.txtURL.Text = "";
            //������������
            NewsInfoForm nif = new NewsInfoForm();
            nif.Show();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            NewsInfoForm nif = new NewsInfoForm();
            nif.Show();
        }
        void InsertXML()
        {
            xmlDoc.Load("NewsInfo.xml");
            XmlNode root = xmlDoc.SelectSingleNode("NewDataSet");
            XmlElement xe1 = xmlDoc.CreateElement("WbAdvInfo");


            XmlElement xesub1 = xmlDoc.CreateElement("Wb_Adv_Name");
            xesub1.InnerText = txtTitle.Text.Trim();//�����ı��ڵ�
            xe1.AppendChild(xesub1);//��ӵ�<book>�ڵ���

            XmlElement xesub2 = xmlDoc.CreateElement("Wb_Adv_Id");
            xesub2.InnerText = DateTime.Now.ToString().Replace(" ", "").Replace(":", "").Replace("-","");//�����ı��ڵ�
            xe1.AppendChild(xesub2);//��ӵ�<book>�ڵ���

            XmlElement xesub3 = xmlDoc.CreateElement("Wb_Adv_Url");
            xesub3.InnerText = txtURL.Text.Trim();
            xe1.AppendChild(xesub3);

            XmlElement xesub4 = xmlDoc.CreateElement("Wb_Adv_Content");
            xesub4.InnerText = txtContent.Text;
            xe1.AppendChild(xesub4);

            XmlElement xesub5 = xmlDoc.CreateElement("Wb_Id");
            xesub5.InnerText = DateTime.Now.ToString();
            xe1.AppendChild(xesub5);

            XmlElement xesub6 = xmlDoc.CreateElement("Adv_Time");
            xesub6.InnerText = DateTime.Now.ToString();//�����ı��ڵ�
            xe1.AppendChild(xesub6);//��ӵ�<book>�ڵ���


            root.AppendChild(xe1);//��ӵ�<bookstore>�ڵ���
            xmlDoc.Save("NewsInfo.xml");
        }
    }
}
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
            //保存到xml
            InsertXML();
            //清空
            this.txtContent.Text = "";
            this.txtTitle.Text = "";
            this.txtURL.Text = "";
            //重新启动窗口
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
            xesub1.InnerText = txtTitle.Text.Trim();//设置文本节点
            xe1.AppendChild(xesub1);//添加到<book>节点中

            XmlElement xesub2 = xmlDoc.CreateElement("Wb_Adv_Id");
            xesub2.InnerText = DateTime.Now.ToString().Replace(" ", "").Replace(":", "").Replace("-","");//设置文本节点
            xe1.AppendChild(xesub2);//添加到<book>节点中

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
            xesub6.InnerText = DateTime.Now.ToString();//设置文本节点
            xe1.AppendChild(xesub6);//添加到<book>节点中


            root.AppendChild(xe1);//添加到<bookstore>节点中
            xmlDoc.Save("NewsInfo.xml");
        }
    }
}
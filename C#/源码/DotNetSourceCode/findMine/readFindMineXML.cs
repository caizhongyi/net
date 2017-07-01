using System;
using System.Xml;
using System.Collections;
using System.IO;
using System.Windows.Forms;
namespace findMine
{
	/// <summary>
	/// Class1 ��ժҪ˵����
	/// </summary>
	public class UserInfo
	{
		private string userScore="999";
		private string userName="����";
		private string userDate="0000-00-00";
		public string UserScore//����û��()������Pascal������
		{
			get//get����
			{
				return userScore;
			}
			set//set����
			{
				this.userScore = value;
			}
		}
		public string UserName//����û��()������Pascal������
		{
			get//get����
			{
				return userName;
			}
			set//set����
			{
				this.userName = value;
			}
		}
		public string UserDate//����û��()������Pascal������
		{
			get//get����
			{
				return userDate;
			}
			set//set����
			{
				this.userDate = value;
			}
		}
	}
	public class ReadAndSave
	{
		public Hashtable readAll()//��ȡ�����б�������ȡ������ʾ����ǰ״̬�ķ���
		{
			try
			{
				XmlDocument document=new XmlDocument();//����XML�ĵ�����
				document.Load("findMine.xml");//����XML�ļ�
				//��ȡ���е�Node
				XmlNodeList nodeList=document.GetElementsByTagName("*");
				XmlElement element;//����һ��Ԫ��
				Hashtable HTUserInfo=new Hashtable();
				UserInfo temp=null;
				for(int i=0;i<nodeList.Count;i++)
				{
					element=(XmlElement)nodeList.Item(i);
					if(element.Name=="UserScore")
					{
						temp=new UserInfo();
						temp.UserScore=element.ChildNodes[0].Value;
					}
					if(element.Name=="UserName")
					{
						temp.UserName=element.ChildNodes[0].Value;
					}
					if(element.Name=="UserDate")
					{
						temp.UserDate=element.ChildNodes[0].Value;
					}
					if(element.Name=="UserGrade")
					{
						 HTUserInfo.Add(element.ChildNodes[0].Value,temp);
					}
				}
				return HTUserInfo;
			}
			catch(Exception e)
			{
				if(File.Exists("findMine.xml"))
				{
					File.Delete("findMine.xml");
				}
				MessageBox.Show("��ȡ��Ϣʱ����:"+e.Message.ToString(),"�쳣",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
				return null;
			}
		}
		public void saveAll(Hashtable HTUserInfo)
		{
			try
			{
				string CurrentURL=Application.StartupPath;
				if(File.Exists(CurrentURL+@"\findMine.xml"))
				{
					File.Delete(CurrentURL+@"\findMine.xml");
				}
				string strXML="<?xml version='1.0'?>"+"\n";
				strXML+="<root>"+"\n";
				UserInfo temp=null;
				foreach(DictionaryEntry obj in HTUserInfo)
				{
					temp=(UserInfo)obj.Value;
					strXML+="<UserScore>"+temp.UserScore+"</UserScore>"+"\n";
					strXML+="<UserName>"+temp.UserName+"</UserName>"+"\n";
					strXML+="<UserDate>"+temp.UserDate+"</UserDate>"+"\n";
					strXML+="<UserGrade>"+obj.Key.ToString()+"</UserGrade>"+"\n";
				}
				strXML+="</root>";
				StreamWriter SWWriteXML;
				SWWriteXML=File.AppendText(CurrentURL+@"\findMine.xml");
				SWWriteXML.Write(strXML);
				SWWriteXML.Close();
			}
			catch(Exception e)
			{
				if(File.Exists("findMine.xml"))
				{
					File.Delete("findMine.xml");
				}
				MessageBox.Show("������Ϣʱ����:"+e.Message.ToString(),"�쳣",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
			}
		}
	}
}

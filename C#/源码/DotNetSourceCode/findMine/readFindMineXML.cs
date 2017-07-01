using System;
using System.Xml;
using System.Collections;
using System.IO;
using System.Windows.Forms;
namespace findMine
{
	/// <summary>
	/// Class1 的摘要说明。
	/// </summary>
	public class UserInfo
	{
		private string userScore="999";
		private string userName="匿名";
		private string userDate="0000-00-00";
		public string UserScore//后面没有()，采用Pascal命名法
		{
			get//get部分
			{
				return userScore;
			}
			set//set部分
			{
				this.userScore = value;
			}
		}
		public string UserName//后面没有()，采用Pascal命名法
		{
			get//get部分
			{
				return userName;
			}
			set//set部分
			{
				this.userName = value;
			}
		}
		public string UserDate//后面没有()，采用Pascal命名法
		{
			get//get部分
			{
				return userDate;
			}
			set//set部分
			{
				this.userDate = value;
			}
		}
	}
	public class ReadAndSave
	{
		public Hashtable readAll()//读取所有列表，并将读取内容显示至当前状态的方法
		{
			try
			{
				XmlDocument document=new XmlDocument();//创建XML文档对象
				document.Load("findMine.xml");//加载XML文件
				//获取所有的Node
				XmlNodeList nodeList=document.GetElementsByTagName("*");
				XmlElement element;//声明一个元素
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
				MessageBox.Show("读取信息时出错:"+e.Message.ToString(),"异常",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
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
				MessageBox.Show("保存信息时出错:"+e.Message.ToString(),"异常",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
			}
		}
	}
}

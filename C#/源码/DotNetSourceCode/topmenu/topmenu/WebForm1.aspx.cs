using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
namespace topmenu
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public partial class WebForm1 : System.Web.UI.Page
	{
		protected DataRow[] father;
		protected DataRow[] first;
		protected DataRow[] second;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//Page.RegisterStartupScript("topmenu",LoadTopMenu());   
		}
		public static string ConnectionString=System.Configuration .ConfigurationSettings .AppSettings["ConnectionString"];
		
		#region GetDataSet
		public static DataSet GetDataSet(string sql)
		{
			SqlDataAdapter    sda =new SqlDataAdapter(sql,ConnectionString);
			DataSet ds=new DataSet();
			sda.Fill(ds);
			return ds;
		}
		#endregion
		protected string LoadTopMenu()
		{
			//IsBoot设置菜单级别,0一级,1二级,2三级,依此类推.
			string sqlFather="select * from topmenu order by IsBoot";
			DataSet dsFather=GetDataSet(sqlFather);
			father=dsFather.Tables[0].Select("IsBoot=0","IsBoot");
			string menu="";            
			int one=0;
			int two=1;
			int three=1;
			foreach(DataRow drfather in father)
			{                
				menu+="mpmenu"+one+"=new mMenu("+"'"+drfather["text"]+"'"+",'/','self','','','','');";
				first=dsFather.Tables[0].Select("ParentID='"+Convert.ToInt32(drfather["ID"])+"' and IsBoot=1","IsBoot");
				foreach(DataRow drfirst in first)
				{
					second=dsFather.Tables[0].Select("ParentID='"+Convert.ToInt32(drfirst["ID"])+"' and IsBoot=2","IsBoot");
					if(second.Length==0)
					{
						menu+="mpmenu"+one+".addItem(new mMenuItem("+"'"+drfirst["text"]+"'";//description
						menu+=","+"'"+drfirst["url"]+"'"+",";//url
						menu+="'"+drfirst["target"]+"'"+",";//target
						menu+=""+drfirst["visible"]+",";//是否可见,false可见,true不可见
						menu+="'"+drfirst["status"]+"'"+",";//状态条
						menu+="null,'','','',''));";
					}
					foreach(DataRow drsecond in second)
					{    
						menu+="msub"+two+"=new mMenuItem("+"'"+drfirst["text"]+"','',"+"'"+drfirst["target"]+"'";
						menu+=","+drfirst["visible"]+",";//是否可见,false可见,true不可见
						menu+="'','1','','','','');";

						menu+="msub"+three+".addsubItem(new mMenuItem("+"'"+drsecond["text"]+"'";//description
						menu+=","+"'"+drsecond["url"]+"'"+",";//url
						menu+="'"+drsecond["target"]+"'"+",";//target
						menu+=""+drsecond["visible"]+",";//是否可见,false可见,true不可见
						menu+="'"+drsecond["status"]+"'"+",";//状态条
						menu+="null,'','','',''));";
						menu+="mpmenu"+one+".addItem(msub"+two+");";//addItem
						three++;
					}
					two++;
				}
				one++;
			}
			return menu;
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion		
	}
}

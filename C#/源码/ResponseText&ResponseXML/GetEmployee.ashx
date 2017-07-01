<%@ WebHandler Language="C#" Class="AspNetAjaxOverview.GetEmployee" %>

using System;
using System.Web;
using System.Web.Script.Serialization;

namespace AspNetAjaxOverview
{
	public class GetEmployee : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			
			string firstName = context.Request.Params["firstName"];
			string lastName = context.Request.Params["lastName"];
			string title = context.Request.Params["title"];
			Employee employee = new Employee(firstName, lastName, title);
			
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			string jsonEmp = serializer.Serialize(employee);
			
			context.Response.Write(jsonEmp);
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

	}
}
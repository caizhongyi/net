using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///Template 的摘要说明
/// </summary>
public class Template
{
	public Template()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static string Root = AppDomain.CurrentDomain.BaseDirectory+ "/templates/Default/";
    public static string LinkUrl = "/templates/Default/";
    public static string HeadTemplate = Root+"head.htm";
    public static string FootTemplate = Root + "foot.htm";
    public static string BodyTemplate = Root + "body.htm";
    public static string LinkTemplate = Root + "link.htm";
    public static string SubBodyTemplate = Root + "subbody.htm";
    public static string SubPageTemplate = Root + "subpage.htm";
    public static string IndexTemplate = Root + "index.htm";
}
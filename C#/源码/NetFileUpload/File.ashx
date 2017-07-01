<%@ WebHandler Language="c#" Class="File_WebHandler" Debug="true" %>

using System;
using System.Web;
using System.IO;

public class File_WebHandler : IHttpHandler
{    
    private const int UploadFileLimit = 3;//�ϴ��ļ���������
	
    private string _msg = "�ϴ��ɹ���";//������Ϣ

    public void ProcessRequest(HttpContext context)
    {
        int iTotal = context.Request.Files.Count;

        if (iTotal == 0)
        {
            _msg = "û������";
        }
        else
        {
            int iCount = 0;

            for (int i = 0; i < iTotal; i++)
            {
                HttpPostedFile file = context.Request.Files[i];

                if (file.ContentLength > 0 || !string.IsNullOrEmpty(file.FileName))
                {
                    //�����ļ�
                    file.SaveAs(System.Web.HttpContext.Current.Server.MapPath("./file/" + Path.GetFileName(file.FileName)));

                    //������Ը���ʵ��������������
                    if (++iCount > UploadFileLimit)
                    {
                        _msg = "�����ϴ����ƣ�" + UploadFileLimit;
                        break;
                    }
                }
            }
        }
        context.Response.Write("<script>window.parent.Finish('" + _msg + "');</script>");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
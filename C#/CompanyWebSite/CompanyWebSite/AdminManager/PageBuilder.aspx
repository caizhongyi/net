<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PageBuilder.aspx.cs" Inherits="AdminManager_PageBuilder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <style type="text/css" >
    body{ font-size:13px;}
    .page-builder{ border:solid 1px #999999; overflow:hidden;  width:700px;padding:10px;}
    .page-builder dl{ margin:0px; padding:0px;}
    .page-builder dl dt{ float:left; width:50%; }
    .page-builder dl dd{ text-align:right;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
       
    <div class="page-builder">
    
    <dl>
    <dt >首页</dt>
    <dd> <asp:Button ID="btn_firstPage"   runat="server" Text="生成" onclick="btn_firstPage_Click" /> </dd>
    <dd> <asp:Label ID="lab_index" runat="server" ></asp:Label></dd> 
    </dl>
    
    <dl>
    <dt>公司简界</dt>
    <dd><asp:Button ID="btn_about"   runat="server" Text="生成" onclick="btn_about_Click" /> </dd> 
    <dd><asp:Label ID="lab_about" runat="server" ></asp:Label></dd> 
    </dl>
    
    <dl>
    <dt>地产经营</dt>
    <dd><asp:Button ID="btn_dichang"   runat="server" Text="生成" onclick="btn_dichang_Click" /></dd>
    <dd><asp:Label ID="lab_dichang" runat="server" ></asp:Label> </dd>
    </dl>
    
    <dl>
    <dt>通讯经营</dt>
    <dd><asp:Button ID="btn_xuntong"  runat="server" Text="生成" onclick="btn_xuntong_Click" /></dd>
    <dd><asp:Label ID="lab_xuntong" runat="server" ></asp:Label></dd>
    </dl>
    
    <dl>
    <dt>证券经营</dt>
    <dd><asp:Button ID="btn_zhengquang"   runat="server" Text="生成" onclick="btn_zhengquang_Click" /> </dd>
    <dd><asp:Label ID="lab_zhengquang" runat="server" ></asp:Label></dd>
    </dl>
    
    <dl>
    <dt>新闻</dt>
    <dd><asp:Button ID="btn_news"   runat="server" Text="生成" onclick="btn_news_Click" /></dd>
    <dd><asp:Label ID="lab_news" runat="server" ></asp:Label> </dd>
    </dl>
    
    <dl>
    <dt>新闻详细页</dt>
    <dd><asp:Button ID="btn_newsDetail"   runat="server" Text="生成" onclick="btn_newsDetail_Click" /> </dd>
    <dd><asp:Label ID="lab_newsDetail" runat="server" ></asp:Label></dd>
    </dl>
    
     <dl>
    <dt>联系我们</dt>
    <dd> <asp:Button ID="btn_contact"    runat="server" Text="生成" onclick="btn_contact_Click" />  </dd>
    <dd><asp:Label ID="lab_contact" runat="server" ></asp:Label>  </dd>
    </dl>
    
      <dl>
    <dt>招贤纳材</dt>
    <dd><asp:Button ID="btn_employee"    runat="server" Text="生成" onclick="btn_employee_Click" />  </dd>
    <dd><asp:Label ID="lab_employee" runat="server" ></asp:Label>  </dd>
    </dl>
    
    <dl>
    <dt>全部生成</dt>
    <dd><asp:Button ID="btn_all"    runat="server" Text="生成" onclick="btn_all_Click" />  </dd>
    <dd><asp:Label ID="lab_all" runat="server"></asp:Label>  </dd>
    </dl>

    </div>

    

    </form>
</body>
</html>

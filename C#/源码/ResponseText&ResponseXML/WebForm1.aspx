<%@ Page language="c#" Inherits="WebApplication2.WebForm1" CodeFile="WebForm1.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
		<meta name="CODE_LANGUAGE" Content="C#" />
		<meta name="vs_defaultClientScript" content="JavaScript" />
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />	
			<script language="javascript" type="text/javascript">
		
		//����XMLHttpReqest����ajax�ĺ��Ķ���
		//��������
      var xmlHttp;
      function CreateXMLHttpRequest()
      {
      //�����ie
         if(window.ActiveXObject)
         {
            xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");    
            
         }
         //���������������������ie7.0���ϰ汾��Ҳ�д˶���
         else
         {
           xmlHttp=new XMLHttpRequest();
         }
      }      
      function updateTotal()
      {        
        frm =document.forms[0];
      //  url="WebForm2.aspx?A="+frm.elements['A'].value+"&B="+frm.elements['B'].value;
        url="Handler.ashx?A="+frm.elements['A'].value+"&B="+frm.elements['B'].value;

      
      //��ʼ���Է�����������
        xmlHttp.open("GET",url,true);//true��ʾҪ�첽����
        xmlHttp.onreadystatechange=doUpdate;//��������ɺ�,�ͻ��˴����¼�
        //��������
        xmlHttp.send();   
        return false;
      }      
      function doUpdate()
      {
        //��ʾ��ʾ������� 
        if(xmlHttp.readyState==4)
        {         
          document.forms[0].elements["TOT"].value=xmlHttp.responseText;  
       //  alert(xmlHttp.responseText);
          
         // document.getElementById('nameList').innerHTML =xmlHttp.responsetext;   
        }
        if(xmlHttp.readyState==0)
        {         
          document.forms[0].elements["TOT"].value="û�г�ʼ��";
        }
        if(xmlHttp.readyState==1)
        {         
          document.forms[0].elements["TOT"].value="װ����";
        }
        if(xmlHttp.readyState==3)
        {         
          document.forms[0].elements["TOT"].value="������";
        }
        
       
        
      }
		</script>
	</HEAD>
	<body onload="CreateXMLHttpRequest();">
		<form>
			<input type="text" id="A" style="Z-INDEX: 101; LEFT: 464px; POSITION: absolute; TOP: 104px"
				onkeyup="updateTotal();" value="0"> 
				<input type="text" id="B" style="Z-INDEX: 102; LEFT: 464px; POSITION: absolute; TOP: 160px"
				onkeyup="updateTotal();" value="0"> 
				<input type="text" id="TOT" style="Z-INDEX: 103; LEFT: 464px; WIDTH: 216px; POSITION: absolute; TOP: 232px; HEIGHT: 24px"
				size="30">
				<div id="nameList" >
                    &nbsp;</div>
			<asp:Label id="Label1" style="Z-INDEX: 105; LEFT: 400px; POSITION: absolute; TOP: 112px" runat="server">A</asp:Label>
			<asp:Label id="Label2" style="Z-INDEX: 106; LEFT: 400px; POSITION: absolute; TOP: 168px" runat="server">B</asp:Label>
			<asp:Label id="Label3" style="Z-INDEX: 107; LEFT: 400px; POSITION: absolute; TOP: 232px" runat="server">SUM</asp:Label>
		</form>
	</body>
</HTML>

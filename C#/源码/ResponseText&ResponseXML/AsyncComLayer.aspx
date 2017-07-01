<%@ Page Language="C#" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Asynchronous Communication Layer Overview</title>
</head>
<body>
 
    <form id="form1" runat="server">
		
		<script language="javascript" type="text/javascript">
			function showEmployee(firstName, lastName, title)
			{
				var request = new Sys.Net.WebRequest();
				request.set_url('GetEmployee.ashx');
				request.set_httpVerb("POST");
				request.add_completed(onGetEmployeeComplete);
				
				var requestBody = String.format(
					"firstName={0}&lastName={1}&title={2}",
					encodeURIComponent(firstName),
					encodeURIComponent(lastName),
					encodeURIComponent(title));
				request.set_body(requestBody);
				
				request.invoke();
			}
			
			function onGetEmployeeComplete(response)
			{
				if (response.get_responseAvailable())
				{
					var employee = response.get_object();
					alert(String.format(
						"Hello I'm {0} {1}, my title is '{2}'",
						employee.FirstName,
						employee.LastName,
						employee.Title));
				}
			}
		</script>

        &nbsp;
       <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
		
		<input type="button" value="Bill Gates"
			onclick="showEmployee('Bill', 'Gates', 'Chair man')" />&nbsp;
		<input type="button" value="Steve Ballmer"
			onclick="showEmployee('Steve', 'Ballmer', 'CEO')" />
    </form>
</body>
</html>

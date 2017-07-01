<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_SWFUpload.ascx.cs" Inherits="Web.SWFUpload.UC_SWFUpload" %>

<script type="text/javascript">
    function <%= this.GetControlId("load") %>() {
        var LoadSettings = {
            upload_url:"/SWFUpload/upload.aspx",
            post_params:{
                            ASPSESSID: "<%=Session.SessionID %>",
		                    path: "<%=swfUploadInfo.Path%>",
		                    fn:"<%=swfUploadInfo.OldFileName%>",
		                    small:"<%=swfUploadInfo.IsSmall%>",
		                    sw:"<%=swfUploadInfo.SmallWidth%>",
		                    sh:"<%=swfUploadInfo.SmallHeight%>",
		                    wm:"<%=swfUploadInfo.IsWaterMark%>"
                        },
            file_size_limit: "<%=swfUploadInfo.File_size_limit%> MB",
		    file_types: "<%=swfUploadInfo.File_types%>",
		    file_types_description: "<%=swfUploadInfo.File_types_description%>",
		    file_upload_limit: <%=swfUploadInfo.UploadMode.ToString()=="BUTTON"?"1":(swfUploadInfo.File_upload_limit - swfUploadInfo.Exist_file_count).ToString()%>,
		    button_action:SWFUpload.BUTTON_ACTION.<%=swfUploadInfo.Button_action%>,
		    button_disabled : <%=((swfUploadInfo.File_upload_limit - swfUploadInfo.Exist_file_count)<= 0 && swfUploadInfo.File_upload_limit!=0)  ? "true" : "false" %>,
		    button_placeholder_id:  "<%= this.GetControlId("spanButtonPlaceholder") %>",
            custom_settings: {
                upload_target: "<%= this.GetControlId("divFileProgressContainer") %>",
                submitBtnId: "<%=swfUploadInfo.SubmitButtonId%>",
                serverDataId: "<%=this.hidIdList.ClientID%>",
                uploadMode: "<%=swfUploadInfo.UploadMode%>"
            }
        }
        SWFLoad(LoadSettings);
    }
    addLoadEvent(<%= this.GetControlId("load") %>);
</script>

<asp:HiddenField runat="server" ID="hidIdList" />
<asp:Literal runat="server" ID="lalHtml"></asp:Literal>
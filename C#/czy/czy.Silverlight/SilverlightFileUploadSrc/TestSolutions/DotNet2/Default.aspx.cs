using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page 
{
    public string args;
    protected void Page_Load(object sender, EventArgs e)
    {
        string UploadPage = "FileUpload.ashx";
        int UploadChunkSize = 4194304;
        int MaximumTotalUpload =-1;
        int MaximumUpload = -1;
        int MaxConcurrentUploads = 1;
        string ResizeImage = "false";
        int ImageSize = 300;
        string Multiselect = "true";
        // filter the file selection
        string Filter = "Images (*.jpg;*.gif)|*.jpg;*.gif|All Files (*.*)|*.*";
        // display files in the uploader
        string AllowThumbnail = "false";
        // javascript function to call when all files have uploaded
        string JavascriptCompleteFunction = "";
        int MaxNumberToUpload = -1;

        args = string.Format("UploadPage={0},UploadChunkSize={1},MaximumTotalUpload={2},MaximumUpload={3},MaxConcurrentUploads={4},ResizeImage={5},ImageSize={6},Multiselect={7},Filter={8},AllowThumbnail={9},JavascriptCompleteFunction={10},MaxNumberToUpload={11}",
            Page.Server.UrlEncode(UploadPage), UploadChunkSize, MaximumTotalUpload, MaximumUpload, MaxConcurrentUploads, ResizeImage, ImageSize, Multiselect, Filter, AllowThumbnail, Page.Server.UrlEncode(JavascriptCompleteFunction), MaxNumberToUpload);
    }
}

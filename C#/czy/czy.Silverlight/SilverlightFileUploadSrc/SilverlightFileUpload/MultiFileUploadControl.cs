using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.SilverlightControls;

namespace DC.SilverlightFileUpload
{
    //[DefaultProperty("Text")]
    //[ToolboxData("<{0}:MultiFileUploadControl runat=server></{0}:MultiFileUploadControl>")]
    public class MultiFileUploadControl : Silverlight
    {
        private const string XAP_FILE = "DC.SilverlightFileUpload.FileUpload.xap";

        public MultiFileUploadControl()
        {
            
        }

        protected new string Source { get; set; }

        [Category("Behavior")]
        [Description("The page to upload files to.")]
        [DefaultValue("")]
        public string UploadPage
        {
            get
            {
                object o = ViewState["UploadPage"];
                if (o == null)
                    return "";
                return o.ToString();
            }
            set { ViewState["UploadPage"] = value; }
        }

        [Category("Behavior")]
        [Description("The size of the chunks sent to the server, don't make too small. Recommend between 4MB-25MB")]
        [DefaultValue("4194304")]
        public long UploadChunkSize
        {
            get
            {
                object o = ViewState["UploadChunkSize"];
                if (o == null)
                    return 4194304;
                return (long)o;
            }
            set { ViewState["UploadChunkSize"] = value; }
        }

        [Category("Behavior")]
        [Description("The maximum total number of bytes that can be uploaded, 0 = unlimited.")]
        [DefaultValue("-1")]
        public long MaximumTotalUpload
        {
            get
            {
                object o = ViewState["MaximumTotalUpload"];
                if (o == null)
                    return -1;
                return (long)o;
            }
            set { ViewState["MaximumTotalUpload"] = value; }
        }

        [Category("Behavior")]
        [Description("The maximum number of bytes a file can be to be uploaded.")]
        [DefaultValue("-1")]
        public long MaximumUpload
        {
            get
            {
                object o = ViewState["MaximumUpload"];
                if (o == null)
                    return -1;
                return (long)o;
            }
            set { ViewState["MaximumUpload"] = value; }
        }

        [Category("Behavior")]
        [Description("The maximum number of concurrent uploads.")]
        [DefaultValue("1")]
        public int MaxConcurrentUploads
        {
            get
            {
                object o = ViewState["MaxConcurrentUploads"];
                if (o == null)
                    return 1;
                return (int)o;
            }
            set { ViewState["MaxConcurrentUploads"] = value; }
        }

        [Category("Behavior")]
        [Description("The maximum number of files allowed to be uploaded.  Set to -1 for unlimited.")]
        [DefaultValue("-1")]
        public int MaxNumberToUpload
        {
            get
            {
                object o = ViewState["MaxNumberToUpload"];
                if (o == null)
                    return -1;
                return (int)o;
            }
            set { ViewState["MaxNumberToUpload"] = value; }
        }

        [Category("Behavior")]
        [Description("Resize jpg files before uploaded.")]
        [DefaultValue("false")]
        public bool ResizeImage
        {
            get
            {
                object o = ViewState["ResizeImage"];
                if (o == null)
                    return false;
                return (bool)o;
            }
            set { ViewState["ResizeImage"] = value; }
        }

        [Category("Behavior")]
        [Description("The page to upload files to.")]
        [DefaultValue("300")]
        public int ImageSize
        {
            get
            {
                object o = ViewState["ImageSize"];
                if (o == null)
                    return 300;
                return (int)o;
            }
            set { ViewState["ImageSize"] = value; }
        }

        [Category("Behavior")]
        [Description("All the user to select and upload multiple files.")]
        [DefaultValue("true")]
        public bool Multiselect
        {
            get
            {
                object o = ViewState["Multiselect"];
                if (o == null)
                    return true;
                return (bool)o;
            }
            set { ViewState["Multiselect"] = value; }
        }

        [Category("Behavior")]
        [Description("The filter to use when selecting files.")]
        [DefaultValue("")]
        public string Filter
        {
            get
            {
                object o = ViewState["Filter"];
                if (o == null)
                    return "";
                return o.ToString();
            }
            set { ViewState["Filter"] = value; }
        }

        [Category("Behavior")]
        [Description("Allow the user to view thumbnails of images.")]
        [DefaultValue("false")]
        public bool AllowThumbnail
        {
            get
            {
                object o = ViewState["AllowThumbnail"];
                if (o == null)
                    return false;
                return (bool)o;
            }
            set { ViewState["AllowThumbnail"] = value; }
        }

        [Category("Behavior")]
        [Description("A javascript function to call when all files have been uploaded.")]
        [DefaultValue("")]
        public string JavascriptCompleteFunction
        {
            get
            {
                object o = ViewState["JavascriptCompleteFunction"];
                if (o == null)
                    return "";
                return o.ToString();
            }
            set { ViewState["JavascriptCompleteFunction"] = value; }
        }
        
        protected override void Render(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(UploadPage))
                throw new ApplicationException("UploadPage cannot be null");
            string uploadPage = ResolveUrl(UploadPage);
            if (!uploadPage.ToLower().StartsWith("http"))
            {
                Uri uri = new Uri(Page.Request.Url, uploadPage);
                uploadPage = uri.AbsoluteUri;
            }
            string url = Page.ClientScript.GetWebResourceUrl(this.GetType(), XAP_FILE);
            base.Source = url;

            if (!string.IsNullOrEmpty(InitParameters))
                InitParameters += "&";

            InitParameters += string.Format("UploadPage={0},UploadChunkSize={1},MaximumTotalUpload={2},MaximumUpload={3},MaxConcurrentUploads={4},ResizeImage={5},ImageSize={6},Multiselect={7},Filter={8},AllowThumbnail={9},JavascriptCompleteFunction={10},MaxNumberToUpload={11}",
                HttpUtility.UrlEncode(uploadPage), UploadChunkSize, MaximumTotalUpload, MaximumUpload, MaxConcurrentUploads, ResizeImage, ImageSize, Multiselect, Filter, AllowThumbnail, JavascriptCompleteFunction, MaxNumberToUpload);

            base.Render(writer);
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using IMMENSITY.SWFUploadAPI;

namespace Web.SWFUpload
{
    public partial class UC_SWFUpload : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Bind();
                this.hidIdList.Value = string.Empty;
            }
        }
        public SWFUploadInfo swfUploadInfo { get; set; }
        private void Bind()
        {
            StringBuilder sbHtml = new StringBuilder();
            if (swfUploadInfo == null) { swfUploadInfo = new SWFUploadInfo(); }
            if (swfUploadInfo.UploadMode == UpMode.LIST)
            {
                sbHtml.Append("\r\n");
                sbHtml.Append("<div class=\"uploadpic\">\r\n");
                sbHtml.Append(" <div class=\"upbatch\">\r\n");
                sbHtml.Append("     <!-- 选择图片[over] -->\r\n");
                sbHtml.Append("     <div id=\"swfupload_header\" class=\"swfupload_header\">\r\n");
                sbHtml.Append("         <span class=\"swfupload_pic_name\">文件名称</span> <span class=\"swfupload_pic_state\">上传状态</span>\r\n");
                sbHtml.Append("         <div class=\"swfupload_pic_option\">\r\n");
                sbHtml.Append("             上传进度</div>\r\n");
                sbHtml.Append("         <span class=\"swfupload_pic_percent\">文件操作</span>\r\n");
                sbHtml.Append("     </div>\r\n");
                sbHtml.Append("     <ul id=\"" + GetControlId("divFileProgressContainer") + "\" class=\"swfupload_main\">\r\n");
                sbHtml.Append("         <!--载入图片文件列表-->\r\n");
                sbHtml.Append("     </ul>\r\n");
                sbHtml.Append(" </div>\r\n");
                sbHtml.Append(" <div style=\"text-align: right; margin: 5px 0 -5px 0;\">\r\n");
                sbHtml.Append("     <span id=\"" + GetControlId("spanButtonPlaceholder") + "\"></span>\r\n");
                sbHtml.Append(" </div>\r\n");
                sbHtml.Append(" </div>\r\n");
                sbHtml.Append(" <div id=\"thumbnails\">\r\n");
                sbHtml.Append(" </div>\r\n");
            }
            else
            {
                sbHtml.Append("<div>\r\n");
                sbHtml.Append("    <div id=\"" + GetControlId("divFileProgressContainer") + "\"></div>\r\n");
                sbHtml.Append("    <span id=\"" + GetControlId("spanButtonPlaceholder") + "\"></span>\r\n");
                sbHtml.Append("</div>\r\n");
            }
            this.lalHtml.Text = sbHtml.ToString();
        }
        /// <summary>
        /// 获取与SWFUpload控件组合后的控件Id
        /// </summary>
        /// <param name="id">副Id</param>
        /// <returns></returns>
        protected string GetControlId(string id)
        {
            return string.Format("{0}_{1}", this.ID, id.Trim());
        }

        /// <summary>
        /// 文件相对路径集合(上传多文件时使用)
        /// </summary>
        public string[] FilePathList
        {
            get
            {
                return this.hidIdList.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }
        /// <summary>
        /// 文件相对路径
        /// </summary>
        public string FilePath
        {
            get
            {
                string value = this.hidIdList.Value.Trim();
                if (value != string.Empty)
                {
                    string[] valueList = this.hidIdList.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    return valueList[valueList.Length - 1];
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// 文件名集合(上传多文件时使用)
        /// </summary>
        public string[] FileNameList
        {
            get
            {
                string[] valueList = this.hidIdList.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < valueList.Length; i++)
                {
                    valueList[i] = valueList[i].Substring(valueList[i].LastIndexOf("/") + 1, valueList[i].Length - valueList[i].LastIndexOf("/") - 1);
                }
                return valueList;
            }
        }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get
            {
                string value = this.hidIdList.Value.Trim();
                if (value != string.Empty)
                {
                    string[] valueList = this.hidIdList.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    string lastValue = valueList[valueList.Length - 1];
                    return lastValue.Substring(lastValue.LastIndexOf("/") + 1, lastValue.Length - lastValue.LastIndexOf("/") - 1);
                }
                return string.Empty;
            }
        }

    }
}
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;
using JuSNS.Config;
using JuSNS.Model;
using JuSNS.Common;
using JuSNS.Factory;
using JuSNS.Home;
using NVelocity;

namespace JuSNS.Web.library.page
{
    public class draw : JuSNS.UI.Page.BasePage
    {
        public string _filename = string.Empty;
        public int _width = 0;
        public int _height = 0;
        public string _fontstring = string.Empty;
        public string _waterimgPath = string.Empty;
        public int _filetype = 0;
        public DealType dealType = DealType.NONE;
        public override void Page_Load(ref VelocityContext context)
        {
            this.PicCreate();
        }

        protected void PicCreate()
        {
            //filetype:图片类别  0头像, 1相册, 2 群组, 3点评, 4问答, 5其他
            _filename = GetQueryString("filename");
            string ww = GetQueryString("w");
            string hh = GetQueryString("h");
            //dealType: none无水印  WaterImage水印图    WaterFont水印文    DoubleDo水印图文   title水印标题   Copyright水印版权 

           // int.TryParse(GetQueryString("dtype"), out dealType);
            if (ww != null && ww.ToLower().EndsWith("px"))
            {
                ww = ww.Substring(0, ww.Length - 2);
            }
            if (hh != null && hh.ToLower().EndsWith("px"))
            {
                hh = hh.Substring(0, hh.Length - 2);
            }
            int.TryParse(GetQueryString("type"), out _filetype);
            int.TryParse(ww, out _width);
            int.TryParse(hh, out _height);
            _fontstring = GetQueryString("str");
            _waterimgPath = GetQueryString("water");
            this.CreateImage();
        }

        /// <summary>
        /// 输出图像
        /// </summary>
        protected void CreateImage()
        {
            string fstr = _fontstring;
            if (!string.IsNullOrEmpty(fstr)) fstr = Common.Input.MD5(fstr);
            string OutFileName = _filetype + "" + _width + "" + _height + "" + fstr;
            string tfile = _filename;
            tfile = tfile.Substring(tfile.LastIndexOf('/') + 1);
            OutFileName =HttpContext.Current.Server.UrlEncode(OutFileName) + tfile;
            string temppath = "~/uploads/thumbnail";
            if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(temppath)))
            {
                System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(temppath));
            }
            string outtempfile = temppath + "/" + OutFileName;
            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(outtempfile)))
            {
                //如果找到已生成的图片
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.WriteFile(HttpContext.Current.Server.MapPath(outtempfile));
            }
            else
            {
                //如果没有找到生成的图片
                string filepath = string.Empty;
                switch (_filetype)
                {
                    // 图片类别  0头像, 1相册, 2 群组, 3点评, 4问答, 5其他
                    case 0: //头像
                        filepath = "~/uploads/head";
                        break;
                    case 1: //相册
                        filepath = "~/uploads/photo";
                        break;
                    case 2: //群组
                        filepath = "~/uploads/group";
                        break;
                    case 3: //点评
                        filepath = "~/uploads/dianping";
                        break;
                    case 4: //问答
                        filepath = "~/uploads/ask";
                        break;
                    case 5: //其他
                        filepath = "~/uploads/other";
                        break;
                    default:
                        filepath = "";
                        break;
                }
                if (!string.IsNullOrEmpty(filepath))
                {
                    filepath += "/" + _filename;
                }
                else
                {
                    filepath = _filename;
                }
                filepath = HttpContext.Current.Server.MapPath(filepath);
                try
                {
                    Image img = Image.FromFile(filepath);

                    if (_width == 0 && _height == 0)
                    {
                        _width = img.Width;
                        _height = img.Height;
                    }
                    Bitmap bm = new Bitmap(img);
                    byte[] byteImg = Input.ImageToByte(bm);
                    byte[] outimg = null;
                    if (dealType == DealType.Title)
                    {
                        Input.CreateThumbnailByPulling(byteImg, out outimg, (double)_width, (double)_height);
                    }
                    else
                    {
                        Input.CreateThumbnail(byteImg, out outimg, (double)_width, (double)_height);
                    }
                    if (!string.IsNullOrEmpty(_waterimgPath) || !string.IsNullOrEmpty(_fontstring))
                    {
                        //水印
                        Image finalImage = ReDrawImg.DealImage(Input.ByteToImage(outimg), _waterimgPath, _fontstring, this.dealType);
                        if (finalImage != null)
                        {
                            finalImage.Save(HttpContext.Current.Server.MapPath(outtempfile));
                            HttpContext.Current.Response.ClearContent();
                            HttpContext.Current.Response.ContentType = "image/Jpeg";
                            HttpContext.Current.Response.BinaryWrite(Input.ImageToByte(finalImage));
                            finalImage.Dispose();
                        }
                        else
                        {
                            Image imgOutimg = Input.ByteToImage(outimg);
                            imgOutimg.Save(HttpContext.Current.Server.MapPath(outtempfile));
                            HttpContext.Current.Response.ClearContent();
                            HttpContext.Current.Response.ContentType = "image/Jpeg";
                            HttpContext.Current.Response.BinaryWrite(outimg);
                            imgOutimg.Dispose();
                        }
                    }
                    else
                    {
                        Image imgOutImgelse = Input.ByteToImage(outimg);
                        imgOutImgelse.Save(HttpContext.Current.Server.MapPath(outtempfile));
                        HttpContext.Current.Response.ClearContent();
                        HttpContext.Current.Response.ContentType = "image/Jpeg";
                        HttpContext.Current.Response.BinaryWrite(outimg);
                        imgOutImgelse.Dispose();
                    }
                    img.Dispose();
                    bm.Dispose();
                }
                catch
                {
                    if (_height == 0)
                    {
                        _height = _width;
                    }
                    if (_width == 0)
                    {
                        _width = _height;
                    }
                    System.Drawing.Bitmap bmp = new Bitmap(_width, _height);
                    System.Drawing.Graphics g = Graphics.FromImage(bmp);
                    g.Clear(Color.White);
                    string err = "找不到图片";
                    g.DrawString("找不到图片", new Font("宋体", 9), new SolidBrush(Color.Black), _width / 2 - err.Length * 9 / 2, _height / 2 - 5);
                    //这里选择文本字体颜色   
                    g.Dispose();

                    HttpContext.Current.Response.ClearContent();
                    HttpContext.Current.Response.ContentType = "image/Jpeg";
                    bmp.Save(HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                    bmp.Dispose();
                }
            }
        }

    }
}


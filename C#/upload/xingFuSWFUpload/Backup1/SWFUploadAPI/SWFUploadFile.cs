using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Xml;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Web.UI;
using System.Net;
using IMMENSITY.SWFUploadAPI.Enums;

namespace IMMENSITY.SWFUploadAPI
{
    /// <summary>
    /// Description:文件上传方法类
    /// Author:张盼
    /// Date:2010-01-16
    /// 
    /// 注意:如果需要附加图片水印,需要在WebConfig中添加如下配置
    /// <appSettings><add key="WaterMarkPath" value="路径"/></appSettings>
    /// 如果需要添加小图,需要在WebConfig中添加如下配置,默认为small_
    /// <appSettings><add key="SmallPicPrefix" value="前缀"/></appSettings>
    /// 如果需要文字水印,需要在WebConfig中添加如下配置
    /// <appSettings><add key="WaterMarkText" value="文字内容"/></appSettings>
    /// </summary>
    public class SWFUploadFile
    {
        /// <summary>
        /// 数据请求基类
        /// </summary>
        private HttpPostedFile _HPFile = null;
        /// <summary>
        /// 水印图片文件
        /// </summary>
        private string _waterMarkImgPath = string.Empty;
        /// <summary>
        /// 网站根目录
        /// </summary>
        private string _webPath = System.Web.HttpContext.Current.Server.MapPath("~/");
        public SWFUploadFile()
        {
            //默认上传路径
            this.Path = System.Web.HttpContext.Current.Server.MapPath("~/");
            //大图子文件夹
            this.BigChildPath = "b";
            //小图子文件夹
            this.SmallChildPath = "s";
            //水印图片路径
            this._waterMarkImgPath = Path + "/" + SWFWebConfigManage.GetByAppSettingsKey("WaterMarkPath");
            //当前时间用作文件名(年,月,日,时,分,秒,3位毫秒)
            this.NewFileName = DateTime.Now.ToString("yMdhhmmssfff");
            //默认不生成小图
            this.SmallPic = false;
            //默认生成小图片最大的宽度
            this.MaxWith = 140;
            //默认生成小图片最大的高度
            this.MaxHeight = 140;
            //默认不需要水印
            this.IsWaterMark = false;
            //默认水印类型
            this.WMType = SWFWaterMarkType.WM_IMAGE;
            //水印位置,默认为右下
            this.WMLocation = SWFWaterMarkLocation.WM_BOTTOM_RIGHT;
            //水印文字
            this.WaterMarkText = SWFWebConfigManage.GetByAppSettingsKey("WaterMarkText");//水印文字
        }
        #region 公共属性
        /// <summary>
        /// 上传路径 默认:网站根目录
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 大图子文件夹
        /// </summary>
        public string BigChildPath { get; set; }
        /// <summary>
        /// 小图子文件夹
        /// </summary>
        public string SmallChildPath { get; set; }
        ///<summary>
        /// 新的文件名 默认:年 月 日 时 分 秒 毫秒
        ///</summary>
        public string NewFileName { get; set; }
        ///<summary>
        /// 上传小图片最大宽度 默认:140
        ///</summary>
        public int MaxWith { get; set; }
        ///<summary>
        /// 上传小图片最大高度度 默认:140
        ///</summary>
        public int MaxHeight { get; set; }
        ///<summary>
        /// 是否需要小图  已否决
        ///</summary>
        public bool SmallPic { get; set; }
        /// <summary>
        /// 是否添加水印
        /// </summary>
        public bool IsWaterMark { get; set; }
        /// <summary>
        /// 水印类型
        /// </summary>
        public SWFWaterMarkType WMType { get; set; }
        /// <summary>
        /// 水印位置
        /// </summary>
        public SWFWaterMarkLocation WMLocation { get; set; }
        /// <summary>
        /// 文字水印
        /// </summary>
        private string WaterMarkText { get; set; }

        #endregion
        #region 上传
        //根据 mime 类型，返回编码器
        private System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            System.Drawing.Imaging.ImageCodecInfo result = null;

            //检索已安装的图像编码解码器的相关信息。
            System.Drawing.Imaging.ImageCodecInfo[] encoders =
                System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < encoders.Length; i++)
            {
                if (encoders[i].MimeType == mimeType)
                {
                    result = encoders[i];
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// 开始上传
        /// </summary>
        /// <param name="hpfBase">数据请求基类</param>
        /// <param name="savePath">要保存的路径</param>
        /// <param name="oldFileName">旧文件名称,便于删除(注:如果存在文件夹路径,程序将自动去除,只留下文件名)</param>
        /// <param name="state">上传状态.  0:上传成功.  1:没有选择要上传的文件.  2:上传文件类型不符.   3:上传文件过大  -1:应用程序错误.</param>
        /// <returns>文件名</returns>
        public string SaveFile(HttpPostedFile hpfBase, string savePath, string oldFileName, ref int state)
        {
            oldFileName = this.ClearFileFolder(oldFileName);
            this._HPFile = hpfBase;
            return this.Save(savePath, oldFileName, ref state);
        }

        /// <summary>
        /// 去除文件名中的文件夹名
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        private string ClearFileFolder(string fileName)
        {
            fileName = fileName.Trim().Replace("//", "/").Replace("\\", "/");
            if (fileName != string.Empty && fileName.IndexOf(".") != -1 && fileName.IndexOf("/") != -1)
            {
                fileName = fileName.Substring(fileName.LastIndexOf("/") + 1, fileName.Length - fileName.LastIndexOf("/") - 1);
            }
            return fileName;
        }
        /// <summary>
        /// 上传核心
        /// 返回文件名或上传状态
        /// </summary>
        /// <param name="inputFile">上传控件</param>
        /// <param name="savePath">存放的文件夹   从网站根目录起</param>
        /// <param name="oldFileName">在修改图片时指定旧图片名以便删除   默认为空字符串</param>
        /// <returns>返回上传状态.  0:上传成功.  1:没有选择要上传的文件.  2:上传文件类型不符.   3:上传文件过大  -1:应用程序错误.</returns>
        private string Save(string savePath, string oldFileName, ref int state)
        {
            try
            {
                #region 判断是否已选择文件  如果没选择返回旧图片名
                //_inputFileValue为空   表示没有选择文件   返回状态1
                if (this._HPFile.ContentLength==0)
                {
                    //设置状态为没有选择图片
                    state = 1;
                    //返回旧图片名
                    return oldFileName;
                }
                #endregion
                #region 检查目录是否存在
                DirectoryInfo dir = new DirectoryInfo(this.Path + savePath + "/" + this.BigChildPath);
                //判断大图存放目录是否存在   不存在则创建
                if (!dir.Exists) { dir.Create(); }
                if (this.SmallPic)
                {
                    dir = new DirectoryInfo(this.Path + savePath + "/" + this.SmallChildPath);
                    //判断小图存放目录是否存在   不存在则创建
                    if (!dir.Exists) { dir.Create(); }
                }
                #endregion
                #region 开始上传
                string extension = System.IO.Path.GetExtension(this._HPFile.FileName).ToLower();
                //存放文件路径+文件名
                string filePath = string.Format("{0}{1}/{2}/{3}{4}", this.Path, savePath, this.BigChildPath, this.NewFileName, extension);
                //判断是否需要水印
                if (IsWaterMark)
                {
                    string tempPath = string.Format("{0}/temp/{1}{2}", this.Path, this.NewFileName, extension);
                    dir = new DirectoryInfo(string.Format("{0}/temp", this.Path));
                    //判断是否存在temp临时目录,如果不存在则创建
                    if (!dir.Exists) { dir.Create(); }
                    if (this._HPFile != null) { this._HPFile.SaveAs(tempPath); }
                    //开始上传文件
                    this.addWaterMark(tempPath, filePath);
                }
                else
                {
                    //开始上传文件
                    if (this._HPFile != null) { this._HPFile.SaveAs(filePath); }
                }
                #region 上传小图
                if (SmallPic)
                {
                    string newFilePath = string.Format("{0}{1}/{2}/{3}{4}", this.Path, savePath, this.SmallChildPath, this.NewFileName, extension);
                    this.GreateMiniImage(filePath, newFilePath, this.MaxWith, this.MaxHeight);
                }
                #endregion
                #endregion
                #region 删除图片
                if (oldFileName.Trim() != string.Empty)
                {
                    this.Delete(savePath, oldFileName, true);
                }
                #endregion
                //设置状态为0,表示上传成功
                state = 0;
                //返回文件名
                return string.Format("{0}{1}", this.NewFileName, extension);
            }
            catch
            {
                //发生严重错误,设置状态为-1,返回旧文件名
                state = -1;
                return oldFileName;
            }
        }

        #endregion
        #region 删除文件
        /// <summary>
        /// 删除指定文件
        /// </summary>
        /// <param name="savePath">文件夹名</param>
        /// <param name="fileName">文件名</param>
        /// <param name="delSmallPic">是否删除小图</param>
        public string Delete(string savePath, string fileName, bool delSmallPic)
        {
            //删除状态
            string msg = string.Empty;
            string folder = savePath;
            folder = folder == null ? "other" : folder;//没有找到则默认为other文件夹
            string delPath = string.Format("{0}/{1}", folder, this.BigChildPath);
            msg=this.Del(delPath, fileName);
            if (delSmallPic)
            {
                delPath = string.Format("{0}/{1}", folder, this.SmallChildPath);
                this.Del(delPath, fileName);//删除小图(删除小图时不记录状态)
            }
            return msg;
        }
        private int deleteCount = 0;
        /// <summary>
        /// 删除核心代码
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string Del(string folder, string fileName)
        {
            string msg = "文件不存在!";
            fileName = this.ClearFileFolder(fileName);
            string delPath = System.Web.HttpContext.Current.Server.MapPath("~/");
            string delPicPath = string.Empty;

            try
            {
                delPicPath = string.Format("{0}/{1}/{2}", delPath, folder, fileName);
                if (File.Exists(delPicPath))//如果该文件存在，则删除
                {
                    deleteCount++;
                    File.Delete(delPicPath);
                    msg = "删除成功!";
                }
            }
            catch
            {
                if (deleteCount < 100)//为避免因资源被占用删除不了数据   所以在此循环100次
                {
                    msg = "删除失败,请与站长联系!";
                    Del(folder, fileName);
                }
            }
            return msg;
        }
        #endregion
        #region 其它
        /// <summary>
        /// 删除指定文件夹中所有文件
        /// </summary>
        /// <param name="directoryPath"></param>
        public void DeleteByDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                DirectoryInfo dir = Directory.CreateDirectory(directoryPath);
                foreach (FileInfo fileInfo in dir.GetFiles())
                {
                    fileInfo.Delete();
                }
                dir.Delete(false);
            }
        }
        /// <summary>
        /// 删除指定文件夹中所有文件
        /// </summary>
        /// <param name="savePath"></param>
        public void DeleteByDirectory(string savePath, string directoryPath)
        {
            string folder = savePath;
            directoryPath = string.Format("{0}/{1}/{2}", this.Path, folder, directoryPath);
            if (Directory.Exists(directoryPath))
            {
                DirectoryInfo dir = Directory.CreateDirectory(directoryPath);
                foreach (FileInfo fileInfo in dir.GetFiles())
                {
                    fileInfo.Delete();
                }
                dir.Delete(false);
            }
        }
        #endregion
        #region 添加水印
        /// <summary>
        /// 添加图片水印
        /// </summary>
        /// <param name="oldpath">原图片绝对地址</param>
        /// <param name="newpath">新图片放置的绝对地址</param>
        private void addWaterMark(string oldpath, string newpath)
        {
            try
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(oldpath);
                Bitmap b = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(b);
                g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.High;

                g.DrawImage(image, 0, 0, image.Width, image.Height);

                if (IsWaterMark)
                {
                    switch (this.WMType)
                    {

                        case SWFWaterMarkType.WM_IMAGE: //水印图片
                            this.addWatermarkImage(g, _waterMarkImgPath, WMLocation, image.Width, image.Height);
                            break;

                        case SWFWaterMarkType.WM_TEXT://水印文字
                            this.addWatermarkText(g, this.WaterMarkText, WMLocation, image.Width, image.Height);
                            break;
                    }

                    #region 降低图片质量
                    System.Drawing.Imaging.ImageCodecInfo encoder = GetEncoderInfo("image/jpeg");
                    System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters(1);
                    encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)80);
                    #endregion
                    b.Save(newpath, encoder, encoderParams);
                    encoderParams.Dispose();
                    b.Dispose();
                    image.Dispose();
                }
            }
            catch
            {
                if (File.Exists(oldpath))
                {
                    File.Delete(oldpath);
                }
            }
            finally
            {
                if (File.Exists(oldpath))
                {
                    File.Delete(oldpath);
                }
            }
        }

        /// <summary>
        ///  加水印图片
        /// </summary>
        /// <param name="picture">imge 对象</param>
        /// <param name="WaterMarkPicPath">水印图片的地址</param>
        /// <param name="_watermarkPosition">水印位置</param>
        /// <param name="_width">被加水印图片的宽</param>
        /// <param name="_height">被加水印图片的高</param>
        private void addWatermarkImage(Graphics picture, string WaterMarkPicPath, SWFWaterMarkLocation location, int _width, int _height)
        {
            Image watermark = new Bitmap(WaterMarkPicPath);

            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap();

            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            float[][] colorMatrixElements = {
                                                 new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                                 new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                                 new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                                 new float[] {0.0f,  0.0f,  0.0f,  0.3f, 0.0f},
                                                 new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                                             };

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            //设置透明色
            //imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            int xpos = 0;
            int ypos = 0;
            int WatermarkWidth = 0;
            int WatermarkHeight = 0;
            double bl = 1d;
            //计算水印图片的比率
            //取背景的1/4宽度来比较
            if ((_width > watermark.Width * 4) && (_height > watermark.Height * 4))
            {
                bl = 1;
            }
            else if ((_width > watermark.Width * 4) && (_height < watermark.Height * 4))
            {
                bl = Convert.ToDouble(_height / 4) / Convert.ToDouble(watermark.Height);

            }
            else

                if ((_width < watermark.Width * 4) && (_height > watermark.Height * 4))
                {
                    bl = Convert.ToDouble(_width / 4) / Convert.ToDouble(watermark.Width);
                }
                else
                {
                    if ((_width * watermark.Height) > (_height * watermark.Width))
                    {
                        bl = Convert.ToDouble(_height / 4) / Convert.ToDouble(watermark.Height);

                    }
                    else
                    {
                        bl = Convert.ToDouble(_width / 4) / Convert.ToDouble(watermark.Width);

                    }

                }
            WatermarkWidth = Convert.ToInt32(watermark.Width * bl);
            WatermarkHeight = Convert.ToInt32(watermark.Height * bl);
            switch (location)
            {
                case SWFWaterMarkLocation.WM_TOP_LEFT:
                    xpos = 10;
                    ypos = 10;
                    break;
                case SWFWaterMarkLocation.WM_TOP_RIGHT:
                    xpos = _width - WatermarkWidth - 10;
                    ypos = 10;
                    break;
                case SWFWaterMarkLocation.WM_BOTTOM_RIGHT:
                    xpos = _width - WatermarkWidth - 10;
                    ypos = _height - WatermarkHeight - 10;
                    break;
                case SWFWaterMarkLocation.WM_BOTTOM_LEFT:
                    xpos = 10;
                    ypos = _height - WatermarkHeight - 10;
                    break;
            }

            picture.DrawImage(watermark, new Rectangle(xpos, ypos, WatermarkWidth, WatermarkHeight), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);


            watermark.Dispose();
            imageAttributes.Dispose();
        }
        /// <summary>
        ///  加水印文字
        /// </summary>
        /// <param name="picture">imge 对象</param>
        /// <param name="_watermarkText">水印文字内容</param>
        /// <param name="_watermarkPosition">水印位置</param>
        /// <param name="_width">被加水印图片的宽</param>
        /// <param name="_height">被加水印图片的高</param>
        private void addWatermarkText(Graphics picture, string _watermarkText, SWFWaterMarkLocation location, int _width, int _height)
        {
            int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };
            Font crFont = null;
            SizeF crSize = new SizeF();
            for (int i = 0; i < 7; i++)
            {
                crFont = new Font("arial", sizes[i], FontStyle.Bold);
                crSize = picture.MeasureString(_watermarkText, crFont);

                if ((ushort)crSize.Width < (ushort)_width)
                    break;
            }
            float xpos = 0;
            float ypos = 0;
            switch (location)
            {
                case SWFWaterMarkLocation.WM_TOP_LEFT:
                    xpos = ((float)_width * (float).01) + (crSize.Width / 2);
                    ypos = (float)_height * (float).01;
                    break;
                case SWFWaterMarkLocation.WM_TOP_RIGHT:
                    xpos = ((float)_width * (float).99) - (crSize.Width / 2);
                    ypos = (float)_height * (float).01;
                    break;
                case SWFWaterMarkLocation.WM_BOTTOM_RIGHT:
                    xpos = ((float)_width * (float).99) - (crSize.Width / 2);
                    ypos = ((float)_height * (float).99) - crSize.Height;
                    break;
                case SWFWaterMarkLocation.WM_BOTTOM_LEFT:
                    xpos = ((float)_width * (float).01) + (crSize.Width / 2);
                    ypos = ((float)_height * (float).99) - crSize.Height;
                    break;
            }

            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;

            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));
            picture.DrawString(_watermarkText, crFont, semiTransBrush2, xpos + 1, ypos + 1, StrFormat);

            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));
            picture.DrawString(_watermarkText, crFont, semiTransBrush, xpos, ypos, StrFormat);

            semiTransBrush2.Dispose();
            semiTransBrush.Dispose();
        }
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="oldpath">原图片地址</param>
        /// <param name="newpath">新图片地址</param>
        /// <param name="tWidth">缩略图的宽</param>
        /// <param name="tHeight">缩略图的高</param>
        private void GreateMiniImage(string oldpath, string newpath, int tWidth, int tHeight)
        {
            try
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(oldpath);
                #region 原有代码(图片尺寸等比缩放)
                //double bl = 1d;
                //if ((image.Width <= image.Height) && (tWidth >= tHeight))
                //{
                //    bl = Convert.ToDouble(image.Height) / Convert.ToDouble(tHeight);
                //}
                //else if ((image.Width > image.Height) && (tWidth < tHeight))
                //{
                //    bl = Convert.ToDouble(image.Width) / Convert.ToDouble(tWidth);
                //}
                //else
                //    if ((image.Width <= image.Height) && (tWidth <= tHeight))
                //    {
                //        if (image.Height / tHeight >= image.Width / tWidth)
                //        {
                //            bl = Convert.ToDouble(image.Width) / Convert.ToDouble(tWidth);
                //        }
                //        else
                //        {
                //            bl = Convert.ToDouble(image.Height) / Convert.ToDouble(tHeight);
                //        }
                //    }
                //    else
                //    {
                //        if (image.Height / tHeight >= image.Width / tWidth)
                //        {
                //            bl = Convert.ToDouble(image.Height) / Convert.ToDouble(tHeight);
                //        }
                //        else
                //        {
                //            bl = Convert.ToDouble(image.Width) / Convert.ToDouble(tWidth);
                //        }
                //    }
                //Bitmap b = new Bitmap(image, Convert.ToInt32(image.Width / bl), Convert.ToInt32(image.Height / bl));
                #endregion
                #region 2010-03-29 改造
                int imgWidth = image.Width;
                int imgHeight = image.Height;
                int newWidth = 0;
                int newHeight = 0;
                if (imgWidth > tWidth)
                {
                    newWidth = tWidth;
                    newHeight = tWidth * imgHeight / imgWidth;
                    if (newHeight > tHeight)
                    {
                        newWidth = tHeight * newWidth / newHeight;
                        newHeight = tHeight;
                    }
                }
                else if (imgHeight > tHeight)
                {
                    newHeight = tHeight;
                    newWidth = tHeight * imgWidth / imgHeight;
                    if (newWidth > tWidth)
                    {
                        newHeight = tWidth * newHeight / newWidth;
                        newWidth = tWidth;
                    }
                }
                else
                {
                    newWidth = imgWidth;
                    newHeight = imgHeight;
                }
                Bitmap b = new Bitmap(image, newWidth, newHeight);
                #endregion
                b.Save(newpath);
                b.Dispose();
                image.Dispose();
            }
            catch
            {
            }
        }
        #endregion
        public static string GetSmallPicName(string picName)
        {
            return SWFWebConfigManage.GetByAppSettingsKey("WaterMarkPath") + picName;//5~1-a-s-p-x
        }
    }
}
using System;
using System.Web;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace JuSNS.Common
{
    public abstract class UploadPic
    {
        /// <summary>
        /// 类型
        /// </summary>
        protected readonly string availExtension = JuSNS.Common.Public.GetXMLValue("pictype");
        /// <summary>
        /// 大小
        /// </summary>
        protected readonly int size = 1024 * Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("picsize")) * 1000;

        /// <summary>
        /// 根目录
        /// </summary>
        protected string RootDir 
        {
            get
            {
                return HttpContext.Current.Request.ApplicationPath;
            }
        }

        /// <summary>
        /// 上传的文件
        /// </summary>
        protected HttpFileCollection hfc;
        /// <summary>
        /// 输出的文件名
        /// </summary>
        public string OutFileName
        {
            get { return _FileName[_FileName.Length - 1]; }
        }

        protected int _SuccessNum;
        /// <summary>
        /// 上传成功数量
        /// </summary>
        public int SuccessNum
        {
            get { return _SuccessNum; }
        }

        private string _server;
        /// <summary>
        /// 要上传到的服务器
        /// </summary>
        public string server
        {
            get { return _server; }
            set { _server = value; }
        }

        protected int[] _Width;
        /// <summary>
        /// 输出图片的宽度
        /// </summary>
        public int[] Width
        {
            get { return _Width; }
        }
        protected int[] _Height;
        /// <summary>
        /// 输出图片的高度
        /// </summary>
        public int[] Height
        {
            get { return _Height; }
        }
        /// <summary>
        /// 输入最后一个图片的高
        /// </summary>
        public int LastHeight
        {
            get { return _Height[_Height.Length - 1]; }
        }
        /// <summary>
        /// 输出最后一个图片的宽
        /// </summary>
        public int LastWidth
        {
            get { return _Width[_Width.Length - 1]; }
        }
        protected string[] _FileName;
        /// <summary>
        /// 输出的文件名
        /// </summary>
        public string[] FileName
        {
            get { return _FileName; }
        }
        protected int[] _ContentLength;
        /// <summary>
        /// 图片内存大小
        /// </summary>
        public int[] ContentLength
        {
            get { return _ContentLength; }
        }
        /// <summary>
        /// 最后一个图片内存大小
        /// </summary>
        public int LastContentLength
        {
            get { return _ContentLength[_ContentLength.Length - 1]; }
        }
        protected string _errmsg;
        /// <summary>
        /// 最后一个错误信息
        /// </summary>
        protected string errmsg
        {
            get { return _errmsg; }
        }
        public UploadPic(HttpFileCollection hfc)
        {
            this.hfc = hfc;
            _Width = new int[1];
            _Height = new int[1];
        }

        public UploadPic()
        {
            this.hfc = HttpContext.Current.Request.Files;
        }
        /// <summary>
        /// 开始上传
        /// </summary>
        abstract public int Start();
        /// <summary>
        /// 生成剪切图
        /// </summary>
        /// <param name="data">输入图数据</param>
        /// <param name="bit">生成的文件</param>
        /// <param name="x">坐标X</param>
        /// <param name="y">坐标Y</param>
        /// <param name="w">宽</param>
        /// <param name="h">高</param>
        static public void makeRectImage(byte[] data, out byte[] bit, int x, int y, int w, int h)
        {
            Bitmap bmp = Bitmap.FromStream(new MemoryStream(data)) as System.Drawing.Bitmap;
            Rectangle rec = new Rectangle(x, y, w, h);
            if (bmp.Width < w || bmp.Height < h)
            {
                int rx, ry, rw, rh;
                if (bmp.Width > bmp.Height)
                {
                    rx = (bmp.Width - bmp.Height) / 2;
                    ry = 0;
                    rw = bmp.Height;
                    rh = bmp.Height;
                    rec = new Rectangle(rx, ry, rw, rh);
                }
                else
                {
                    rx = 0;
                    ry = (bmp.Height - bmp.Width) / 2;
                    rw = bmp.Width;
                    rh = bmp.Width;
                    rec = new Rectangle(rx, ry, rw, rh);
                }
            }
            Bitmap bmp1 = bmp.Clone(rec, bmp.PixelFormat);
            MemoryStream ms = new MemoryStream();
            ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myImageCodecInfo = ImageCodecInfo.GetImageEncoders()[0];
            myEncoder = System.Drawing.Imaging.Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, 0L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save(ms, myImageCodecInfo, myEncoderParameters);
            bit = ms.ToArray();
            bmp1.Dispose();
            ms.Dispose();
        }
        /// <summary>
        /// 允许的文件类型
        /// </summary>
        /// <param name="ext">文件类型</param>
        /// <returns></returns>
        protected bool AllowExt(string ext)
        {
            return availExtension.IndexOf(ext) > -1 ? true : false;
        }
        /// <summary>
        /// 取得文件扩展名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        protected string GetExt(string filename)
        {
            int pos = filename.LastIndexOf(".");
            return filename.Substring(pos + 1);
        }
        /// <summary>
        /// 取得主文件名
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        protected string GetMainName(string filename)
        {
            int pos = filename.LastIndexOfAny(new char[] { '\\', '/' });
            int pos1 = 0;
            int len = pos - pos1;
            return filename.Substring(pos1 + 1, len);
        }
    }
}

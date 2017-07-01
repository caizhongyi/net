using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace JuSNS.Common
{
    public enum DealType { NONE, WaterImage, WaterFont, DoubleDo, Title, Copyright }; //枚举命令 
    /// <summary>
    /// 水印图片处理类
    /// </summary>
    public class ReDrawImg
    {
        private string ImageName = string.Empty; //被处理的图片物理路径
        private string ImageWater = string.Empty; //水印图片物理路径
        private string FontString = string.Empty; //水印文字 
        private string targetImagePath = string.Empty;
        public Image inputImage = null; //输入image
        public Image outImage = null;  //输出image


        private DealType dealtype;

        /// <summary>
        /// 生成水印图片
        /// </summary>
        /// <param name="imagePath">被处理的图片物理路径</param>
        /// <param name="imageWaterPath">水印图片物理路径</param>
        /// <param name="fontString">水印文字</param>
        /// <param name="targetImagePath">要生成的图片物理路径，扩展名为jpg</param>
        public static void DealImage(string imagePath, string imageWaterPath, string fontString, string targetImagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                throw new Exception("被处理的图片物理路径 未指定");
            if (string.IsNullOrEmpty(imageWaterPath) && string.IsNullOrEmpty(fontString))
                throw new Exception("未指定水印图片或文字");

            ReDrawImg reDrawImg = new ReDrawImg();
            reDrawImg.ImageName = imagePath;
            reDrawImg.ImageWater = imageWaterPath;
            reDrawImg.FontString = fontString;
            reDrawImg.targetImagePath = targetImagePath;
            reDrawImg.DealImage();
        }
        /// <summary>
        /// 生成水印图片 返回 image类型
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="imageWaterPath"></param>
        /// <param name="fontString"></param>
        public static Image DealImage(Image image, string imageWaterPath, string fontString, DealType dealType)
        {
            if (image == null)
                throw new Exception("被处理的图片未指定");
            ReDrawImg reDrawImg = new ReDrawImg();
            reDrawImg.inputImage = image;
            reDrawImg.ImageWater = imageWaterPath;
            reDrawImg.FontString = fontString;
            reDrawImg.targetImagePath = string.Empty;
            reDrawImg.dealtype = dealType;
            reDrawImg.DealImage();
            return reDrawImg.outImage;
        }


        void DealImage()
        {
            if (dealtype == DealType.NONE)
            {
                IsDouble();
            }

            switch (dealtype)
            {
                case DealType.WaterFont: WriteFont(); break;
                case DealType.WaterImage: WriteImg(); break;
                case DealType.DoubleDo: WriteFontAndImg(); break;
                case DealType.Title: WriteImgTitle(); break;
                case DealType.Copyright: WriteImgCopyright(); break;
            }

        }

        private void IsDouble()
        {
            if (!string.IsNullOrEmpty(ImageWater) && !string.IsNullOrEmpty(FontString))
            {
                dealtype = DealType.DoubleDo;
            }
            else if (!string.IsNullOrEmpty(ImageWater))
            {
                dealtype = DealType.WaterImage;
            }
            else if (!string.IsNullOrEmpty(FontString))
            {
                dealtype = DealType.WaterFont;
            }
        }

        /// <summary>
        /// 写字方法
        /// </summary>
        private void WriteFont()
        {
            Image imgPhoto = null;
            if (!string.IsNullOrEmpty(ImageName))
            {
                imgPhoto = Image.FromFile(ImageName);
            }
            else
            {
                if (this.inputImage == null) throw new Exception("没有任何要处理的图像！");
                imgPhoto = this.inputImage;
            }
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;

            //create a Bitmap the Size of the original photograph
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            //load the Bitmap into a Graphics object 
            Graphics grPhoto = Graphics.FromImage(bmPhoto);

            //------------------------------------------------------------
            //Step #1 - Insert Copyright message
            //------------------------------------------------------------ 

            //Set the rendering quality for this Graphics object
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

            //Draws the photo Image object at original size to the graphics object.
            grPhoto.DrawImage(
            imgPhoto, // Photo Image object
            new Rectangle(0, 0, phWidth, phHeight), // Rectangle structure
            0, // x-coordinate of the portion of the source image to draw. 
            0, // y-coordinate of the portion of the source image to draw. 
            phWidth, // Width of the portion of the source image to draw. 
            phHeight, // Height of the portion of the source image to draw. 
            GraphicsUnit.Pixel); // Units of measure 

            //-------------------------------------------------------
            //to maximize the size of the Copyright message we will 
            //test multiple Font sizes to determine the largest posible 
            //font we can use for the width of the Photograph
            //define an array of point sizes you would like to consider as possiblities
            //-------------------------------------------------------
            int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };

            Font crFont = null;
            SizeF crSize = new SizeF();

            //Loop through the defined sizes checking the length of the Copyright string
            //If its length in pixles is less then the image width choose this Font size.
            for (int i = 0; i < 7; i++)
            {
                //set a Font object to Arial (i)pt, Bold
                //crFont = new Font("arial", sizes[i], FontStyle.Bold); 

                crFont = new Font("arial", sizes[i], FontStyle.Bold);

                //Measure the Copyright string in this Font
                crSize = grPhoto.MeasureString(FontString, crFont);

                if ((ushort)crSize.Width < (ushort)phWidth)
                    break;
            }

            //Since all photographs will have varying heights, determine a 
            //position 5% from the bottom of the image
            int yPixlesFromBottom = (int)(phHeight * .08);

            //Now that we have a point size use the Copyrights string height 
            //to determine a y-coordinate to draw the string of the photograph
            float yPosFromBottom = ((phHeight - yPixlesFromBottom) - (crSize.Height / 2));

            //Determine its x-coordinate by calculating the center of the width of the image
            float xCenterOfImg = (phWidth / 2);

            //Define the text layout by setting the text alignment to centered
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;

            //define a Brush which is semi trasparent black (Alpha set to 153)
            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));

            //Draw the Copyright string
            grPhoto.DrawString(FontString, //string of text
            crFont, //font
            semiTransBrush2, //Brush
            new PointF(xCenterOfImg + 1, yPosFromBottom + 1), //Position
            StrFormat);

            //define a Brush which is semi trasparent white (Alpha set to 153)
            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));

            //Draw the Copyright string a second time to create a shadow effect
            //Make sure to move this text 1 pixel to the right and down 1 pixel
            grPhoto.DrawString(FontString, //string of text
            crFont, //font
            semiTransBrush, //Brush
            new PointF(xCenterOfImg, yPosFromBottom), //Position
            StrFormat);
            imgPhoto = bmPhoto;
            grPhoto.Dispose();

            //save new image to file system.
            //imgPhoto.Save(this.targetImagePath, ImageFormat.Jpeg);
            //imgPhoto.Dispose();
            //Text alignment
            if (!string.IsNullOrEmpty(targetImagePath))
            {
                imgPhoto.Save(this.targetImagePath, ImageFormat.Jpeg);
                imgPhoto.Dispose();
            }
            else
            {
                outImage = imgPhoto;
            }
        }

        private void WriteImg()
        {
            //set a working directory
            //string WorkingDirectory = @"C:\Watermark_src\WaterPic" 

            //create a image object containing the photograph to watermark
            Image imgPhoto = null;
            if (!string.IsNullOrEmpty(ImageName))
            {
                imgPhoto = Image.FromFile(ImageName);
            }
            else
            {
                if (this.inputImage == null) throw new Exception("没有任何要处理的图像！");
                imgPhoto = this.inputImage;
            }
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;

            //create a Bitmap the Size of the original photograph
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            //load the Bitmap into a Graphics object 
            Graphics grPhoto = Graphics.FromImage(bmPhoto);

            //create a image object containing the watermark
            Image imgWatermark = new Bitmap(ImageWater);
            int wmWidth = imgWatermark.Width;
            int wmHeight = imgWatermark.Height;

            //Set the rendering quality for this Graphics object
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

            //Draws the photo Image object at original size to the graphics object.
            grPhoto.DrawImage(
            imgPhoto, // Photo Image object
            new Rectangle(0, 0, phWidth, phHeight), // Rectangle structure
            0, // x-coordinate of the portion of the source image to draw. 
            0, // y-coordinate of the portion of the source image to draw. 
            phWidth, // Width of the portion of the source image to draw. 
            phHeight, // Height of the portion of the source image to draw. 
            GraphicsUnit.Pixel); // Units of measure 

            //------------------------------------------------------------
            //Step #2 - Insert Watermark image
            //------------------------------------------------------------ 

            //Create a Bitmap based on the previously modified photograph Bitmap
            Bitmap bmWatermark = new Bitmap(bmPhoto);
            bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
            //Load this Bitmap into a new Graphic Object
            Graphics grWatermark = Graphics.FromImage(bmWatermark);

            //To achieve a transulcent watermark we will apply (2) color 
            //manipulations by defineing a ImageAttributes object and 
            //seting (2) of its properties.
            ImageAttributes imageAttributes = new ImageAttributes();

            //The first step in manipulating the watermark image is to replace 
            //the background color with one that is trasparent (Alpha=0, R=0, G=0, B=0)
            //to do this we will use a Colormap and use this to define a RemapTable
            ColorMap colorMap = new ColorMap();

            //My watermark was defined with a background of 100% Green this will
            //be the color we search for and replace with transparency
            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);

            ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            //The second color manipulation is used to change the opacity of the 
            //watermark. This is done by applying a 5x5 matrix that contains the 
            //coordinates for the RGBA space. By setting the 3rd row and 3rd column 
            //to 0.3f we achive a level of opacity
            float[][] colorMatrixElements = { 
new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f}, 
new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f}, 
new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f}, 
new float[] {0.0f, 0.0f, 0.0f, 0.3f, 0.0f}, 
new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}};
            ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default,
            ColorAdjustType.Bitmap);

            //For this example we will place the watermark in the upper right
            //hand corner of the photograph. offset down 10 pixels and to the 
            //left 10 pixles 

            int xPosOfWm = ((phWidth - wmWidth) - 10);
            int yPosOfWm = 10;

            grWatermark.DrawImage(imgWatermark,
            new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight), //Set the detination Position
            0, // x-coordinate of the portion of the source image to draw. 
            0, // y-coordinate of the portion of the source image to draw. 
            wmWidth, // Watermark Width
            wmHeight, // Watermark Height
            GraphicsUnit.Pixel, // Unit of measurment
            imageAttributes); //ImageAttributes Object 

            //Replace the original photgraphs bitmap with the new Bitmap
            imgPhoto = bmWatermark;
            grPhoto.Dispose();
            grWatermark.Dispose();

            //save new image to file system.
            if (!string.IsNullOrEmpty(targetImagePath))
            {
                imgPhoto.Save(this.targetImagePath, ImageFormat.Jpeg);
                imgPhoto.Dispose();
            }
            else
            {
                outImage = imgPhoto;
            }
            imgWatermark.Dispose();

        }

        /// <summary>
        /// 写字和画图
        /// </summary>
        private void WriteFontAndImg()
        {
            //create a image object containing the photograph to watermark
            Image imgPhoto = null;
            if (!string.IsNullOrEmpty(ImageName))
            {
                imgPhoto = Image.FromFile(ImageName);
            }
            else
            {
                if (this.inputImage == null) throw new Exception("没有任何要处理的图像！");
                imgPhoto = this.inputImage;
            }
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;

            //create a Bitmap the Size of the original photograph
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            //load the Bitmap into a Graphics object 
            Graphics grPhoto = Graphics.FromImage(bmPhoto);

            //create a image object containing the watermark
            Image imgWatermark = new Bitmap(ImageWater);
            int wmWidth = imgWatermark.Width;
            int wmHeight = imgWatermark.Height;

            //------------------------------------------------------------
            //Step #1 - Insert Copyright message
            //------------------------------------------------------------ 

            //Set the rendering quality for this Graphics object
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

            //Draws the photo Image object at original size to the graphics object.
            grPhoto.DrawImage(
            imgPhoto, // Photo Image object
            new Rectangle(0, 0, phWidth, phHeight), // Rectangle structure
            0, // x-coordinate of the portion of the source image to draw. 
            0, // y-coordinate of the portion of the source image to draw. 
            phWidth, // Width of the portion of the source image to draw. 
            phHeight, // Height of the portion of the source image to draw. 
            GraphicsUnit.Pixel); // Units of measure 

            //-------------------------------------------------------
            //to maximize the size of the Copyright message we will 
            //test multiple Font sizes to determine the largest posible 
            //font we can use for the width of the Photograph
            //define an array of point sizes you would like to consider as possiblities
            //-------------------------------------------------------
            int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };

            Font crFont = null;
            SizeF crSize = new SizeF();

            //Loop through the defined sizes checking the length of the Copyright string
            //If its length in pixles is less then the image width choose this Font size.
            for (int i = 0; i < 7; i++)
            {
                //set a Font object to Arial (i)pt, Bold
                crFont = new Font("arial", sizes[i], FontStyle.Bold);
                //Measure the Copyright string in this Font
                crSize = grPhoto.MeasureString(FontString, crFont);

                if ((ushort)crSize.Width < (ushort)phWidth)
                    break;
            }

            //Since all photographs will have varying heights, determine a 
            //position 5% from the bottom of the image
            int yPixlesFromBottom = (int)(phHeight * .05);

            //Now that we have a point size use the Copyrights string height 
            //to determine a y-coordinate to draw the string of the photograph
            float yPosFromBottom = ((phHeight - yPixlesFromBottom) - (crSize.Height / 2));

            //Determine its x-coordinate by calculating the center of the width of the image
            float xCenterOfImg = (phWidth / 2);

            //Define the text layout by setting the text alignment to centered
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;

            //define a Brush which is semi trasparent black (Alpha set to 153)
            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));

            //Draw the Copyright string
            grPhoto.DrawString(FontString, //string of text
            crFont, //font
            semiTransBrush2, //Brush
            new PointF(xCenterOfImg + 1, yPosFromBottom + 1), //Position
            StrFormat);

            //define a Brush which is semi trasparent white (Alpha set to 153)
            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));

            //Draw the Copyright string a second time to create a shadow effect
            //Make sure to move this text 1 pixel to the right and down 1 pixel
            grPhoto.DrawString(FontString, //string of text
            crFont, //font
            semiTransBrush, //Brush
            new PointF(xCenterOfImg, yPosFromBottom), //Position
            StrFormat); //Text alignment 

            //------------------------------------------------------------
            //Step #2 - Insert Watermark image
            //------------------------------------------------------------ 

            //Create a Bitmap based on the previously modified photograph Bitmap
            Bitmap bmWatermark = new Bitmap(bmPhoto);
            bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
            //Load this Bitmap into a new Graphic Object
            Graphics grWatermark = Graphics.FromImage(bmWatermark);

            //To achieve a transulcent watermark we will apply (2) color 
            //manipulations by defineing a ImageAttributes object and 
            //seting (2) of its properties.
            ImageAttributes imageAttributes = new ImageAttributes();

            //The first step in manipulating the watermark image is to replace 
            //the background color with one that is trasparent (Alpha=0, R=0, G=0, B=0)
            //to do this we will use a Colormap and use this to define a RemapTable
            ColorMap colorMap = new ColorMap();

            //My watermark was defined with a background of 100% Green this will
            //be the color we search for and replace with transparency
            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);

            ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            //The second color manipulation is used to change the opacity of the 
            //watermark. This is done by applying a 5x5 matrix that contains the 
            //coordinates for the RGBA space. By setting the 3rd row and 3rd column 
            //to 0.3f we achive a level of opacity
            float[][] colorMatrixElements = { 
new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f}, 
new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f}, 
new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f}, 
new float[] {0.0f, 0.0f, 0.0f, 0.3f, 0.0f}, 
new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}};
            ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default,
            ColorAdjustType.Bitmap);

            //For this example we will place the watermark in the upper right
            //hand corner of the photograph. offset down 10 pixels and to the 
            //left 10 pixles 

            int xPosOfWm = ((phWidth - wmWidth) - 10);
            int yPosOfWm = 10;

            grWatermark.DrawImage(imgWatermark,
            new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight), //Set the detination Position
            0, // x-coordinate of the portion of the source image to draw. 
            0, // y-coordinate of the portion of the source image to draw. 
            wmWidth, // Watermark Width
            wmHeight, // Watermark Height
            GraphicsUnit.Pixel, // Unit of measurment
            imageAttributes); //ImageAttributes Object 

            //Replace the original photgraphs bitmap with the new Bitmap
            imgPhoto = bmWatermark;
            grPhoto.Dispose();
            grWatermark.Dispose();

            //save new image to file system.
            if (!string.IsNullOrEmpty(targetImagePath))
            {
                imgPhoto.Save(this.targetImagePath, ImageFormat.Jpeg);
                imgPhoto.Dispose();
            }
            else
            {
                //输出到 outImage
                outImage = imgPhoto;
            }
            imgWatermark.Dispose();

        }
        /// <summary>
        /// 标题图片
        /// </summary>
        private void WriteImgTitle()
        {
            Font CopyrightFont = new Font("楷体_GB2312", 14, FontStyle.Bold);
            Image imgPhoto = null;
            if (!string.IsNullOrEmpty(ImageName))
            {
                imgPhoto = Image.FromFile(ImageName);
            }
            else
            {
                if (this.inputImage == null) throw new Exception("没有任何要处理的图像！");
                imgPhoto = this.inputImage;
            }
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;

            //create a Bitmap the Size of the original photograph
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            //load the Bitmap into a Graphics object 
            Graphics grPhoto = Graphics.FromImage(bmPhoto);

            //------------------------------------------------------------
            //Step #1 - Insert Copyright message
            //------------------------------------------------------------

            //Set the rendering quality for this Graphics object
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

            //Draws the photo Image object at original size to the graphics object.
            grPhoto.DrawImage(
             imgPhoto,                                 // Photo Image object
             new Rectangle(0, 0, phWidth, phHeight), // Rectangle structure
             0,                                        // x-coordinate of the portion of the source image to draw. 
             0,                                        // y-coordinate of the portion of the source image to draw. 
             phWidth,                                  // Width of the portion of the source image to draw. 
             phHeight,                                 // Height of the portion of the source image to draw. 
             GraphicsUnit.Pixel);                      // Units of measure 


            Image ImgTitle = null;

            using (Font fnt = CopyrightFont)
                ImgTitle = (Bitmap)ImageFromText(FontString, fnt, Color.Black, Color.White, imgPhoto);

            int xStart = (imgPhoto.Width - ImgTitle.Width) / 2;
            int yPosFromBottom = phHeight - 30;
            grPhoto.DrawImage(ImgTitle, new PointF(xStart, yPosFromBottom));

            imgPhoto = bmPhoto;
            grPhoto.Dispose();


            //save new image to file system.
            if (!string.IsNullOrEmpty(targetImagePath))
            {
                imgPhoto.Save(this.targetImagePath, ImageFormat.Jpeg);
                imgPhoto.Dispose();
            }
            else
            {
                //输出到 outImage
                outImage = imgPhoto;
            }

            ImgTitle.Dispose();
        }
        /// <summary>
        /// 版权图片
        /// </summary>
        private void WriteImgCopyright()
        {
            Image imgPhoto = null;
            if (!string.IsNullOrEmpty(ImageName))
            {
                imgPhoto = Image.FromFile(ImageName);
            }
            else
            {
                if (this.inputImage == null) throw new Exception("没有任何要处理的图像！");
                imgPhoto = this.inputImage;
            }
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;

            //create a Bitmap the Size of the original photograph

            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            //load the Bitmap into a Graphics object 
            Graphics grPhoto = Graphics.FromImage(bmPhoto);

            //------------------------------------------------------------
            //Step #1 - Insert Copyright message
            //------------------------------------------------------------

            //Set the rendering quality for this Graphics object
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

            //Draws the photo Image object at original size to the graphics object.
            grPhoto.DrawImage(
             imgPhoto,                                 // Photo Image object
             new Rectangle(0, 0, phWidth, phHeight), // Rectangle structure
             0,                                        // x-coordinate of the portion of the source image to draw. 
             0,                                        // y-coordinate of the portion of the source image to draw. 
             phWidth,                                  // Width of the portion of the source image to draw. 
             phHeight,                                 // Height of the portion of the source image to draw. 
             GraphicsUnit.Pixel);                      // Units of measure 


            Image ImgCopyRight = null;
            //using (Font fnt = new Font("华文隶书", 22, FontStyle.Bold))
            using (Font fnt = new Font("华文隶书", 12, FontStyle.Bold))
            {
                ImgCopyRight = (Bitmap)ImageFromText(FontString, fnt, Color.FromArgb(153, 155, 154), Color.White, imgPhoto);
                //ImgCopyRight = (Bitmap)FancyText.ImageFromText(FontString, fnt, Color.FromArgb(153, 156, 154), Color.White, fromimg);
            }

            int xStart = imgPhoto.Width - ImgCopyRight.Width - 10;
            int yPosFromBottom = phHeight - 25;
            grPhoto.DrawImage(ImgCopyRight, new PointF(xStart, yPosFromBottom));

            imgPhoto = bmPhoto;
            grPhoto.Dispose();

            //save new image to file system.
            if (!string.IsNullOrEmpty(targetImagePath))
            {
                imgPhoto.Save(this.targetImagePath, ImageFormat.Jpeg);
                imgPhoto.Dispose();
            }
            else
            {
                //输出到 outImage
                outImage = imgPhoto;
            }
            ImgCopyRight.Dispose();
        }
        private Image ImageFromText(string strText, Font fnt, Color clrFore, Color clrBack, Image fromimg)
        {
            Image imgPhoto = null;
            if (!string.IsNullOrEmpty(ImageName))
            {
                imgPhoto = Image.FromFile(ImageName);
            }
            else
            {
                if (this.inputImage == null) throw new Exception("没有任何要处理的图像！");
                imgPhoto = this.inputImage;
            }
            const int blurAmount = 5;
            Font CopyrightFont = new Font("楷体_GB2312", 14, FontStyle.Bold);
            Bitmap bmpOut = null; // bitmap we are creating and will return from this function.
            int iPadding = 4;      //标题水印在图上显示的左右边距(当文字太多时)


            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;

            //create a Bitmap the Size of the original photograph
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            //load the Bitmap into a Graphics object 
            using (Graphics g = Graphics.FromImage(bmPhoto))
            {
                SizeF sz = g.MeasureString(strText, fnt);

                bool blFirst = true;
                string strTempText;

                for (int iIndex = strText.Length; iIndex > 0; iIndex--)
                {
                    if (blFirst)
                        strTempText = strText.Substring(0, iIndex);
                    else
                        strTempText = strText.Substring(0, iIndex) + "…";

                    sz = g.MeasureString(strTempText, fnt);
                    if ((bmPhoto.Width - iPadding) >= sz.Width)
                    {
                        strText = strTempText;
                        break;
                    }
                    blFirst = false;


                }
                using (Bitmap bmp = new Bitmap((int)sz.Width, (int)sz.Height))
                using (Graphics gBmp = Graphics.FromImage(bmp))
                using (SolidBrush brBack = new SolidBrush(Color.FromArgb(16, clrBack.R, clrBack.G, clrBack.B)))
                using (SolidBrush brFore = new SolidBrush(clrFore))
                {
                    gBmp.SmoothingMode = SmoothingMode.HighQuality;
                    gBmp.InterpolationMode = InterpolationMode.HighQualityBilinear;
                    gBmp.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                    StringFormat StrFormat = new StringFormat();
                    StrFormat.Alignment = StringAlignment.Near;
                    gBmp.DrawString(strText, fnt, brBack, 0, 0, StrFormat);

                    // make bitmap we will return.
                    bmpOut = new Bitmap(bmp.Width + blurAmount, bmp.Height + blurAmount);
                    using (Graphics gBmpOut = Graphics.FromImage(bmpOut))
                    {
                        gBmpOut.SmoothingMode = SmoothingMode.HighQuality;
                        gBmpOut.InterpolationMode = InterpolationMode.HighQualityBilinear;
                        gBmpOut.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                        // smear image of background of text about to make blurred background "halo"
                        for (float x = 0; x <= blurAmount; x = x + 0.5f)
                            for (float y = 0; y <= blurAmount; y = y + 0.5f)
                                gBmpOut.DrawImage(bmp, x, y);


                        gBmpOut.DrawString(strText, fnt, brFore, blurAmount / 2, blurAmount / 2, StrFormat);

                    }
                }
            }

            return bmpOut;
        }
    }
}

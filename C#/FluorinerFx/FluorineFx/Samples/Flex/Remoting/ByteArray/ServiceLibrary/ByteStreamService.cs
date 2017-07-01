using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using FluorineFx;
using FluorineFx.AMF3;
using FluorineFx.IO;
using AForge.Imaging.Filters;

namespace ServiceLibrary
{
    [RemotingService]
    public class ByteStreamService
    {
        public ByteArray ProcessStream(ByteArray ba)
        {
            ba.Uncompress();

            ba.ObjectEncoding = ObjectEncoding.AMF0;
            string str = ba.ReadUTF();
            ba.ObjectEncoding = ObjectEncoding.AMF3;
            str = ba.ReadObject() as string;
            ba.ObjectEncoding = ObjectEncoding.AMF0;
            str = ba.ReadObject() as string;

            ByteArray result = new ByteArray();
            result.ObjectEncoding = ObjectEncoding.AMF0;
            result.WriteUTF(str);
            result.ObjectEncoding = ObjectEncoding.AMF3;
            result.WriteObject(str);
            result.ObjectEncoding = ObjectEncoding.AMF0;
            result.WriteObject(str);

            result.Compress();
            return result;
        }

        public ByteArray UploadImage(ByteArray ba)
        {
            //Create our Bitmap from the ByteArray
            MemoryStream ms = new MemoryStream(ba.GetBuffer());
            Image img = Bitmap.FromStream(ms);
            Bitmap bmp = new Bitmap(img);
            //Apply effect/filter
            Bitmap newImage = new Sepia().Apply(bmp);

            MemoryStream tempStream = new MemoryStream();
            newImage.Save(tempStream, System.Drawing.Imaging.ImageFormat.Png);
            ByteArray result = new ByteArray(tempStream);
            return result;

            //If we want to save it to the FS
            //MemoryStream tempStream = new MemoryStream();
            //newImage.Save(tempStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            //FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath("test.png"), FileMode.Create);
            //tempStream.WriteTo(fs);
            //fs.Close();
        }
    }
}

/*
//Access bitmap data
BitmapData lockData = newImage.LockBits(new Rectangle(0, 0, newImage.Width, newImage.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
// Create an array to store image data
Int32[] imageData = new Int32[newImage.Width * newImage.Height];
// Use the Marshal class to copy image data
System.Runtime.InteropServices.Marshal.Copy(lockData.Scan0, imageData, 0, imageData.Length);
// Unlock image
newImage.UnlockBits(lockData);
//Write resulting image back in a ByteArray
ByteArray result = new ByteArray();
for (int i = 0; i < imageData.Length; i++)
    result.WriteUnsignedInt((uint)imageData[i]);
//Compress the raw image data
result.Compress();
return result;
*/
